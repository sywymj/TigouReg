namespace TigouRegApp
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSetAccountFilePath = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImportEmail = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonBeginReg = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStopReg = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.textBoxVerifyCode = new System.Windows.Forms.TextBox();
            this.pictureBoxVerify = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVerify)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSetAccountFilePath,
            this.toolStripButtonImportEmail,
            this.toolStripButtonBeginReg,
            this.toolStripButtonStopReg});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(592, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonSetAccountFilePath
            // 
            this.toolStripButtonSetAccountFilePath.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSetAccountFilePath.Image")));
            this.toolStripButtonSetAccountFilePath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSetAccountFilePath.Name = "toolStripButtonSetAccountFilePath";
            this.toolStripButtonSetAccountFilePath.Size = new System.Drawing.Size(76, 22);
            this.toolStripButtonSetAccountFilePath.Text = "帐号文件";
            this.toolStripButtonSetAccountFilePath.Click += new System.EventHandler(this.toolStripButtonSetAccountFilePath_Click);
            // 
            // toolStripButtonImportEmail
            // 
            this.toolStripButtonImportEmail.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonImportEmail.Image")));
            this.toolStripButtonImportEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImportEmail.Name = "toolStripButtonImportEmail";
            this.toolStripButtonImportEmail.Size = new System.Drawing.Size(76, 22);
            this.toolStripButtonImportEmail.Text = "导入邮箱";
            this.toolStripButtonImportEmail.Click += new System.EventHandler(this.toolStripButtonImportEmail_Click);
            // 
            // toolStripButtonBeginReg
            // 
            this.toolStripButtonBeginReg.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBeginReg.Image")));
            this.toolStripButtonBeginReg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBeginReg.Name = "toolStripButtonBeginReg";
            this.toolStripButtonBeginReg.Size = new System.Drawing.Size(76, 22);
            this.toolStripButtonBeginReg.Text = "开始注册";
            this.toolStripButtonBeginReg.Click += new System.EventHandler(this.toolStripButtonBeginReg_Click);
            // 
            // toolStripButtonStopReg
            // 
            this.toolStripButtonStopReg.Enabled = false;
            this.toolStripButtonStopReg.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStopReg.Image")));
            this.toolStripButtonStopReg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStopReg.Name = "toolStripButtonStopReg";
            this.toolStripButtonStopReg.Size = new System.Drawing.Size(76, 22);
            this.toolStripButtonStopReg.Text = "停止注册";
            this.toolStripButtonStopReg.Click += new System.EventHandler(this.toolStripButtonStopReg_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 316);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(592, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxLog);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxVerifyCode);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBoxVerify);
            this.splitContainer1.Size = new System.Drawing.Size(592, 291);
            this.splitContainer1.SplitterDistance = 415;
            this.splitContainer1.TabIndex = 2;
            // 
            // listBoxLog
            // 
            this.listBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 12;
            this.listBoxLog.Location = new System.Drawing.Point(0, 0);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(415, 291);
            this.listBoxLog.TabIndex = 0;
            // 
            // textBoxVerifyCode
            // 
            this.textBoxVerifyCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxVerifyCode.Location = new System.Drawing.Point(0, 82);
            this.textBoxVerifyCode.Name = "textBoxVerifyCode";
            this.textBoxVerifyCode.Size = new System.Drawing.Size(173, 21);
            this.textBoxVerifyCode.TabIndex = 1;
            // 
            // pictureBoxVerify
            // 
            this.pictureBoxVerify.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBoxVerify.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxVerify.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxVerify.Name = "pictureBoxVerify";
            this.pictureBoxVerify.Size = new System.Drawing.Size(173, 82);
            this.pictureBoxVerify.TabIndex = 0;
            this.pictureBoxVerify.TabStop = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "文本文件|*.txt";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "文本文件|*.txt";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 338);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "TigouRegister";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVerify)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSetAccountFilePath;
        private System.Windows.Forms.ToolStripButton toolStripButtonImportEmail;
        private System.Windows.Forms.ToolStripButton toolStripButtonBeginReg;
        private System.Windows.Forms.ToolStripButton toolStripButtonStopReg;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox textBoxVerifyCode;
        private System.Windows.Forms.PictureBox pictureBoxVerify;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

