using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.IO;

namespace PlayEncryptedVideo
{
    class VirtualFileStream : IStream
    {
        private byte[] _currentChunk;
        private long _currentChunkIndex = -1;
        private long _position = 0;
        private Object _lock = new Object();

        private VideoFileCache _cache;

        public VirtualFileStream(string encryptedVideoFilePath)
        {
            _cache = new VideoFileCache(encryptedVideoFilePath);
        }

        private VirtualFileStream(VideoFileCache cache)
        {
            _cache = cache;
        }

        #region IStream Members

        public void Read(byte[] pv, int cb, IntPtr pcbRead)
        {
            int readBytes;

            if (_position < 0 || _position > _cache.Length)
            {
                readBytes = 0;
            }
            else
            {
                // Let's protect _Position: _Position might be changed by another Read() or Seek()
                lock (_lock)
                {
                    int totalReadBytes = 0;
                    int restBytesToCopy = cb;

                    int offsetInOutput = 0;

                    // Let's move chunk by chunk until all requested data is read or end of file reached
                    while (restBytesToCopy > 0 && _position < _cache.Length)
                    {
                        // Original data is splitted into chunks, so let's find a chunk number that corresponds
                        // to current position
                        long requiredChunkIndex = _position / VDCSDK.App.ChunkSize;

                        // We do cache decrypted data, so let's update the cache if either it's not initialized
                        // or cached chunk has another index
                        if (-1 == _currentChunkIndex || _currentChunkIndex != requiredChunkIndex)
                        {
                            _currentChunkIndex = requiredChunkIndex;
                            _currentChunk = _cache.GetDecryptedChunk(requiredChunkIndex);
                        }

                        // So for now uncrypted data is available, now let's get starting point within the chunk
                        // and how many bytes we are able to read from the chunk (chunks might have different lengths)
                        int offsetInChunk = (int)(_position - (_currentChunkIndex * VDCSDK.App.ChunkSize));
                        int restInChunk = (int)(_cache.GetSourceChunkLength(_currentChunkIndex) - offsetInChunk);

                        int bytesToCopy;
                        if (restInChunk < restBytesToCopy)
                            bytesToCopy = restInChunk;
                        else
                            bytesToCopy = restBytesToCopy;

                        // Copy the data...
                        Array.Copy(_currentChunk, offsetInChunk, pv, offsetInOutput, bytesToCopy);

                        // ...and move forward
                        restBytesToCopy -= bytesToCopy;
                        totalReadBytes += bytesToCopy;
                        offsetInOutput += bytesToCopy;
                        _position += bytesToCopy;
                    }

                    readBytes = totalReadBytes;
                }
            }

            if (IntPtr.Zero != pcbRead)
                Marshal.WriteIntPtr(pcbRead, new IntPtr(readBytes));
        }

        public void Write(byte[] pv, int cb, IntPtr pcbWritten)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Clone(out IStream ppstm)
        {
            ppstm = new VirtualFileStream(_cache);
        }

        public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
        {
            SeekOrigin origin = (SeekOrigin)dwOrigin;

            // Let's protect _Position: _Position might be changed by Read()
            lock (_lock)
            {
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        {
                            _position = dlibMove;
                            break;
                        }
                    case SeekOrigin.Current:
                        {
                            _position += dlibMove;
                            break;
                        }
                    case SeekOrigin.End:
                        {
                            _position = _cache.Length + dlibMove;
                            break;
                        }
                }
            }

            if (IntPtr.Zero != plibNewPosition)
                Marshal.WriteInt64(plibNewPosition, _position);
        }

        public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
        {
            pstatstg = new System.Runtime.InteropServices.ComTypes.STATSTG();
            pstatstg.cbSize = _cache.Length;
        }

        public void Commit(int grfCommitFlags)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void LockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Revert()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void SetSize(long libNewSize)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void UnlockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
