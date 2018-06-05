using log4net;
using PlayEncryptedVideo.Class;
using System;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace PlayEncryptedVideo.WForm
{
    public partial class SupportForm : Form
    {
        private ILog log;
        private string mailContact = null;
        public SupportForm(ILog log,string mailContact)
        {
            InitializeComponent();
            this.log = log;
            this.mailContact = mailContact;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmail.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập địa chỉ email của bạn.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!Common.IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Địa chỉ email không đúng.\r\nVui lòng nhập địa chỉ email của bạn.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtRequest.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập nội dung cần hỗ trợ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string uuId = Common.getUUID().Replace("-", "");

                using (MD5 md5Hash = MD5.Create())
                {
                    uuId = VDCSDK.App.GetMd5Hash(md5Hash, uuId);
                }

                string subject = "[Support]";
                string body = String.Format("Mã yêu cầu: {0}\r\nUserName: {1}\r\nEmail: {2}\r\nNội dung: {3}",
                    uuId,txtName.Text, txtEmail.Text,txtRequest.Text);

                if (VDCSDK.App.SendMail(mailContact,subject, body))
                {
                    MessageBox.Show("Cảm ơn bạn đã sử dụng phần mềm.\r\nYêu cầu của bạn đã được gửi. Vui lòng chờ phản hồi.", "Gửi thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                this.log.Error(ex.Message);
                MessageBox.Show("Lỗi xảy ra trong khi gửi yêu cầu.\r\nVui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           if(txtRequest.Text != "")
           {
                DialogResult result = MessageBox.Show("Yêu cầu của bạn sẽ không được gửi đi.\r\nBạn muốn hủy?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(result == DialogResult.OK)
                {
                    this.Dispose();
                }
            }
            else
            {
                this.Dispose();
            }
        }
    }
}
