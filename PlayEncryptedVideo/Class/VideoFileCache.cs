using log4net;
using PlayEncryptedVideo.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace PlayEncryptedVideo
{
    class VideoFileCache
    {
        //ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private string _encryptedVideoFilePath;
        private int[] _encryptedChunkLength;
        private long[] _encryptedChunkPosition;
        private int[] _sourceChunkLength;
        private int _chunkCount;
        private long _length;

        public long Length
        {
            get
            {
                return _length;
            }
        }

        public string EncryptedVideoFilePath
        {
            get
            {
                return _encryptedVideoFilePath;
            }
        }

        private Hashtable _decryptedChunkByChunkIndex = new Hashtable();
        private List<long> _decryptedChunkIndexSortedByCreationTime = new List<long>();

        private static int _maxDecryptedChunkCount = 16;
        private static int _additionalChunkToCache = 4;

        private Object _lock = new Object();

        public VideoFileCache(string encryptedVideoFilePath)
        {
            _encryptedVideoFilePath = encryptedVideoFilePath;

            // Read chunk information
            using (Stream encryptedVideoFile = File.Open(encryptedVideoFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (Stream encryptedVideoFileStream = File.Open(encryptedVideoFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (BinaryReader reader = new BinaryReader(encryptedVideoFileStream))
                    {
                        _length = 0;

                        encryptedVideoFileStream.Position = encryptedVideoFile.Length - sizeof(int);
                        _chunkCount = reader.ReadInt32();

                        encryptedVideoFileStream.Position = encryptedVideoFile.Length - sizeof(int) - _chunkCount * sizeof(int) - _chunkCount * sizeof(int);

                        _encryptedChunkLength = new int[_chunkCount];
                        _sourceChunkLength = new int[_chunkCount];
                        _encryptedChunkPosition = new long[_chunkCount];

                        for (int i = 0; i < _chunkCount; i++)
                        {
                            _sourceChunkLength[i] = reader.ReadInt32();
                            _length += _sourceChunkLength[i];
                        }

                        long offset = 0;

                        for (int i = 0; i < _chunkCount; i++)
                        {
                            _encryptedChunkLength[i] = reader.ReadInt32();
                            _encryptedChunkPosition[i] = offset;

                            offset += _encryptedChunkLength[i];
                        }
                    }
                }
            }
        }

        public long GetEncryptedChunkPosition(long chunkIndex)
        {
            return _encryptedChunkPosition[chunkIndex];
        }

        public long GetEncryptedChunkLength(long chunkIndex)
        {
            return _encryptedChunkLength[chunkIndex];
        }

        public long GetSourceChunkLength(long chunkIndex)
        {
            return _sourceChunkLength[chunkIndex];
        }

        private void RemoveOldCachedItems()
        {
            // Remove oldest until cache size become ok
            while (_decryptedChunkByChunkIndex.Count >= _maxDecryptedChunkCount && _decryptedChunkIndexSortedByCreationTime.Count > 0)
            {
                _decryptedChunkByChunkIndex.Remove(_decryptedChunkIndexSortedByCreationTime[0]);
                _decryptedChunkIndexSortedByCreationTime.RemoveAt(0);
            }
        }

        private DecryptedChunk GetOrAddDecryptedChunk(long chunkIndex)
        {
            lock (_lock)
            {
                if (_decryptedChunkByChunkIndex.Contains(chunkIndex))
                {
                    //Logger.Log(string.Format("{0}: Chunk #{1} already cached", new object[] { DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"), chunkIndex }));
                    return ((DecryptedChunk)_decryptedChunkByChunkIndex[chunkIndex]);
                }

                //Logger.Log(string.Format("{0}: Chunk #{1} is NOT yet available", new object[] { DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"), chunkIndex }));

                DecryptedChunk DecryptedChunk = new DecryptedChunk(this, chunkIndex);

                _decryptedChunkByChunkIndex.Add(chunkIndex, DecryptedChunk);
                _decryptedChunkIndexSortedByCreationTime.Add(chunkIndex);

                RemoveOldCachedItems();

                return DecryptedChunk;
            }
        }

        private bool CacheChunk(long chunkIndex)
        {
            lock (_lock)
            {
                if (_decryptedChunkByChunkIndex.Contains(chunkIndex))
                    return false;

                //log.Info(string.Format("{0}: Going to add chunk #{1} to cache", new object[] { DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"), chunkIndex }));

                DecryptedChunk DecryptedChunk = new DecryptedChunk(this, chunkIndex);

                _decryptedChunkByChunkIndex.Add(chunkIndex, DecryptedChunk);
                _decryptedChunkIndexSortedByCreationTime.Add(chunkIndex);

                RemoveOldCachedItems();

                // Cached
                return true;
            }
        }

        public byte[] GetDecryptedChunk(long chunkIndex)
        {
            //log.Info(string.Format("{0}: Asked for chunk #{1}, thread = {2}", new object[] { DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"), chunkIndex, Thread.CurrentThread.ManagedThreadId }));

            DecryptedChunk DecryptedChunk = GetOrAddDecryptedChunk(chunkIndex);

            // Start caching some next chunks
            int cachedChunks = 0;
            for (long AdditionalCachedChunkIndex = chunkIndex + 1;
                cachedChunks < _additionalChunkToCache && // Cache some number of next chunks
                AdditionalCachedChunkIndex < _chunkCount &&
                _decryptedChunkByChunkIndex.Count < _maxDecryptedChunkCount; // Doesn't occupy entire cache
                AdditionalCachedChunkIndex++)
            {
                if (CacheChunk(AdditionalCachedChunkIndex))
                    cachedChunks++;
            }

            return DecryptedChunk.DecryptedData;
        }
    }
    class DecryptedChunk
    {
        ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private VideoFileCache _cache;
        private Thread _decryptionThread;
        private long _chunkIndex;
        private byte[] _decryptedData;

        public byte[] DecryptedData
        {
            get
            {
                _decryptionThread.Join();
                return _decryptedData;
            }
        }

        public DecryptedChunk(VideoFileCache cache, long chunkIndex)
        {
            _cache = cache;
            _chunkIndex = chunkIndex;

            _decryptionThread = new Thread(new ThreadStart(Decrypt));
            _decryptionThread.Start();
        }

        private void Decrypt()
        {
            //log.Info(string.Format("{0}: Started decryption chunk #{1}", new object[] { DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"), _chunkIndex }));

            using (Stream encryptedVideoFile = File.Open(_cache.EncryptedVideoFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                encryptedVideoFile.Position = _cache.GetEncryptedChunkPosition(_chunkIndex);

                byte[] data = new byte[_cache.GetEncryptedChunkLength(_chunkIndex)];
                encryptedVideoFile.Read(data, 0, data.Length);

                _decryptedData = VDCSDK.App.DecryptVideo(data, KeyGen.Key, KeyGen.IV, data.Length);
            }

            //log.Info(string.Format("{0}: Ended decryption chunk #{1}", new object[] { DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"), _chunkIndex }));
        }
    }
}
