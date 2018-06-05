using log4net;
using PlayEncryptedVideo.Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace PlayEncryptedVideo.WForm
{
    public partial class ContentForm : Form
    {
        private ILog log;
        private string typeKey;
        private string courseKey;
        private string contentKey;
        private LoadContent content = null;
        public static TreeNode tn;
        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();
        private int LastNodeIndex = 0;
        private string LastSearchText;
        public VideoNode videoplay;
        public event EventHandler PlayVideoEvent;
        
        public ContentForm(ILog log, string courseKey,string contentKey, LoadContent content,string typeKey)
        {
            InitializeComponent();
            this.content = content;
            this.courseKey = courseKey;
            this.contentKey = contentKey;
            this.typeKey = typeKey;
            this.log = log;
        }

        private void ContentForm_Load(object sender, EventArgs e)
        {
            try
            {
                imageList.ImageSize = new Size(16, 16);
                Image image = Properties.Resources.icon_book_close;
                imageList.Images.Add(image);
                image = Properties.Resources.icon_book_open;
                imageList.Images.Add(image);
                image = Properties.Resources.icon_video;
                imageList.Images.Add(image);
                contentView.ImageList = imageList;

                foreach (string key in content.dict.Keys)
                {
                    tn = contentView.Nodes.Add(key, key, 0, 1); // ルート・ノードの追加

                    VideoNode[] videoList = content.dict[key];

                    foreach (VideoNode video in videoList)
                    {
                        using (MD5 md5Hash = MD5.Create())
                        {
                            tn.Nodes.Add(VDCSDK.App.GetMd5Hash(md5Hash,video.name), video.name, 2, 2);
                        }                       
                    }

                    if (typeKey == "Upgrade") break;
                }
            }
            catch(Exception ex)
            {
                this.log.Error(ex.Message);
            }
        }

        private void contentView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // すでに追加されている子ノードを削除
            e.Node.Nodes.Clear();
            if (!content.dict.ContainsKey(e.Node.Name)) return;

            VideoNode[] videoList = content.dict[e.Node.Name];

            foreach (VideoNode video in videoList)
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    e.Node.Nodes.Add(VDCSDK.App.GetMd5Hash(md5Hash, video.name), video.name, 2, 2);
                }
            }

            e.Node.Expand(); // ルート・ノードの展開（子ノードの表示）
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = this.txtSearch.Text;
            if (String.IsNullOrEmpty(searchText))
            {
                return;
            };

            if (LastSearchText != searchText)
            {
                //It's a new Search
                CurrentNodeMatches.Clear();
                LastSearchText = searchText;
                LastNodeIndex = 0;
                SearchNodes(searchText, contentView.Nodes[0]);
            }

            if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
            {
                TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
                LastNodeIndex++;
                this.contentView.SelectedNode = selectedNode;
                this.contentView.SelectedNode.Expand();
                this.contentView.Select();

            }
        }

        private void SearchNodes(string SearchText, TreeNode StartNode)
        {
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    CurrentNodeMatches.Add(StartNode);
                };
                if (StartNode.Nodes.Count != 0)
                {
                    SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search 
                };
                StartNode = StartNode.NextNode;
            };

        }

        private void contentView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                string parentNode = e.Node.Parent.Text;
                

                if (!content.dict.ContainsKey(parentNode)) return;

                VideoNode[] videoList = content.dict[parentNode];

                foreach (VideoNode video in videoList)
                {
                    using (MD5 md5Hash = MD5.Create())
                    {
                        if (VDCSDK.App.GetMd5Hash(md5Hash, video.name) == e.Node.Name)
                        {
                            videoplay = video;
                            videoplay.name = VDCSDK.App.GetMd5Hash(md5Hash, video.name);
                            videoplay.chapter = parentNode;
                            videoplay.num = e.Node.Parent.GetNodeCount(true);
                            videoplay.stt = e.Node.Index;
                            break;
                        }
                    }
                }

                if (!File.Exists(String.Format(@"{0}Files\{1}\{2}\Tutorial Videos\{3}", Manager.path, courseKey,contentKey, videoplay.name)))
                {
                    MessageBox.Show("Video không tồn tại.Vui lòng liên hệ với support để được hỗ trợ.","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }

                PlayVideoEvent(this, EventArgs.Empty);
            }
            catch { }
        }

        private void contentView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                string parentNode = e.Node.Parent.Text;

                if (!content.dict.ContainsKey(parentNode)) return;

                VideoNode[] videoList = content.dict[parentNode];

                foreach (VideoNode video in videoList)
                {
                    using (MD5 md5Hash = MD5.Create())
                    {
                        if (VDCSDK.App.GetMd5Hash(md5Hash, video.name) == e.Node.Name)
                        {
                            txtTitle.Text = video.titleDesc;
                            txtDesc.Text = String.Format("{0}\r\n{1}", video.line1, video.line2);
                            break;
                        }
                    }
                }  
            }
            catch { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
