//-------------------------------------------------//
//  Speedy "Like" sender for Instagram.            //
//  インスタでスピーディに「いいね」して回る       //
//-------------------------------------------------//



■1.ABSTRACT

Are you tired to send "LIKE"s on your timeline / hash-tagged photos in Instagram?
This software is to do double-click each photos for "LIKE"s.
A. Your main time line.
B. Photos thumbnail page of a hash-tag. (Your followers will increase.)


このソフトウェアは、Instagramのタイムラインやタグの写真一覧画面にて、
自動でダブルクリックしていくことで「いいね」送信していくものです。
「いいね」を押して回るのが面倒な時、２時間ほどで制限手前まで「いいね」送信できます。
A.タイムラインの写真に片っ端から「いいね」する。
B.ハッシュタグの写真一覧画面で片っ端から「いいね」する(フォロワーが増えます)



■2.How to use.

Install ".net Framework"4 or later and IE11 to your Windows PC.
Run "SpeedyLikeSender.exe".
Then log-in to your Instagram account.
Access to your main-timeline page and press the [1] button. 
Or access to a thumbnails page about a hash-tag and press [2] button.

See https://www.youtube.com/watch?v=H1UNhahx-Ew&feature=youtu.be .


パソコンに「.net Framework」とIE11をインストールします。
「SpeedyLikeSender.exe」を起動し、あなたのインスタアカウントでログインします。

�@　タイムラインに一斉に「いいね」を付けたい場合は、
タイムラインを表示させた状態で、[1]のボタンを押します。
止めたいときはキーボードのPause/Breakキーを押してください。

�A　特定のハッシュタグの画像に一斉に「いいね」を付けたい場合は、
ハッシュタグの画像一覧画面を表示してから、[2]のボタンを押してください。

�B　自動でいろんなハッシュタグの画像に次々と「いいね」したい場合は、
keywords.iniの中身をそのタグに書き換えてから起動し、[3]ボタンを押してください。

詳しくは上のYoutube動画をご覧ください。



■3.Settings
"limitter.ini" -> Number of auto-stop limitter.
"interval.ini" -> Interval-time of changing to the next photo.

１日のうちで「いいね」しすぎるとInstagramからブロックされ、しばらく「いいね」できなくなります。
(およそ1000回/1日ではないかと言われていますが、定かではありません)
そこで、デフォルトでは1000回クリックしたらダイアログが表示され、いいね送信が自動停止します。
この自動停止の回数はデフォルトで1000としていますが、
変更したい場合は、limitter.iniをテキストエディタで開き、
数値を書き換えてからツールを起動しなおしてください。

ハッシュタグ画像に「いいね」していくモードでは、デフォルトでは7000ミリ秒(=7秒)ごとに画像を切り替えています。
通信速度によってはもっと遅くしたほうが安定して動作したり、
逆に早く動作させられることもあるので、
このインターバルを変更したい場合は、
interval.iniを書き換えてからツールを起動しなおしてください。




■4.Other tools
For Twitter, "AutoFav" is released on the URL below.
https://github.com/mfright/SpeedyFavoriter2_forTwitter/releases
Twitter用には、AutoFavというソフトを提供しています。
上記URLで公開しています。


There is an accout that is for supporting to increase your followers.
Accounts following the account below is following each other.
https://www.instagram.com/follow_each_other2018/

相互フォロー支援アカウントも提供しています。
上記アカウントも活用してみてください。



■5.Develop

Source code is stored on Github.(https://github.com/mfright/SpeedyLikePusher)
ぜひ上記URLにアクセスしてください。
http://www.ddhost.jp