namespace RPS_Server
{
    partial class FormServer
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ListBox lstLog;

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
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(12, 9);
            this.lblStatus.Size = new System.Drawing.Size(400, 23);
            this.lblStatus.Text = "Server chưa khởi động";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(15, 40);
            this.btnStart.Size = new System.Drawing.Size(150, 30);
            this.btnStart.Text = "Bắt đầu Server";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(180, 40);
            this.btnStop.Size = new System.Drawing.Size(150, 30);
            this.btnStop.Text = "Dừng Server";
            this.btnStop.Enabled = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lstLog
            // 
            this.lstLog.Location = new System.Drawing.Point(15, 80);
            this.lstLog.Size = new System.Drawing.Size(400, 200);
            // 
            // FormServer
            // 
            this.ClientSize = new System.Drawing.Size(430, 300);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lstLog);
            this.Text = "RPS Server";
            this.ResumeLayout(false);
        }
    }
}
