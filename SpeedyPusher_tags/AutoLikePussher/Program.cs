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
