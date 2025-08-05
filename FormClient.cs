using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace RPS_Client
{
    public partial class FormClient : Form
    {
        private TcpClient client;
        private NetworkStream stream;

        public FormClient()
        {
            InitializeComponent();
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            txtServerIP.Text = "127.0.0.1";
            txtResult.Text = "Vui lòng kết nối đến Server.";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient(txtServerIP.Text.Trim(), 8888);
                stream = client.GetStream();
                txtResult.Text = "✅ Kết nối thành công!";
                btnConnect.Enabled = false;
            }
            catch (Exception ex)
            {
                txtResult.Text = "❌ Không thể kết nối: " + ex.Message;
            }
        }

        private void SendChoice(string choice)
        {
            try
            {
                if (stream == null)
                {
                    txtResult.Text = "❌ Chưa kết nối đến Server!";
                    return;
                }

                // Gửi lựa chọn
                byte[] data = Encoding.UTF8.GetBytes(choice);
                stream.Write(data, 0, data.Length);

                // Nhận phản hồi
                byte[] buffer = new byte[1024];
                int bytes = stream.Read(buffer, 0, buffer.Length);
                string result = Encoding.UTF8.GetString(buffer, 0, bytes);

                txtResult.Text = $"📩 {result}";
            }
            catch (Exception ex)
            {
                txtResult.Text = "❌ Lỗi: " + ex.Message;
            }
        }

        private void btnBua_Click(object sender, EventArgs e) => SendChoice("Búa");
        private void btnBao_Click(object sender, EventArgs e) => SendChoice("Bao");
        private void btnKeo_Click(object sender, EventArgs e) => SendChoice("Kéo");
    }
}
