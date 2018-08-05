using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


using System.Windows;
using System.Windows.Input;

namespace AutoLikePussher
{


    public partial class frmMain : Form
    {
        // ブラウザコンポーネントのバージョンを変更するレジストリ制御用
        Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(FEATURE_BROWSER_EMULATION);
        const string FEATURE_BROWSER_EMULATION = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
        string process_name = System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe";
        string process_dbg_name = System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".vshost.exe";

        //マウス操作のためのDLLをimport
        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void SetCursorPos(int X, int Y);

        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x2;
        private const int MOUSEEVENTF_LEFTUP = 0x4;

        // マウスカーソル座標
        System.Drawing.Point mp;

        // webBrowserスクロール回数
        int countScroll = 0;

        // ダブルクリック回数
        int countDoubleClick = 0;

        // 実行中フラグ
        Boolean running_tag = false;
        Boolean running_timeline = false;

        

        public frmMain()
        {
            InitializeComponent();
        }

        private void timerTag_Tick(object sender, EventArgs e)
        {
            //画像の上にカーソルを動かしダブルクリック
            mp = this.PointToScreen(
                    new System.Drawing.Point(100,450));
            System.Windows.Forms.Cursor.Position = mp;
            System.Threading.Thread.Sleep(10);

            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(100);

            countDoubleClick++;
            System.Console.WriteLine(countDoubleClick);


            //次画像ボタンの上にカーソルを動かしダブルクリック
            mp = this.PointToScreen(
                    new System.Drawing.Point(740, 340));
            System.Windows.Forms.Cursor.Position = mp;

            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // キー操作を拾うイベントハンドラを登録 (ESCを押したときに拾う)
            Application.Idle += new EventHandler(Application_Idle);

            // webBrowserコンポーネントで使用するIEのバージョンを上げる。
            regkey.SetValue(process_name, 11001, Microsoft.Win32.RegistryValueKind.DWord);
            regkey.SetValue(process_dbg_name, 11001, Microsoft.Win32.RegistryValueKind.DWord);
        }


        private void btn_startTag_Click(object sender, EventArgs e)
        {
            if (running_tag)
            {
                stop();             
            
            }
            else
            {
                this.Text = "RUNNING...  Press ESC key to STOP";
                btn_startTag.BackColor = Color.Yellow;
                this.TopMost = true;
                running_tag = true;
                btn_startTag.Text = "Press ESC key to STOP.";

                // 頭へスクロール
                myBrowser.Document.Window.ScrollTo(new System.Drawing.Point(0, countScroll * 400));
                
                timerHashTag.Start();
            } 
            
        }

        
        private void Application_Idle(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Escape) == true)
            {
                stop();
            }
        }

        private void stop()
        {
            this.Text = "Stopped. -- ";
            btn_startTag.BackColor = Color.LightGray;
            btnStartTimeline.BackColor = Color.LightGray;
            this.TopMost = false;
            countScroll = 0;
            running_tag = false;
            running_timeline = false;
            timerHashTag.Stop();
            timerTimeline.Stop();
            btn_startTag.Text = "Open photos page about a hash-tag. \r\nThen press here to START.";
            btnStartTimeline.Text = "Open time-line page.\r\nThen press here to START.";
        }

        private void btnStartTimeline_Click(object sender, EventArgs e)
        {
            if (running_timeline)
            {
                stop();

            }
            else
            {
                this.Text = "RUNNING...  Press ESC key to STOP";   
                btnStartTimeline.BackColor = Color.Yellow;
                this.TopMost = true;
                running_timeline = true;
                btnStartTimeline.Text = "Press ESC key to STOP.";

                timerTimeline.Start();
            } 
        }

        private void timerTimeline_Tick(object sender, EventArgs e)
        {
            // カーソルを移動
            System.Drawing.Point mp = this.PointToScreen(
                    new System.Drawing.Point(85, 300));
            System.Windows.Forms.Cursor.Position = mp;

            // ダブルクリック
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            countDoubleClick++;
            System.Console.WriteLine(countDoubleClick);

            // 下へスクロール
            myBrowser.Document.Window.ScrollTo(new System.Drawing.Point(0, countScroll * 400));
            countScroll++;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // レジストリを元に戻す
            regkey.DeleteValue(process_name);
            regkey.DeleteValue(process_dbg_name);
            regkey.Close();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            myBrowser.Refresh();
        }
    }
}
