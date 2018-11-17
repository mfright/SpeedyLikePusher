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

using System.IO;
using System.Windows;
using System.Windows.Input;

namespace SpeedyLikeSender
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

        // タイムラインはダブルクリック２回で１回とみなすため
        Boolean point5 = false;

        //カウントリミット
        public int limitter = 1000;

        // 実行中フラグ
        Boolean running_tag = false;
        Boolean running_timeline = false;

        

        public frmMain(bool automationMode)
        {
            InitializeComponent();

            //送信回数制限ファイルを読み込み
            loadLimit();

            //ハッシュタグいいね送信インターバル設定ファイル読込
            loadInterval();

            if (automationMode)
            {
                btnAutomation.BackColor = Color.Yellow;
                tmAutomation.Start();
            }
        }

        /// <summary>
        /// 送信回数制限の設定ファイルを読み込む
        /// </summary>
        private void loadLimit()
        {
            StreamReader sr1;   
            try
            {
                sr1 = new StreamReader("limitter.ini", System.Text.Encoding.GetEncoding("shift_jis"));

                string message = sr1.ReadLine();
                limitter = int.Parse(message);
                lblLimitter.Text = "/ " + limitter;

                sr1.Close();
            }
            catch (Exception e)
            {
                

                // 読み込みに失敗したら　設定ファイルを作成
                try{
                    StreamWriter sw1 = new StreamWriter("limitter.ini", false, System.Text.Encoding.GetEncoding("shift_jis"));
                    sw1.WriteLine("1000");
                    sw1.Close();

                }catch(Exception ex){
                    MessageBox.Show("File saving error. limitter.ini");

                }
            }
            
        }

        /// <summary>
        /// インターバル設定ファイルを読み込む
        /// </summary>
        private void loadInterval()
        {
            StreamReader sr1;
            try
            {
                sr1 = new StreamReader("interval.ini", System.Text.Encoding.GetEncoding("shift_jis"));

                string message = sr1.ReadLine();
                timerHashTag.Interval = int.Parse(message);

                sr1.Close();
            }
            catch (Exception e)
            {


                // 読み込みに失敗したら　設定ファイルを作成
                try
                {
                    StreamWriter sw1 = new StreamWriter("interval.ini", false, System.Text.Encoding.GetEncoding("shift_jis"));
                    sw1.WriteLine("7000");
                    sw1.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("File saving error. interval.ini");

                }
            }

        }

        Boolean flagSingleClicked = false;
        private void timerTag_Tick(object sender, EventArgs e)
        {
            this.TopMost = true;

            // 数回に一回、画像読み込みを試行する
            if (countDoubleClick % 10 == 0 && !flagSingleClicked)
            {
                // 下へスクロール
                myBrowser.Document.Window.ScrollTo(new System.Drawing.Point(0, 600000));
                System.Threading.Thread.Sleep(1000);

                // 画像をクリック
                mp = this.PointToScreen(
                    new System.Drawing.Point(100, 400));
                System.Windows.Forms.Cursor.Position = mp;

                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

                flagSingleClicked = true;

            }
            else
            {
                //表示された画像の上にカーソルを動かしダブルクリック
                mp = this.PointToScreen(
                        new System.Drawing.Point(259, 430));
                System.Windows.Forms.Cursor.Position = mp;

                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                System.Threading.Thread.Sleep(100);

                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                System.Threading.Thread.Sleep(300);



                //次画像ボタンの上にカーソルを動かしクリック
                mp = this.PointToScreen(
                        new System.Drawing.Point(750, 350));
                System.Windows.Forms.Cursor.Position = mp;

                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);


                //カウンターを増加
                countIncrement(false);


                flagSingleClicked = false;
            }
            
        }

        private void countIncrement(Boolean half){
            if (half)
            {//タイムラインの時は、２回に１回インクリメントする。
                if (point5)
                {
                    countDoubleClick++;
                    point5 = false;
                    lblCounter.Text = countDoubleClick.ToString();
                }
                else
                {
                    point5 = true;
                }
            }
            else
            {   //タグ一覧の時は、１回に１回インクリメントする。
                countDoubleClick++;
                lblCounter.Text = countDoubleClick + "";
            }


            if (countDoubleClick == limitter)
            {
                stop();
                countDoubleClick++; //タイムラインだと２回警告を出してしまうのを防ぐため1+
                MessageBox.Show("Maybe still sent LIKEs about near limit with using this tool.\r\nYour account will be locked by Instagram if you continue to sending today.","Attention!");
                
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // キー操作を拾うイベントハンドラを登録 (ESCを押したときに拾う)
            Application.Idle += new EventHandler(Application_Idle);
            
        }


        private void btn_startTag_Click(object sender, EventArgs e)
        {
            if (running_tag)
            {
                stop();             
            
            }
            else
            {
                this.Text = "RUNNING...  Press Pause/Break key to STOP";
                btn_startTag.BackColor = Color.Yellow;
                this.TopMost = true;
                running_tag = true;
                btn_startTag.Text = "Press Pause/Break key to STOP.";
                
                
                timerHashTag.Start();
            } 
            
        }

        //キーが押された時の動作
        private void Application_Idle(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Escape) == true ||  Keyboard.IsKeyDown(Key.Pause) == true )
            {
                // ESCかPause/Breakキー
                stop();

            }else if (Keyboard.IsKeyDown(Key.A))
            {
                // Aキー
                btnAutomation_Click(null, null);
            }
        }

        private void stop()
        {
            this.Text = "Stopped. -- Speedy LIKE sender.";
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
                this.Text = "RUNNING...  Press Pause/Break key to STOP";   
                btnStartTimeline.BackColor = Color.Yellow;
                this.TopMost = true; 
                running_timeline = true;
                btnStartTimeline.Text = "Press Pause/Break key to STOP.";

                timerTimeline.Start();
            } 
        }

        private void timerTimeline_Tick(object sender, EventArgs e)
        {
            try
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

                // カウンター表示
                countIncrement(true);

                // 下へスクロール
                myBrowser.Document.Window.ScrollTo(new System.Drawing.Point(0, countScroll * 400));
                countScroll++;
                
                
            }
            catch (Exception ex)
            {
                stop();
                MessageBox.Show("Finished.");
            }
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



        // オートメーションボタン
        private void btnAutomation_Click(object sender, EventArgs e)
        {
            if (tmAutomation.Enabled)
            {
                tmAutomation.Stop();
                btnAutomation.BackColor = Control.DefaultBackColor;
            }
            else
            {
                tmAutomation.Start();
                btnAutomation.BackColor = Color.Yellow;

                // キーワードを読み込む
                loadKeywords();

                // 最初のキーワードの検索結果ページへ
                myBrowser.Navigate("https://www.instagram.com/explore/tags/" + keywords[0]);
            }
        }


        //オートメーションモードのときは、このタイマーが走る
        int stage = 1;  //状態
        List<string> keywords = new List<string>(); //キーワード群
        int prevChangedNumber = 0;
        private void tmAutomation_Tick(object sender, EventArgs e)
        {
            this.TopMost = true;

            if (countDoubleClick >= limitter)
            {
                tmAutomation.Stop();
                btnAutomation.BackColor = Control.DefaultBackColor;
            }
            else if (stage == 1)
            {
                //読み込みにちょっと待つ
                stage++;

            }
            else if (stage == 2)
            {
                // 下へスクロール
                myBrowser.Document.Window.ScrollTo(new System.Drawing.Point(0, 1000));

                //画像を選ぶ
                mp = this.PointToScreen(
                    new System.Drawing.Point(300, 430));
                System.Windows.Forms.Cursor.Position = mp;
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                System.Threading.Thread.Sleep(100);


                // 自動クリック開始
                btn_startTag_Click(null, null);

                stage++;
            }
            else if (stage == 3)
            {
                int keywordsCount = keywords.Count; //キーワード数
                int photoCountOfKeyword = limitter / keywordsCount;//キーワード１個あたりの連打数
                Console.WriteLine("limitter:"+limitter);
                Console.WriteLine("keywordsCount:" + keywordsCount);
                Console.WriteLine("photoCountOfKeyword:"+photoCountOfKeyword);

                if (countDoubleClick % photoCountOfKeyword == 0 //次のキーワードに変える節目に来た
                    && countDoubleClick > prevChangedNumber //まだキーワードチェンジしていない
                    && countDoubleClick/photoCountOfKeyword < keywords.Count) //まだキーワードが残っている
                {
                    // 自動クリック一時停止
                    btn_startTag_Click(null, null);

                    string nextKeyword = keywords[countDoubleClick / photoCountOfKeyword];//次のキーワード

                    myBrowser.Navigate("https://www.instagram.com/explore/tags/" + nextKeyword);

                    prevChangedNumber = countDoubleClick;

                    stage++;
                }
            }
            else if (stage == 4)
            {
                //読み込みにちょっと待つ
                stage++;

            }
            else if(stage == 5)
            {
                // 下へスクロール
                myBrowser.Document.Window.ScrollTo(new System.Drawing.Point(0, 1000));

                //画像を選ぶ
                mp = this.PointToScreen(
                    new System.Drawing.Point(300, 430));
                System.Windows.Forms.Cursor.Position = mp;
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                System.Threading.Thread.Sleep(100);


                // 自動クリック再開
                btn_startTag_Click(null, null);

                stage -= 2;
            }




            /*
            if (stage == 1)
            {
                //1ターン目 プロフィールをクリック 
                System.Drawing.Point mp = this.PointToScreen(
                        new System.Drawing.Point(714, 93));
                System.Windows.Forms.Cursor.Position = mp;

                System.Threading.Thread.Sleep(10);

                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

                tmAutomation.Interval = 5000;
            }
            else if (stage == 2)
            {
                //2ターン目 最新の投稿済み画像をクリック 
                System.Drawing.Point mp = this.PointToScreen(
                        new System.Drawing.Point(130, 622));
                System.Windows.Forms.Cursor.Position = mp;

                System.Threading.Thread.Sleep(10);

                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            }
            else if (stage == 3)
            {
                //3ターン目 投稿キャプションを下へスクロール 
                System.Drawing.Point mp = this.PointToScreen(
                        new System.Drawing.Point(720, 380));
                System.Windows.Forms.Cursor.Position = mp;

                for (int i = 0; i < 15; i++)
                {
                    System.Threading.Thread.Sleep(100);
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                }

            }
            else if (stage == 4)
            {
                //4ターン目 投稿キャプションを下へスクロール 
                System.Drawing.Point mp = this.PointToScreen(
                        new System.Drawing.Point(720, 380));
                System.Windows.Forms.Cursor.Position = mp;

                for (int i = 0; i < 15; i++)
                {
                    System.Threading.Thread.Sleep(100);
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                }

            }
            else if (stage == 5)
            {
                //5ターン目 タグをクリック
                System.Drawing.Point mp = this.PointToScreen(
                        new System.Drawing.Point(461, 325));
                System.Windows.Forms.Cursor.Position = mp;

                System.Threading.Thread.Sleep(100);
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);


            }
            else if (stage == 6)
            {
                //6ターン目 下へスクロール(人気投稿ではなく新着投稿へスクロール)
                System.Drawing.Point mp = this.PointToScreen(
                        new System.Drawing.Point(778,529));
                System.Windows.Forms.Cursor.Position = mp;

                for (int i = 0; i < 10; i++)
                {
                    System.Threading.Thread.Sleep(100);
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                }


            }
            else if (stage == 7)
            {
                //右上のボタンをクリック。
                System.Drawing.Point mp = this.PointToScreen(
                        new System.Drawing.Point(607, 29));
                System.Windows.Forms.Cursor.Position = mp;

                System.Threading.Thread.Sleep(100);
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);


            }

            stage++;
            */

        }

        /// <summary>
        /// キーワードの設定ファイルを読み込む
        /// </summary>
        private void loadKeywords()
        {
            if(keywords.Count > 0)
            {
                return;
            }

            StreamReader sr1;
            try
            {
                sr1 = new StreamReader("keywords.ini", System.Text.Encoding.GetEncoding("shift_jis"));

                while (true)
                {
                    string message = sr1.ReadLine();

                    if(message == null)
                    {
                        break;
                    }
                    else if(message.Length>1)
                    {
                        keywords.Add(message);
                        Console.WriteLine(keywords.Count + ":" + message);
                        
                    }
                }
                
                

                sr1.Close();
            }
            catch (Exception e)
            {


                // 読み込みに失敗したら　ファイルを作成
                try
                {
                    StreamWriter sw1 = new StreamWriter("keywords.ini", false, System.Text.Encoding.GetEncoding("shift_jis"));
                    sw1.WriteLine("airy_pics\r\nart_of_japan_\r\nvisitjapanjp");
                    sw1.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("File saving error. keywords.ini");

                }
            }

        }


    }
}
