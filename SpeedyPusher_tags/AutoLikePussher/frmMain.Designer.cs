﻿namespace SpeedyLikeSender
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.myBrowser = new System.Windows.Forms.WebBrowser();
            this.btn_startTag = new System.Windows.Forms.Button();
            this.timerHashTag = new System.Windows.Forms.Timer(this.components);
            this.timerTimeline = new System.Windows.Forms.Timer(this.components);
            this.btnStartTimeline = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.lblCounter = new System.Windows.Forms.Label();
            this.tmAutomation = new System.Windows.Forms.Timer(this.components);
            this.lblLimitter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // myBrowser
            // 
            this.myBrowser.Location = new System.Drawing.Point(0, 54);
            this.myBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.myBrowser.Name = "myBrowser";
            this.myBrowser.Size = new System.Drawing.Size(785, 604);
            this.myBrowser.TabIndex = 0;
            this.myBrowser.Url = new System.Uri("https://instagram.com", System.UriKind.Absolute);
            // 
            // btn_startTag
            // 
            this.btn_startTag.Location = new System.Drawing.Point(484, 0);
            this.btn_startTag.Name = "btn_startTag";
            this.btn_startTag.Size = new System.Drawing.Size(301, 55);
            this.btn_startTag.TabIndex = 1;
            this.btn_startTag.Text = "[2] Open photos page about a hash-tag.\r\nThen press here to START.";
            this.btn_startTag.UseVisualStyleBackColor = true;
            this.btn_startTag.Click += new System.EventHandler(this.btn_startTag_Click);
            // 
            // timerHashTag
            // 
            this.timerHashTag.Interval = 3000;
            this.timerHashTag.Tick += new System.EventHandler(this.timerTag_Tick);
            // 
            // timerTimeline
            // 
            this.timerTimeline.Interval = 300;
            this.timerTimeline.Tick += new System.EventHandler(this.timerTimeline_Tick);
            // 
            // btnStartTimeline
            // 
            this.btnStartTimeline.Location = new System.Drawing.Point(0, 0);
            this.btnStartTimeline.Name = "btnStartTimeline";
            this.btnStartTimeline.Size = new System.Drawing.Size(326, 55);
            this.btnStartTimeline.TabIndex = 2;
            this.btnStartTimeline.Text = "[1] Open timeline page \r\nand press here to START.";
            this.btnStartTimeline.UseVisualStyleBackColor = true;
            this.btnStartTimeline.Click += new System.EventHandler(this.btnStartTimeline_Click);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(331, 30);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(147, 23);
            this.btnReload.TabIndex = 3;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // lblCounter
            // 
            this.lblCounter.AutoSize = true;
            this.lblCounter.Location = new System.Drawing.Point(354, 7);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(11, 12);
            this.lblCounter.TabIndex = 4;
            this.lblCounter.Text = "0";
            // 
            // lblLimitter
            // 
            this.lblLimitter.AutoSize = true;
            this.lblLimitter.Location = new System.Drawing.Point(415, 8);
            this.lblLimitter.Name = "lblLimitter";
            this.lblLimitter.Size = new System.Drawing.Size(11, 12);
            this.lblLimitter.TabIndex = 6;
            this.lblLimitter.Text = "/";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 658);
            this.Controls.Add(this.lblLimitter);
            this.Controls.Add(this.lblCounter);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.btnStartTimeline);
            this.Controls.Add(this.btn_startTag);
            this.Controls.Add(this.myBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "SpeedyLikeSender";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser myBrowser;
        private System.Windows.Forms.Button btn_startTag;
        private System.Windows.Forms.Timer timerHashTag;
        private System.Windows.Forms.Timer timerTimeline;
        private System.Windows.Forms.Button btnStartTimeline;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Timer tmAutomation;
        private System.Windows.Forms.Label lblLimitter;
    }
}

