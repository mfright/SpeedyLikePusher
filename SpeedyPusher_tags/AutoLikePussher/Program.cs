using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeedyLikeSender
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // ブラウザコンポーネントのバージョンを変更するレジストリ制御用
            const string FEATURE_BROWSER_EMULATION = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(FEATURE_BROWSER_EMULATION);
            string process_name = System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe";
            string process_dbg_name = System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".vshost.exe";

            // webBrowserコンポーネントで使用するIEのバージョンを上げる。
            regkey.SetValue(process_name, 11001, Microsoft.Win32.RegistryValueKind.DWord);
            regkey.SetValue(process_dbg_name, 11001, Microsoft.Win32.RegistryValueKind.DWord);



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //オートメーションオプションが有効なとき・無効な時
            bool automation = false;
            if (args != null)
            {
                if (args.Length > 0 && args[0].Equals("-a"))
                {
                    automation = true;
                    Application.Run(new frmMain(true));
                }
            }

            if (!automation)
            {
                Application.Run(new frmMain(false));
            }
        }
    }
}
