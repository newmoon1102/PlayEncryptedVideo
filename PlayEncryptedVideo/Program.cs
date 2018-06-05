using log4net;
using LoggingConfiguration;
using PlayEncryptedVideo.Class;
using PlayEncryptedVideo.WForm;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace PlayEncryptedVideo
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            LogConfiguration.SetupLog4net();
            ILog log = LogManager.GetLogger(typeof(Program));
            string processname = Process.GetCurrentProcess().ProcessName;

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                using (LoadForm frm = new LoadForm())
                {
                    frm.Show();
                    frm.Refresh();

                    Manager.AppSDK_Init();
                }

                Application.Run(new SelectCourseForm(log));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.FatalFormat("Application Finish with Error.Error Message: {0}", ex.Message);
            }
            finally
            {
                foreach (var process in Process.GetProcessesByName(processname))
                {
                    process.Kill();
                }
            }
        }
    }
}
