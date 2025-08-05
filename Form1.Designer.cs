namespace PddRefundCheckerWin
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(20, 20);
            this.txtFilePath.Size = new System.Drawing.Size(400, 23);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(430, 20);
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile.Text = "选择文件";
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(20, 60);
            this.btnStart.Size = new System.Drawing.Size(100, 30);
            this.btnStart.Text = "开始处理";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(20, 110);
            this.logBox.Multiline = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(600, 300);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(140, 65);
            this.progressBar.Size = new System.Drawing.Size(480, 20);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 450);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.progressBar);
            this.Name = "Form1";
            this.Text = "拼多多退货状态标记工具";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}