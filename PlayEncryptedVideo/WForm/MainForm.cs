using log4net;
using Microsoft.Win32.SafeHandles;
using PlayEncryptedVideo.Class;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;

namespace PlayEncryptedVideo.WForm
{
    public partial class MainForm : Form
    {
        private ILog log;
        private string courseKey;
        private string courseName;
        private string contentKey;
        private string typeKey;
        private static LoadContent content = null;
        private static bool closeForm = false;
        private static bool autoPlay = false;
        private static string pdfFile = null;
        private ContentForm frm;
        public MainForm(ILog log, string courseKey,string courseName, string contentKey, string typeKey)
        {
            InitializeComponent();
            this.courseKey = courseKey;
            this.courseName = courseName;
            this.contentKey = contentKey;
            this.typeKey = typeKey;
            this.log = log;
        }
        ~MainForm()
        {
            content = null;
        }

        private void btnContent_Click(object sender, EventArgs e)
        {
            try
            {
                frm = new ContentForm(log, courseKey, contentKey, content,typeKey);
                frm.PlayVideoEvent += (sender1, e1) => this.PlayVideo(frm.videoplay);
                frm.ShowDialog();
                frm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PlayEncryptedVideo(string path)
        {
            using (SafeFileHandle fileHandle =
               new SafeFileHandle(
                   BoxedAppSDK.NativeMethods.BoxedAppSDK_CreateVirtualFileBasedOnIStream(
                       @"video.avi", // name of the pseudo file
                       BoxedAppSDK.NativeMethods.EFileAccess.GenericWrite,
                       BoxedAppSDK.NativeMethods.EFileShare.Read,
                       IntPtr.Zero,
                       BoxedAppSDK.NativeMethods.ECreationDisposition.New,
                       BoxedAppSDK.NativeMethods.EFileAttributes.Normal,
                       IntPtr.Zero,
                       new VirtualFileStream(path)
                   ),
                   true
               )
            )
            {
                // Close SafeFileHandle
            }
            axWMP.URL = @"video.avi";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = courseName;
            closeForm = false;
            axWMP.Visible = false;
            panelBottom.Visible = false;
            //Load content
            try
            {
                string path = Manager.path + @"Files\";
                content = new LoadContent(log, String.Format(@"{0}\{1}\{2}\Content\ContentXml.xml", path, courseKey, contentKey));
            }
            catch(Exception ex)
            {
                btnContent.Enabled = false;
                //panelMain.BackgroundImage = Image.FromFile(String.Format(@"{0}\Images\Courses\Themes\404.JPG", Directory.GetCurrentDirectory()), true);
                //panelMain.BackgroundImageLayout = ImageLayout.Stretch;
                this.log.Error(String.Format("Content of [{0}] not exist. {1}", contentKey, ex.Message));
            }
            //Load theme
            try
            {
                panelMain.BackgroundImage = Image.FromFile(String.Format(@"{0}\Images\Courses\Themes\{1}.JPG", Directory.GetCurrentDirectory(), contentKey), true);
                panelMain.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                panelMain.BackgroundImage = Image.FromFile(String.Format(@"{0}\Images\Courses\Themes\405.JPG", Directory.GetCurrentDirectory()), true);
                panelMain.BackgroundImageLayout = ImageLayout.Stretch;
                this.log.Error(String.Format("Theme  of [{0}] not exist. [{0}]", contentKey, ex.Message));
            }

            int widthScreen = SystemInformation.VirtualScreen.Width;
            int heightScreen = SystemInformation.VirtualScreen.Height;
            if (widthScreen < this.Width || heightScreen < this.Height)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void btnWebsite_Click(object sender, EventArgs e)
        {
            try
            {
                axWMP.Ctlcontrols.pause();
                Common.OpenSite();
            }
            catch (Exception ex)
            {
                this.log.Error(ex.Message);
            }
        }

        private void btnAutoplay_Click(object sender, EventArgs e)
        {
            if (autoPlay)
            {
                btnAutoplay.Image = Properties.Resources.auto_off;
                autoPlay = false;
            }
            else
            {
                btnAutoplay.Image = Properties.Resources.auto_on;
                autoPlay = true;
            }
        }

        private void PlayVideo(VideoNode video)
        {
            try
            {
                frm.Close();
                string path = Manager.path;

                if (!Directory.Exists(String.Format(@"{0}Files\{1}\{2}\Tutorial Videos", path, courseKey, contentKey)))
                {
                    DialogResult result = folderBrowserDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        path = folderBrowserDialog.SelectedPath;
                        Common.AddUpdateAppSettings("Path", path);
                    }
                    else
                    {
                        return;
                    }
                }

                string FilePath = String.Format(@"{0}Files\{1}\{2}\Tutorial Videos\{3}", path, courseKey, contentKey, video.name);

                if (!File.Exists(FilePath))
                {
                    MessageBox.Show(String.Format("Lesson {0} Video không tồn tại.Vui lòng liên hệ với support để được hỗ trợ.", video.stt + 1), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                axWMP.Visible = true;
                axWMP.Scale(new SizeF(1280, 720));
                lbTitle.Text = video.titleDesc;
                lbDescLine1.Text = video.line1;
                lbDescLine2.Text = video.line2;
                lbChapter.Text = video.chapter;
                lbLessonOf.Text  = String.Format("Lesson {0} of {1}", video.stt+1,video.num);
                if (!String.IsNullOrEmpty(video.practice))
                {
                    btnpdf.Visible = true;
                    pdfFile = video.practice;
                }
                else
                {
                    btnpdf.Visible = false;
                    pdfFile = null;
                }
                panelBottom.Visible = true;

                PlayEncryptedVideo(FilePath);
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }
 
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                VideoNode videoplay = new VideoNode();

                if (ContentForm.tn.TreeView.SelectedNode.NextNode == null) return;
                ContentForm.tn.TreeView.SelectedNode = ContentForm.tn.TreeView.SelectedNode.NextNode;
                ContentForm.tn.TreeView.Focus();

                string parentNode = ContentForm.tn.TreeView.SelectedNode.Parent.Text;

                if (!content.dict.ContainsKey(parentNode)) return;

                VideoNode[] videoList = content.dict[parentNode];

                foreach (VideoNode video in videoList)
                {
                    using (MD5 md5Hash = MD5.Create())
                    {
                        if (VDCSDK.App.GetMd5Hash(md5Hash, video.name) == ContentForm.tn.TreeView.SelectedNode.Name)
                        {
                            videoplay = video;
                            videoplay.name = VDCSDK.App.GetMd5Hash(md5Hash, video.name);
                            videoplay.chapter = parentNode;
                            videoplay.num = ContentForm.tn.TreeView.SelectedNode.Parent.GetNodeCount(true);
                            videoplay.stt = ContentForm.tn.TreeView.SelectedNode.Index;
                            break;
                        }
                    }
                }

                PlayVideo(videoplay);
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                VideoNode videoplay = new VideoNode();

                if (ContentForm.tn.TreeView.SelectedNode.PrevNode == null) return;
                ContentForm.tn.TreeView.SelectedNode = ContentForm.tn.TreeView.SelectedNode.PrevNode;
                ContentForm.tn.TreeView.Focus();

                string parentNode = ContentForm.tn.TreeView.SelectedNode.Parent.Text;

                if (!content.dict.ContainsKey(parentNode)) return;

                VideoNode[] videoList = content.dict[parentNode];

                foreach (VideoNode video in videoList)
                {
                    using (MD5 md5Hash = MD5.Create())
                    {
                        if (VDCSDK.App.GetMd5Hash(md5Hash, video.name) == ContentForm.tn.TreeView.SelectedNode.Name)
                        {
                            videoplay = video;
                            videoplay.name = VDCSDK.App.GetMd5Hash(md5Hash, video.name);
                            videoplay.chapter = parentNode;
                            videoplay.num = ContentForm.tn.TreeView.SelectedNode.Parent.GetNodeCount(true);
                            videoplay.stt = ContentForm.tn.TreeView.SelectedNode.Index;
                            break;
                        }
                    }
                }

                PlayVideo(videoplay);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void axWMP_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // MediaEnded
            if(e.newState == 8 && autoPlay)
            {
                this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
                btnNext_Click(this, EventArgs.Empty);
            }
            // Ready
            if (e.newState == 10 && autoPlay)
            {
                axWMP.Ctlcontrols.play();
            }
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            try
            {
                axWMP.Ctlcontrols.pause();
                string path = String.Format(@"{0}Files\{1}\{2}\Exercise Files\", Manager.path,courseKey,contentKey);

                string pdfPath = path + pdfFile;
                if (!File.Exists(pdfPath))
                {
                    MessageBox.Show("File không tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ProcessStartInfo s = new ProcessStartInfo(pdfPath);
                p.StartInfo = s;
                p.Start();
            }
            catch(Exception ex)
            {
                this.log.Error(ex.Message);
                if(p != null)
                {
                    p.Dispose();
                    p = null;
                }
            }

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            closeForm = true;
            SelectContentForm frm = new SelectContentForm(log, courseKey, courseName, typeKey);
            this.Dispose();
            frm.Show();  
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutForm frm = new AboutForm();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn đóng chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                string processname = Process.GetCurrentProcess().ProcessName;
                foreach (var process in Process.GetProcessesByName(processname))
                {
                    process.Kill();
                }
            }
        }

        private void btnSupport_Click(object sender, EventArgs e)
        {
            string contact = null;
            string path = String.Format(@"{0}Files\{1}\contact.txt", Manager.path, courseKey);

            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                if (Common.IsValidEmail(text))
                {
                    contact = text;
                }
            }

            SupportForm frm = new SupportForm(log, contact);
            frm.ShowDialog();
            frm.Dispose();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string helpPath = String.Format(@"{0}\help.chm", Directory.GetCurrentDirectory(), contentKey);
            Help.ShowHelp(this, helpPath);
        }

        private void btnCourse_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.InitialDirectory = String.Format(@"{0}Files\{1}\{2}\Course Files\", Manager.path, courseKey, contentKey);
                    dialog.Filter = "All files (*.*)|*.*";
                    dialog.RestoreDirectory = true;
                    dialog.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                this.log.Error(ex.Message);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closeForm)
            {
                SelectContentForm frm = new SelectContentForm(log, courseKey, courseName, typeKey);
                this.Dispose();
                frm.Show();
            }
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            axWMP.Scale(new SizeF(1280, 720));
        }
    }
}
