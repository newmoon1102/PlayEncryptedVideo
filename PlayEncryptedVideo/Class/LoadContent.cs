using log4net;
using System;
using System.Collections.Generic;
using System.Xml;

namespace PlayEncryptedVideo.Class
{
    public class LoadContent
    {
        private ILog log;
        private VideoNode[] videoList = null;
        public Dictionary<string, VideoNode[]> dict;

        public LoadContent(ILog log, string XMLFilePath)
        {
            this.log = log;
            try
            {
                // オプションの読込
                LoadContentXml(XMLFilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ~LoadContent()
        {
            videoList = null;
        }

        private void LoadContentXml(string XMLFilePath)
        {
            XmlDocument xml = null;

            try
            {
                dict = new Dictionary<string, VideoNode[]>();
                xml = new XmlDocument();
                xml.Load(XMLFilePath);

                int index = 0;
                foreach (XmlElement parent in xml.DocumentElement)
                {
                    // 送信先リスト取得
                    if (parent.Name == "Content")
                    {
                        GetContentList(parent, index++);
                    }
                }
            }
            catch (Exception ex)
            {
                this.log.Error(ex.Message);
                throw;
            }
            finally
            {
                xml = null;
            }
        }

        private void GetContentList(XmlElement element, int index)
        {
            try
            {
                string title = null;
                XmlNodeList tl = element.GetElementsByTagName("title");
                foreach (XmlElement sub in tl)
                {
                    if (sub.Name == "title")
                    {
                        title = sub.InnerText;
                    }
                }

                XmlNodeList video = element.GetElementsByTagName("Video");
                if(video.Count > 0)
                {
                    videoList = new VideoNode[video.Count];
                }

                int index1 = 0;
                foreach (XmlElement sub in video)
                {
                    if (sub.Name == "Video")
                    {
                        GetContentList1(sub, index1++);
                    }
                }
                if (title != null)
                {
                    dict.Add(title, videoList);
                }
            }
            catch (Exception ex)
            {
                this.log.Error(ex.Message);
                throw;
            }
        }

        private void GetContentList1(XmlElement element, int index)
        {
            try
            {
                foreach (XmlElement sub in element)
                {
                    if (sub.Name == "name")
                    {
                        videoList[index].name = sub.InnerText;
                    }
                    if (sub.Name == "practice")
                    {
                        videoList[index].practice = sub.InnerText;
                    }
                    if (sub.Name == "titleDesc")
                    {
                        videoList[index].titleDesc = sub.InnerText;
                    }
                    if (sub.Name == "line1")
                    {
                        videoList[index].line1 = sub.InnerText;
                    }
                    if (sub.Name == "line2")
                    {
                        videoList[index].line2 = sub.InnerText;
                    }
                }
            }
            catch (Exception ex)
            {
                this.log.Error(ex.Message);
                throw;
            }
        }
    }
    public struct VideoNode
    {
        public int num;//num of videos in chapter
        public int stt; //index of video in chapter
        public string chapter;//chapter name
        public string name;//display name of video
        public string practice;//Enable or Disable Practice Exercise button
        public string titleDesc;//discription title
        public string line1;//discription line 1
        public string line2;//discription line 2
    }
}
