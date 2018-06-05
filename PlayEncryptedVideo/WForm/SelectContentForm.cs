using log4net;
using PlayEncryptedVideo.Class;
using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace PlayEncryptedVideo.WForm
{
    public partial class SelectContentForm : Form
    {
        private ILog log;
        private string path;
        private string courseKey;
        private string courseName;
        private string typeKey;
        private bool trialFlg;

        public SelectContentForm(ILog log,string courseKey, string courseName, string typeKey)
        {
            InitializeComponent();
            this.courseKey = courseKey;
            this.courseName = courseName;
            this.typeKey = typeKey;
            this.log = log;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string contentKey = (string)btn.Tag;
            string contentType = btn.AccessibleName;
            if (contentType != "Actived")
            {
                if(contentType == "Upgrade")
                {
                    DialogResult result = MessageBox.Show("Bạn đang dùng thử khóa học.\r\nBạn muốn kích hoạt ngay không?", "Xác nhận", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Information);
                    if(result == DialogResult.Abort)
                    {
                        return;
                    }
                    else if(result == DialogResult.Retry)
                    {
                        ShowActiveForm(contentKey, contentType);
                    }
                    else if (result == DialogResult.Ignore)
                    {
                        ShowMainForm(contentKey, contentType);
                    }
                }
                else
                {
                    ShowActiveForm(contentKey, contentType);
                }
            }
            else
            {
                ShowMainForm(contentKey, contentType);
            }
        }

        private void ShowActiveForm(string contentKey, string contentType)
        {
            ActiveForm frm = new ActiveForm(log, contentKey, contentType, this.Name);
            frm.ActiveContentEvent += (sender, e) => this.ActiveContent(contentKey, frm.course,0);
            frm.ShowDialog();
            frm.Dispose();
        }

        private void ShowMainForm(string contentName, string contentType)
        {
            MainForm frm = new MainForm(log, courseKey, courseName, contentName, contentType);
            this.Hide();
            frm.ShowDialog();
        }

        private void SelectContentForm_Load(object sender, EventArgs e)
        {
            this.Text = courseName;
            CourseActive course = new CourseActive();
            path = Manager.path + @"Files\" + courseKey;

            try
            {
                string prdKeymd5 = null;
                string value = null;

                using (MD5 md5Hash = MD5.Create())
                {
                    prdKeymd5 = VDCSDK.App.GetMd5Hash(md5Hash, "PEVCSALL");
                }

                value = VDCSDK.App.GetValueOfKey(prdKeymd5);
                if (value != null)
                {
                    try
                    {
                        string encryted1 = VDCSDK.App.DecryptKey(value, true);
                        string encryted2 = VDCSDK.App.DecryptKey(encryted1.Substring(0, encryted1.Length - 14), true);

                        Common.CheckAppKey("PEVCSALL", encryted2);
                        string timeActive = encryted1.Substring(encryted1.Length - 14);
                        var activeDate = DateTime.ParseExact(timeActive, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                        int result = DateTime.Compare(activeDate, DateTime.Now);
                        if(result < 0)
                        {
                            course.typeKey = "Expired";
                        }
                        else
                        {
                            course.typeKey = "Actived";
                        }

                        ActiveContent("PEVCSALL", course,0);
                        return;
                    }
                    catch
                    {
                        // do not nothing
                    }
                }

                using (MD5 md5Hash = MD5.Create())
                {
                    prdKeymd5 = VDCSDK.App.GetMd5Hash(md5Hash, "PEVCS" + courseKey);
                }

                value = VDCSDK.App.GetValueOfKey(prdKeymd5);
                if (value != null)
                {
                    try
                    {
                        string encryted1 = VDCSDK.App.DecryptKey(value, true);
                        string encryted2 = VDCSDK.App.DecryptKey(encryted1.Substring(0, encryted1.Length - 14), true);

                        int time = Common.CheckAppKey("PEVCS" + courseKey, encryted2);

                        if (time != 0)
                        {
                            string timeActive = encryted1.Substring(encryted1.Length - 14);
                            var activeDate = DateTime.ParseExact(timeActive, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                            int result = DateTime.Compare(activeDate, DateTime.Now);
                            if (result > 0)
                            {
                                trialFlg = true;
                            }
                        }
                        else
                        {
                            course.typeKey = "Actived";
                            ActiveContent("PEVCS" + courseKey, course,0);
                            return;
                        }
                    }
                    catch
                    {
                        // do not nothing
                    }
                }

                int count = 0;
                int index = 0;
                foreach (string folderName in Directory.GetDirectories(path))
                {
                    string contentKey = folderName.Remove(0, folderName.LastIndexOf('\\') + 1);
                    if (contentKey.Substring(0, 3) == courseKey)
                    {
                        course = new CourseActive();
                        course.typeKey = "UnActive";

                        using (MD5 md5Hash = MD5.Create())
                        {
                            prdKeymd5 = VDCSDK.App.GetMd5Hash(md5Hash, contentKey);
                        }

                        value = VDCSDK.App.GetValueOfKey(prdKeymd5);
                        if (value != null)
                        {
                            string encryted1 = VDCSDK.App.DecryptKey(value, true);
                            string encryted2 = VDCSDK.App.DecryptKey(encryted1.Substring(0, encryted1.Length - 14), true);

                            int time = Common.CheckAppKey(contentKey, encryted2);

                            if (time != 0)
                            {
                                string timeActive = encryted1.Substring(encryted1.Length - 14);
                                var activeDate = DateTime.ParseExact(timeActive, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                                int result = DateTime.Compare(activeDate, DateTime.Now);
                                if (result < 0)
                                {
                                    course.typeKey = "Expired";
                                }
                                else
                                {
                                    course.typeKey = "Upgrade";
                                }
                            }
                            else
                            {
                                course.typeKey = "Actived";
                            }
                        }

                        if (typeKey == "Upgrade" && count < 2 && trialFlg)
                        {
                            course.typeKey = "Upgrade";
                        }

                        ActiveContent(contentKey, course, index);
                        index++;
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                this.log.Error(ex.Message);
            }
            finally
            {
                course = null;
            }
        }

        private void ActiveContent(string prdKeySub, CourseActive course,int index)
        {
            if (prdKeySub == "PEVCSALL" || (prdKeySub == "PEVCS" + courseKey && course.typeKey != "Upgrade"))
            {
                index = 0;
                foreach (string folderName in Directory.GetDirectories(path))
                {
                    string contentKey = folderName.Remove(0, folderName.LastIndexOf('\\') + 1);
                    if (contentKey.Substring(0, 3) == courseKey)
                    {
                        foreach (Control control in this.Controls)
                        {
                            if (control.GetType() == typeof(Button))
                            {
                                Button btn = (Button)control;
                                if (btn.TabIndex == index)
                                {
                                    btn.Visible = true;

                                    if (File.Exists(String.Format(@"{0}\Images\Courses\{1}\{2}.JPG", Directory.GetCurrentDirectory(), courseKey, contentKey)))
                                    {
                                        btn.Image = Image.FromFile(String.Format(@"{0}\Images\Courses\{1}\{2}.JPG", Directory.GetCurrentDirectory(), courseKey, contentKey));
                                    }
                                    else
                                    {
                                        btn.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Images\Courses\noImage.JPG");
                                    }

                                    btn.Tag = contentKey;
                                    btn.AccessibleName = course.typeKey;
                                }
                            }
                        }

                        index++;
                    }
                }
            }
            else
            {
                foreach (Control control in this.Controls)
                {
                    if (control.GetType() == typeof(Button))
                    {
                        Button btn = (Button)control;
                        if(btn.TabIndex == index)
                        {
                            btn.Visible = true;

                            if (File.Exists(String.Format(@"{0}\Images\Courses\{1}\{2}.JPG", Directory.GetCurrentDirectory(), courseKey, prdKeySub)))
                            {
                                btn.Image = Image.FromFile(String.Format(@"{0}\Images\Courses\{1}\{2}.JPG", Directory.GetCurrentDirectory(), courseKey, prdKeySub));
                            }
                            else
                            {
                                btn.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Images\Courses\noImage.JPG");
                            }

                            btn.Tag = prdKeySub;
                            btn.AccessibleName = course.typeKey;
                        }
                    }
                }
            }
        }

        private void SelectContentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SelectCourseForm frm = new SelectCourseForm(log);
            frm.Show();
        }

        private void SelectContentForm_Shown(object sender, EventArgs e)
        {
            //Scroll to Top
            this.AutoScrollPosition = new Point(0, 0);
            this.Refresh();
        }
    }
}
