using PlayEncryptedVideo.Class;
using System;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace PlayEncryptedVideo.WForm
{
    public partial class GetKeyForm : Form
    {
        private string Type;
        private string Key = null;
        private string uuId = null;
        private string hash = null;

        public GetKeyForm(string Key, string uuId, string Type)
        {
            InitializeComponent();
            this.Type = Type;
            this.Key = Key;
            this.uuId = uuId;
        }

        private void GetKeyForm_Load(object sender, EventArgs e)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                hash = VDCSDK.App.GetMd5Hash(md5Hash, uuId);
            }
            txtRequestCode.Text = hash;

            if(Type != "UnActive" || Key == "PEVCSALL")
            {
                chkTrial.Visible = false;
                lbTrial.Visible = false;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtEmail.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập địa chỉ email của bạn.", "Cảnh báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }

                if (!Common.IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Địa chỉ email không đúng.\r\nVui lòng nhập địa chỉ email của bạn.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string keyType = "";

                if (Key == "PEVCSALL")
                {
                    keyType = "Quyền truy cập tất cả";
                }
                else
                {
                    if(Key.Length == 3)
                    {
                        keyType = "Mã toàn khóa học";
                        if (chkTrial.Checked)
                        {
                            keyType = "Dùng thử toàn khóa học";
                        }
                    }
                    else
                    {
                        keyType = "Mã từng khóa học";
                        if (chkTrial.Checked)
                        {
                            keyType = "Dùng thử từng khóa học";
                        }
                    }
                }

                string subject = "[ActiveKey]";
                string body = String.Format("Mã yêu cầu: {0}\r\nUserName: {1}\r\nEmail: {2}\r\nMã sản phẩm: {3}\r\nMã uuid: {4}\r\nLoại key: {5}",
                    hash,txtUsername.Text,txtEmail.Text, Key, uuId, keyType);

                if (VDCSDK.App.SendMail(null,subject, body))
                {
                    MessageBox.Show("Yêu cầu bạn đã được gửi. Vui lòng chờ phản hồi.", "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xảy ra trong khi gửi yêu cầu.\r\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }           
        }
    }
}
