using System;
using System.IO;
using System.Windows.Forms;

namespace PlayEncryptedVideo.Class
{
    class Manager
    {
        public static string path = null;
        public static void AppSDK_Init()
        {
            try
            {
                //VDCSDK.App.AppSDK_Init("OKwf97RMC2k6+0JSSSvP0jP+774+zyDUp6L92AoCa74tpnMeF3726vtizvrfs757epzvYNho3C7gidEk1JwQ5g==");
                VDCSDK.App.AppSDK_Init("vtut.player@gmail.com", "VtutPlayer386", "dinhchung.utc.k49@gmail.com");
                BoxedAppSDK.NativeMethods.BoxedAppSDK_SetContext("468ded90-2737-4e50-9962-34e61c340ee4");
                BoxedAppSDK.NativeMethods.BoxedAppSDK_Init();

                path = VDCSDK.App.GetValueOfKey("Path");
                if (path == null || !Directory.Exists(path + @"\Files"))
                {
                    FolderBrowserDialog fd = new FolderBrowserDialog();
                    fd.Description = "Please Insert DVD Or choose folder on the hard disk";
                    fd.ShowNewFolderButton = false;
                    DialogResult result = fd.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        path = fd.SelectedPath;
                        if (!Directory.Exists(path + @"\Files"))
                        {
                            throw new Exception("Thư mục chương trình không đúng.");
                        }
                        //Common.AddUpdateAppSettings("Path", path);
                        if (path.Substring(path.Length - 1) != @"\")
                        {
                            path = path + @"\";
                        }
                        VDCSDK.App.RegisterKey("Path", path);
                    }
                    else
                    {
                        throw new Exception("Không nhận thấy thư mục chương trình.");
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
