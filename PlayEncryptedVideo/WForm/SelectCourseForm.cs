using log4net;
using PlayEncryptedVideo.Class;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace PlayEncryptedVideo.WForm
{
    public partial class SelectCourseForm : Form
    {
        private ILog log;
        private string path;

        public SelectCourseForm(ILog log)
        {
            InitializeComponent();
            this.log = log;
        }

        private void SelectCourseForm_Load(object sender, EventArgs e)
        {
            CourseActive course = new CourseActive();
            path = Manager.path + @"Files\";

            try
            {
                course.btnVisiable = true;
                course.linkVisiable = false;

                string prdKeymd5 = null;
                string value = null;

                using (MD5 md5Hash = MD5.Create())
                {
                    prdKeymd5 = VDCSDK.App.GetMd5Hash(md5Hash, "PEVCSALL");
                }

                value = VDCSDK.App.GetValueOfKey(prdKeymd5);
                if(value != null)
                {
                    try
                    {
                        string encryted1 = VDCSDK.App.DecryptKey(value, true);
                        string encryted2 = VDCSDK.App.DecryptKey(encryted1.Substring(0, encryted1.Length - 14),true);

                        Common.CheckAppKey("PEVCSALL", encryted2);
                        string timeActive = encryted1.Substring(encryted1.Length - 14);
                        var activeDate = DateTime.ParseExact(timeActive, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                        int result = DateTime.Compare(activeDate, DateTime.Now);
                        if (result < 0)
                        {
                            course.btnVisiable = false;
                            course.linkVisiable = true;
                            course.linkText = "Active Now";
                            course.typeKey = "";
                        }
                        ActiveCourse("PEVCSALL", course);
                        return;
                    }
                    catch
                    {
                        // do not nothing
                    }
                }

                foreach (string folderName in Directory.GetDirectories(path))
                {
                    string courseKey = folderName.Remove(0, folderName.LastIndexOf('\\') + 1);
                    if (courseKey.Length  == 3)
                    {
                        course = new CourseActive();

                        using (MD5 md5Hash = MD5.Create())
                        {
                            prdKeymd5 = VDCSDK.App.GetMd5Hash(md5Hash, "PEVCS" + courseKey);
                        }

                        value = VDCSDK.App.GetValueOfKey(prdKeymd5);
                        if (value != null)
                        {
                            string encryted1 = VDCSDK.App.DecryptKey(value, true);
                            string encryted2 = VDCSDK.App.DecryptKey(encryted1.Substring(0, encryted1.Length - 14), true);

                            int time = Common.CheckAppKey("PEVCS" + courseKey, encryted2);

                            string timeActive = encryted1.Substring(encryted1.Length - 14);
                            var activeDate = DateTime.ParseExact(timeActive, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                            int result = DateTime.Compare(activeDate, DateTime.Now);
                            if (result < 0)
                            {
                                course.btnVisiable = false;
                                course.linkVisiable = true;
                                course.linkText = "Active Now";
                            }
                            else
                            {
                                course.btnVisiable = true;
                                if (time != 0)
                                {
                                    course.typeKey = "Upgrade";
                                    course.linkVisiable = true;
                                    course.linkText = "Upgrade Now";
                                }
                                else
                                {
                                    course.linkVisiable = false;
                                }
                            }
                        }
                        else
                        {
                            foreach (string folder in Directory.GetDirectories(path + courseKey))
                            {
                                string contentKey = folder.Remove(0, folder.LastIndexOf('\\') + 1);
                                if (contentKey.Substring(0, 3) == courseKey)
                                {
                                    course = new CourseActive();

                                    using (MD5 md5Hash = MD5.Create())
                                    {
                                        prdKeymd5 = VDCSDK.App.GetMd5Hash(md5Hash,contentKey);
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
                                                course.btnVisiable = false;
                                            }
                                            else
                                            {
                                                course.btnVisiable = true;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            course.btnVisiable = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        ActiveCourse(courseKey, course);
                    }
                }
            }
            catch(Exception ex)
            {
                this.log.Error(ex.Message);
            }
            finally
            {
                course = null;
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string courseKey = (string)btn.Tag;
            string courseName = btn.AccessibleDescription;
            string typeKey = null;
            if (btn.AccessibleName == "Upgrade")
            {
                typeKey = "Upgrade";
            }
            ShowSelectContentForm(courseKey, courseName, typeKey);
        }

        private void linkView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lb = (LinkLabel)sender;
            string courseKey = (string)lb.Tag;
            string courseName = lb.AccessibleDescription;
            string typeKey = null;
            if (lb.AccessibleName == "Upgrade")
            {
                typeKey = "Upgrade";
            }
            ShowSelectContentForm(courseKey, courseName, typeKey);
        }

        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lb = (LinkLabel)sender;
            string courseKey = (string)lb.Tag;
            string courseType = "UnActive";
            if (lb.Text == "Active Now" && lb.AccessibleName == "Upgrade")
            {
                courseType = "Expired";
            }

            if (lb.Text == "Upgrade Now")
            {
                courseType = "Upgrade";
            }
            ShowActiveForm(courseKey, courseType);
        }

        private void ShowActiveForm(string courseKey, string courseType)
        {
            ActiveForm frm = new ActiveForm(log, courseKey, courseType, this.Name);
            frm.ActiveCourseEvent += (sender, e) => this.ActiveCourse(courseKey, frm.course);
            frm.ShowDialog();
            frm.Dispose();
        }

        private void ShowSelectContentForm(string courseKey,string courseName, string typeKey)
        {
            SelectContentForm frm = new SelectContentForm(log, courseKey, courseName, typeKey);
            this.Hide();
            frm.ShowDialog();
        }

        private void ShowNewCourse(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Common.OpenSite();
            }
            catch (Exception ex)
            {
                this.log.Error(ex.Message);
            }
        }

        private void ActiveCourse(string courseKeySub, CourseActive course)
        {
            if (courseKeySub == "PEVCSALL")
            {
                btnActiveAll.Visible = false;
            }

            foreach (Control control in this.Controls)
            {
                if(courseKeySub == "PEVCSALL")
                {
                    if (control.GetType() == typeof(Button))
                    {
                        Button btn = (Button)control;
                        btn.Enabled = course.btnVisiable;

                        string courseKey = (string)btn.Tag;
                        //if (String.IsNullOrEmpty(courseKey)) continue;

                        if (File.Exists(String.Format(@"{0}\Images\Courses\{1}\{1}.JPG", Directory.GetCurrentDirectory(), courseKey)))
                        {
                            btn.Image = Image.FromFile(String.Format(@"{0}\Images\Courses\{1}\{1}.JPG", Directory.GetCurrentDirectory(), courseKey));
                        }
                        else
                        {
                            btn.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Images\Courses\noImage.JPG");
                        }                      
                    }
                    if (control.GetType() == typeof(LinkLabel))
                    {
                        control.Visible = course.linkVisiable;
                    }
                }
                else
                {
                    if (control.GetType() == typeof(Button))
                    {
                        Button btn = (Button)control;
                        string courseKey = (string)btn.Tag;

                        if (String.Equals(courseKey, courseKeySub))
                        {
                            btn.Enabled = course.btnVisiable;
                            
                            if (String.IsNullOrEmpty(courseKey)) continue;

                            if (File.Exists(String.Format(@"{0}\Images\Courses\{1}\{1}.JPG", Directory.GetCurrentDirectory(), courseKey)))
                            {
                                btn.Image = Image.FromFile(String.Format(@"{0}\Images\Courses\{1}\{1}.JPG", Directory.GetCurrentDirectory(), courseKey));
                            }
                            else
                            {
                                btn.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Images\Courses\noImage.JPG");
                            }

                            if (course.typeKey == "Upgrade")
                            {
                                btn.AccessibleName = "Upgrade";
                            }
                        }
                    }
                    if (control.GetType() == typeof(LinkLabel))
                    {
                        if (String.Equals(control.Tag, courseKeySub) && control.Name.Contains("Active"))
                        {
                            control.Visible = course.linkVisiable;
                            if (control.Visible)
                            {
                                control.Text = course.linkText;
                                if (course.typeKey == "Upgrade")
                                {
                                    control.AccessibleName = "Upgrade";
                                    control.Text = course.linkText;
                                }  
                            }
                        }

                        if (String.Equals(control.Tag, courseKeySub) && control.Name.Contains("View"))
                        {
                            control.Visible = course.linkVisiable;
                            if (course.typeKey == "Upgrade")
                            {
                                control.AccessibleName = "Upgrade";
                            }
                        }
                    }
                }

                if (control.GetType() == typeof(Button))
                {
                    Button btn = (Button)control;
                    if (btn.Name != "btnActiveAll" && btn.Image == null)
                    {
                        btn.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Images\Courses\noImage.JPG");
                    }
                }
            }
        }

        private void btnActiveAll_Click(object sender, EventArgs e)
        {
            ShowActiveForm("PEVCSALL", "UnActive");
        }

        private void SelectCourseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            string processname = Process.GetCurrentProcess().ProcessName;

            foreach (var process in Process.GetProcessesByName(processname))
            {
                process.Kill();
            }
        }
    }
}
