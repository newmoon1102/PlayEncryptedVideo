using System;
using System.Configuration;
using System.Diagnostics;
using System.Management;
using System.Text.RegularExpressions;

namespace PlayEncryptedVideo.Class
{
    struct KeyGen
    {
        public static byte[] Key = { 0x9b, 0x67, 0x14, 0xc, 0xb8, 0x7e, 0xf0, 0x4b, 0x6e, 0xd, 0x88, 0x7a, 0xf1, 0xbb, 0x33, 0xc1, 0xc1, 0x12, 0xa3, 0x1f, 0xca, 0x2d, 0xdc, 0x54 };
        public static byte[] IV = { 0x3d, 0x12, 0xe9, 0x8c, 0xea, 0x24, 0x61, 0xf0 };
    }

    public class CourseActive
    {
        public bool btnVisiable;
        public bool linkVisiable;
        public string typeKey;
        public string linkText;
        public CourseActive()
        {
            btnVisiable = false;
            linkVisiable = true;
            typeKey = "";
            linkText = "Active Now";
        }
    }

    public class Common
    {
        public static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                throw;
            }
        }

        /// <summary>Get CPU ID</summary>
        /// <returns>cpuid</returns>
        public static String getCPUID()
        {
            String cpuid = "";
            try
            {
                ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select ProcessorID From Win32_processor");
                ManagementObjectCollection mbsList = mbs.Get();

                foreach (ManagementObject mo in mbsList)
                {
                    cpuid = mo["ProcessorID"].ToString().Trim();
                    break;
                }
                return cpuid;
            }
            catch (Exception) { return cpuid; }
        }

        /// <summary>Get Mother Board ID</summary>
        /// <returns>MotherBoardID</returns>
        public static String getMotherBoardID()
        {
            String serial = "";
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard");
                ManagementObjectCollection moc = mos.Get();

                foreach (ManagementObject mo in moc)
                {
                    serial = mo["SerialNumber"].ToString().Trim();
                    break;
                }
                return serial;
            }
            catch (Exception) { return serial; }
        }

        /// <summary>Get UUID</summary>
        /// <returns>UUID</returns>
        public static String getUUID()
        {
            String uuid = "";
            try
            {
                ManagementClass mc = new ManagementClass("Win32_ComputerSystemProduct");
                ManagementObjectCollection moc = mc.GetInstances();

                foreach (ManagementObject mo in moc)
                {
                    uuid = mo.Properties["UUID"].Value.ToString().Trim();
                    break;
                }
                return uuid;
            }
            catch (Exception) { return uuid; }
        }

        static Regex validEmailRegex = CreateValidEmailRegex();

        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        public static bool IsValidEmail(string email)
        {
            bool isValid = validEmailRegex.IsMatch(email);
            return isValid;
        }

        public static int CheckAppKey(string keyCourse, string keyActive)
        {
            try
            {
                if (keyActive.Length != 45)
                {
                    throw new Exception();
                }

                string prdKeySub = keyActive.Substring(0, 8);
                string uuId = keyActive.Substring(8, 32);
                string typeKey = keyActive.Substring(40, 3);
                string uuId_pc = Common.getUUID().Replace("-", "");

                // Active all course
                if (keyCourse == "PEVCSALL")
                {
                    string uuIdSub = uuId.Substring(16, 16) + uuId.Substring(0, 16);
                    if (!String.Equals(keyCourse, prdKeySub) || !String.Equals(uuId_pc, uuIdSub))
                    {
                        throw new Exception();
                    }
                }
                else //Active course
                {
                    if (!String.Equals(keyCourse, prdKeySub) || !String.Equals(uuId_pc, uuId))
                    {
                        throw new Exception();
                    }

                    if (String.Equals(typeKey, "TRL"))
                    {
                        return int.Parse(keyActive.Substring(43, 2));
                    }
                }

                return 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static void OpenSite()
        {
            Process myProcess = new Process();
            try
            {
                // true is the default, but it is important not to set it to false
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.FileName = "http://meslab.org/";
                myProcess.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
