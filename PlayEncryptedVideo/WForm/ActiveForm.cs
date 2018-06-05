using log4net;
using PlayEncryptedVideo.Class;
using System;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace PlayEncryptedVideo.WForm
{
    public partial class ActiveForm : Form
    {
        private ILog log;
        private string Type;
        private string frmName;
        private string Key;
        public CourseActive course;
        public event EventHandler ActiveCourseEvent;
        public event EventHandler ActiveContentEvent;

        public ActiveForm(ILog log, string Key,string Type, string frmName)
        {
            InitializeComponent();
            this.frmName = frmName;
            this.Type = Type;
            this.Key = Key;
            this.log = log;
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtKey.Text == "")
                {
                    MessageBox.Show("Vui lòng điền mã key trước khi kích hoạt.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string keyDecrypt = VDCSDK.App.DecryptKey(txtKey.Text, true);

                string sub = null;
                if(Key.Length == 3)
                {
                    sub = "PEVCS" + Key;
                }
                else
                {
                    sub = Key;
                }

                int time = Common.CheckAppKey(sub, keyDecrypt);

                string prdKeymd5 = null;

                using (MD5 md5Hash = MD5.Create())
                {
                    prdKeymd5 = VDCSDK.App.GetMd5Hash(md5Hash, sub);
                }

                if(Type == "Upgrade" || Type == "Expired")
                {
                    string value = VDCSDK.App.GetValueOfKey(prdKeymd5);
                    if (value != null)
                    {
                        string encryted1 = VDCSDK.App.DecryptKey(value, true);
                        string activedKey = encryted1.Substring(0, encryted1.Length - 14);
                        if (String.Equals(activedKey, txtKey.Text))
                        {
                            throw new Exception();
                        }
                    }
                }

                DateTime endDate = DateTime.Now;

                if (time != 0)
                {
                    endDate = DateTime.Now.AddDays(time);
                }
                else
                {
                    endDate = DateTime.Now.AddYears(5);
                }
     
                string end = endDate.ToString("yyyyMMddHHmmss");
                string keyEncryt = VDCSDK.App.EncryptKey(txtKey.Text + end, true);

                VDCSDK.App.RegisterKey(prdKeymd5, keyEncryt);
                course = new CourseActive();

                if(time != 0)
                {
                    course.typeKey = "Upgrade";
                    course.linkText = "Upgrade";
                    course.linkVisiable = true;
                }
                else
                {
                    course.typeKey = "Actived";
                    course.linkVisiable = false;
                }

                if(frmName  == "SelectCourseForm")
                {
                    course.btnVisiable = true;
                    ActiveCourseEvent(this, EventArgs.Empty);
                }
                else
                {
                    ActiveContentEvent(this, EventArgs.Empty);
                }
                
                this.Dispose();
            }
            catch(Exception ex)
            {
                this.log.Error(ex.Message);
                DialogResult result = MessageBox.Show("Mã kích hoạt không đúng.\r\nVui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                if(result == DialogResult.Cancel)
                {
                    this.Dispose();
                }
            }
        }

        private void btnGetKey_Click(object sender, EventArgs e)
        {
            string uuId = Common.getUUID().Replace("-","");
            GetKeyForm frm = new GetKeyForm(Key,uuId, Type);
            frm.ShowDialog();
            this.Close();
            frm.Dispose();

        }

        private void ActiveForm_Load(object sender, EventArgs e)
        {
            switch(Type)
            {
                case "Upgrade":
                    btnActive.Text = "Upgrade";
                    break;
                default:
                    btnActive.Text = "Active";
                    break;
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
