namespace RPS_Client
{
    partial class FormClient
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnBua;
        private System.Windows.Forms.Button btnBao;
        private System.Windows.Forms.Button btnKeo;

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
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnBua = new System.Windows.Forms.Button();
            this.btnBao = new System.Windows.Forms.Button();
            this.btnKeo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(20, 20);
            this.txtServerIP.Size = new System.Drawing.Size(150, 22);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(190, 20);
            this.btnConnect.Size = new System.Drawing.Size(100, 25);
            this.btnConnect.Text = "Kết Nối";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(20, 60);
            this.txtResult.Size = new System.Drawing.Size(270, 22);
            this.txtResult.ReadOnly = true;
            // 
            // btnBua
            // 
            this.btnBua.Location = new System.Drawing.Point(20, 100);
            this.btnBua.Size = new System.Drawing.Size(80, 40);
            this.btnBua.Text = "Búa";
            this.btnBua.Click += new System.EventHandler(this.btnBua_Click);
            // 
            // btnBao
            // 
            this.btnBao.Location = new System.Drawing.Point(110, 100);
            this.btnBao.Size = new System.Drawing.Size(80, 40);
            this.btnBao.Text = "Bao";
            this.btnBao.Click += new System.EventHandler(this.btnBao_Click);
            // 
            // btnKeo
            // 
            this.btnKeo.Location = new System.Drawing.Point(200, 100);
            this.btnKeo.Size = new System.Drawing.Size(80, 40);
            this.btnKeo.Text = "Kéo";
            this.btnKeo.Click += new System.EventHandler(this.btnKeo_Click);
            // 
            // FormClient
            // 
            this.ClientSize = new System.Drawing.Size(320, 180);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnBua);
            this.Controls.Add(this.btnBao);
            this.Controls.Add(this.btnKeo);
            this.Load += new System.EventHandler(this.FormClient_Load);
            this.Text = "RPS Client";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
