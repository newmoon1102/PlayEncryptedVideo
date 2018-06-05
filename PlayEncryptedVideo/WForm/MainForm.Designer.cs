namespace PlayEncryptedVideo.WForm
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.axWMP = new AxWMPLib.AxWindowsMediaPlayer();
            this.panelContent = new System.Windows.Forms.Panel();
            this.btnCourse = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnWebsite = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSupport = new System.Windows.Forms.Button();
            this.btnContent = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnpdf = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAutoplay = new System.Windows.Forms.Button();
            this.panelDesc = new System.Windows.Forms.Panel();
            this.lbChapter = new System.Windows.Forms.Label();
            this.lbLessonOf = new System.Windows.Forms.Label();
            this.lbDescLine2 = new System.Windows.Forms.Label();
            this.lbDescLine1 = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.axWMP)).BeginInit();
            this.panelContent.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelDesc.SuspendLayout();
            this.SuspendLayout();
            // 
            // axWMP
            // 
            this.axWMP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axWMP.Enabled = true;
            this.axWMP.Location = new System.Drawing.Point(0, 0);
            this.axWMP.Margin = new System.Windows.Forms.Padding(0);
            this.axWMP.Name = "axWMP";
            this.axWMP.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWMP.OcxState")));
            this.axWMP.Size = new System.Drawing.Size(1245, 765);
            this.axWMP.TabIndex = 0;
            this.axWMP.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.axWMP_PlayStateChange);
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelContent.BackColor = System.Drawing.SystemColors.Control;
            this.panelContent.Controls.Add(this.btnCourse);
            this.panelContent.Controls.Add(this.btnHome);
            this.panelContent.Controls.Add(this.btnWebsite);
            this.panelContent.Controls.Add(this.btnExit);
            this.panelContent.Controls.Add(this.btnSupport);
            this.panelContent.Controls.Add(this.btnContent);
            this.panelContent.Controls.Add(this.btnHelp);
            this.panelContent.Controls.Add(this.btnAbout);
            this.panelContent.Location = new System.Drawing.Point(0, -1);
            this.panelContent.Margin = new System.Windows.Forms.Padding(0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(202, 905);
            this.panelContent.TabIndex = 7;
            // 
            // btnCourse
            // 
            this.btnCourse.BackColor = System.Drawing.SystemColors.Control;
            this.btnCourse.Font = new System.Drawing.Font("メイリオ", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnCourse.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCourse.Image = global::PlayEncryptedVideo.Properties.Resources.coursefiles;
            this.btnCourse.Location = new System.Drawing.Point(3, 317);
            this.btnCourse.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.btnCourse.Name = "btnCourse";
            this.btnCourse.Size = new System.Drawing.Size(195, 47);
            this.btnCourse.TabIndex = 8;
            this.btnCourse.UseVisualStyleBackColor = false;
            this.btnCourse.Click += new System.EventHandler(this.btnCourse_Click);
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.SystemColors.Control;
            this.btnHome.Font = new System.Drawing.Font("メイリオ", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnHome.ForeColor = System.Drawing.SystemColors.Control;
            this.btnHome.Image = global::PlayEncryptedVideo.Properties.Resources.home;
            this.btnHome.Location = new System.Drawing.Point(3, 5);
            this.btnHome.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(195, 47);
            this.btnHome.TabIndex = 7;
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnWebsite
            // 
            this.btnWebsite.BackColor = System.Drawing.SystemColors.Control;
            this.btnWebsite.Font = new System.Drawing.Font("メイリオ", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnWebsite.ForeColor = System.Drawing.SystemColors.Control;
            this.btnWebsite.Image = global::PlayEncryptedVideo.Properties.Resources.website;
            this.btnWebsite.Location = new System.Drawing.Point(3, 109);
            this.btnWebsite.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.btnWebsite.Name = "btnWebsite";
            this.btnWebsite.Size = new System.Drawing.Size(195, 47);
            this.btnWebsite.TabIndex = 0;
            this.btnWebsite.UseVisualStyleBackColor = false;
            this.btnWebsite.Click += new System.EventHandler(this.btnWebsite_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnExit.Image = global::PlayEncryptedVideo.Properties.Resources.exit;
            this.btnExit.Location = new System.Drawing.Point(3, 854);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(195, 40);
            this.btnExit.TabIndex = 5;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSupport
            // 
            this.btnSupport.BackColor = System.Drawing.SystemColors.Control;
            this.btnSupport.Font = new System.Drawing.Font("メイリオ", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnSupport.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSupport.Image = global::PlayEncryptedVideo.Properties.Resources.support;
            this.btnSupport.Location = new System.Drawing.Point(3, 161);
            this.btnSupport.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.btnSupport.Name = "btnSupport";
            this.btnSupport.Size = new System.Drawing.Size(195, 47);
            this.btnSupport.TabIndex = 3;
            this.btnSupport.UseVisualStyleBackColor = false;
            this.btnSupport.Click += new System.EventHandler(this.btnSupport_Click);
            // 
            // btnContent
            // 
            this.btnContent.BackColor = System.Drawing.SystemColors.Control;
            this.btnContent.Font = new System.Drawing.Font("メイリオ", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnContent.ForeColor = System.Drawing.SystemColors.Control;
            this.btnContent.Image = global::PlayEncryptedVideo.Properties.Resources.content;
            this.btnContent.Location = new System.Drawing.Point(3, 57);
            this.btnContent.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.btnContent.Name = "btnContent";
            this.btnContent.Size = new System.Drawing.Size(195, 47);
            this.btnContent.TabIndex = 1;
            this.btnContent.UseVisualStyleBackColor = false;
            this.btnContent.Click += new System.EventHandler(this.btnContent_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.SystemColors.Control;
            this.btnHelp.Font = new System.Drawing.Font("メイリオ", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnHelp.ForeColor = System.Drawing.SystemColors.Control;
            this.btnHelp.Image = global::PlayEncryptedVideo.Properties.Resources.help;
            this.btnHelp.Location = new System.Drawing.Point(3, 213);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(195, 47);
            this.btnHelp.TabIndex = 6;
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.BackColor = System.Drawing.SystemColors.Control;
            this.btnAbout.Font = new System.Drawing.Font("メイリオ", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnAbout.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAbout.Image = global::PlayEncryptedVideo.Properties.Resources.about;
            this.btnAbout.Location = new System.Drawing.Point(3, 265);
            this.btnAbout.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(195, 47);
            this.btnAbout.TabIndex = 4;
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.Controls.Add(this.panelBottom);
            this.panelMain.Controls.Add(this.axWMP);
            this.panelMain.Location = new System.Drawing.Point(202, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1250, 898);
            this.panelMain.TabIndex = 8;
            // 
            // panelBottom
            // 
            this.panelBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelBottom.Controls.Add(this.btnpdf);
            this.panelBottom.Controls.Add(this.label4);
            this.panelBottom.Controls.Add(this.btnAutoplay);
            this.panelBottom.Controls.Add(this.panelDesc);
            this.panelBottom.Controls.Add(this.btnNext);
            this.panelBottom.Controls.Add(this.btnPrevious);
            this.panelBottom.Location = new System.Drawing.Point(0, 765);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(0);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1245, 132);
            this.panelBottom.TabIndex = 1;
            // 
            // btnpdf
            // 
            this.btnpdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnpdf.BackColor = System.Drawing.SystemColors.Control;
            this.btnpdf.Font = new System.Drawing.Font("メイリオ", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnpdf.ForeColor = System.Drawing.SystemColors.Control;
            this.btnpdf.Image = ((System.Drawing.Image)(resources.GetObject("btnpdf.Image")));
            this.btnpdf.Location = new System.Drawing.Point(874, 10);
            this.btnpdf.Margin = new System.Windows.Forms.Padding(0);
            this.btnpdf.Name = "btnpdf";
            this.btnpdf.Size = new System.Drawing.Size(162, 35);
            this.btnpdf.TabIndex = 10;
            this.btnpdf.UseVisualStyleBackColor = false;
            this.btnpdf.Click += new System.EventHandler(this.btnpdf_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(1112, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "AutoPlay";
            // 
            // btnAutoplay
            // 
            this.btnAutoplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutoplay.Image = global::PlayEncryptedVideo.Properties.Resources.auto_off;
            this.btnAutoplay.Location = new System.Drawing.Point(1179, 19);
            this.btnAutoplay.Margin = new System.Windows.Forms.Padding(0);
            this.btnAutoplay.Name = "btnAutoplay";
            this.btnAutoplay.Size = new System.Drawing.Size(55, 22);
            this.btnAutoplay.TabIndex = 8;
            this.btnAutoplay.UseVisualStyleBackColor = true;
            this.btnAutoplay.Click += new System.EventHandler(this.btnAutoplay_Click);
            // 
            // panelDesc
            // 
            this.panelDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDesc.Controls.Add(this.lbChapter);
            this.panelDesc.Controls.Add(this.lbLessonOf);
            this.panelDesc.Controls.Add(this.lbDescLine2);
            this.panelDesc.Controls.Add(this.lbDescLine1);
            this.panelDesc.Controls.Add(this.lbTitle);
            this.panelDesc.Location = new System.Drawing.Point(0, 53);
            this.panelDesc.Margin = new System.Windows.Forms.Padding(0);
            this.panelDesc.Name = "panelDesc";
            this.panelDesc.Size = new System.Drawing.Size(1245, 86);
            this.panelDesc.TabIndex = 7;
            // 
            // lbChapter
            // 
            this.lbChapter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbChapter.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbChapter.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbChapter.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbChapter.Location = new System.Drawing.Point(842, 8);
            this.lbChapter.Name = "lbChapter";
            this.lbChapter.Size = new System.Drawing.Size(392, 18);
            this.lbChapter.TabIndex = 4;
            this.lbChapter.Text = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            this.lbChapter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbLessonOf
            // 
            this.lbLessonOf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLessonOf.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLessonOf.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbLessonOf.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbLessonOf.Location = new System.Drawing.Point(1132, 33);
            this.lbLessonOf.Name = "lbLessonOf";
            this.lbLessonOf.Size = new System.Drawing.Size(102, 18);
            this.lbLessonOf.TabIndex = 3;
            this.lbLessonOf.Text = "Lesson 0 of 0";
            this.lbLessonOf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbDescLine2
            // 
            this.lbDescLine2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbDescLine2.AutoSize = true;
            this.lbDescLine2.Font = new System.Drawing.Font("Arial", 12F);
            this.lbDescLine2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbDescLine2.Location = new System.Drawing.Point(5, 55);
            this.lbDescLine2.Name = "lbDescLine2";
            this.lbDescLine2.Size = new System.Drawing.Size(162, 18);
            this.lbDescLine2.TabIndex = 2;
            this.lbDescLine2.Text = "xxxxxxxxxxxxxxxxxxxxxx";
            // 
            // lbDescLine1
            // 
            this.lbDescLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbDescLine1.AutoSize = true;
            this.lbDescLine1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescLine1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbDescLine1.Location = new System.Drawing.Point(5, 33);
            this.lbDescLine1.Name = "lbDescLine1";
            this.lbDescLine1.Size = new System.Drawing.Size(78, 18);
            this.lbDescLine1.TabIndex = 1;
            this.lbDescLine1.Text = "xxxxxxxxxx";
            // 
            // lbTitle
            // 
            this.lbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbTitle.Location = new System.Drawing.Point(4, 4);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(142, 22);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "xxxxxxxxxxxx";
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNext.BackColor = System.Drawing.SystemColors.Control;
            this.btnNext.Font = new System.Drawing.Font("メイリオ", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnNext.ForeColor = System.Drawing.SystemColors.Control;
            this.btnNext.Image = global::PlayEncryptedVideo.Properties.Resources.next;
            this.btnNext.Location = new System.Drawing.Point(189, 10);
            this.btnNext.Margin = new System.Windows.Forms.Padding(0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(165, 35);
            this.btnNext.TabIndex = 6;
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrevious.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrevious.Font = new System.Drawing.Font("メイリオ", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnPrevious.ForeColor = System.Drawing.SystemColors.Control;
            this.btnPrevious.Image = global::PlayEncryptedVideo.Properties.Resources.previous;
            this.btnPrevious.Location = new System.Drawing.Point(8, 10);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(166, 35);
            this.btnPrevious.TabIndex = 5;
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Please Insert DVD #1 then select movies folder Or choose movies folder on the har" +
    "d disk";
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1448, 897);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelContent);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PlayEncryptedVideo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.axWMP)).EndInit();
            this.panelContent.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.panelDesc.ResumeLayout(false);
            this.panelDesc.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnWebsite;
        private System.Windows.Forms.Button btnSupport;
        private System.Windows.Forms.Button btnContent;
        private System.Windows.Forms.Button btnHelp;
        private AxWMPLib.AxWindowsMediaPlayer axWMP;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelDesc;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Label lbDescLine2;
        private System.Windows.Forms.Label lbDescLine1;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAutoplay;
        private System.Windows.Forms.Button btnpdf;
        private System.Windows.Forms.Label lbLessonOf;
        private System.Windows.Forms.Button btnCourse;
        private System.Windows.Forms.Label lbChapter;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

