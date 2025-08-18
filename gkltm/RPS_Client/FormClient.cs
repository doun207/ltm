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

            btnConnect.Click += btnConnect_Click;
            btnBua.Click += btnBua_Click;
            btnBao.Click += btnBao_Click;
            btnKeo.Click += btnKeo_Click;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient(txtServerIP.Text.Trim(), 8888);
                stream = client.GetStream();
                txtResult.Text = "✅ Kết nối thành công đến Server.";
            }
            catch (Exception ex)
            {
                txtResult.Text = "❌ Không thể kết nối: " + ex.Message;
            }
        }

        private void SendChoice(string choice)
        {
            if (client == null || !client.Connected)
            {
                txtResult.Text = "❌ Vui lòng kết nối đến Server trước.";
                return;
            }

            try
            {
                Console.WriteLine($"[CLIENT] Chuẩn bị gửi lựa chọn: {choice}");

                // Sử dụng stream hiện tại
                byte[] data = Encoding.UTF8.GetBytes(choice);
                stream.Write(data, 0, data.Length);

                Console.WriteLine($"[CLIENT] Đã gửi: {choice}");

                // Nhận phản hồi
                byte[] buffer = new byte[1024];
                int bytes = stream.Read(buffer, 0, buffer.Length);
                string result = Encoding.UTF8.GetString(buffer, 0, bytes);

                Console.WriteLine($"[CLIENT] Nhận từ server: {result}");
                txtResult.Text = $"📩 Kết quả:\n{result}";
            }
            catch (Exception ex)
            {
                txtResult.Text = "❌ Lỗi giao tiếp: " + ex.Message;
                Console.WriteLine($"[CLIENT] Lỗi khi gửi dữ liệu: {ex.Message}");
            }
        }

        private void btnBua_Click(object sender, EventArgs e)
        {
            SendChoice("Búa");
        }

        private void btnBao_Click(object sender, EventArgs e)
        {
            SendChoice("Bao");
        }

        private void btnKeo_Click(object sender, EventArgs e)
        {
            SendChoice("Kéo");
        }

        private void txtServerIP_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void txtResult_TextChanged(object sender, EventArgs e) { }
        private void lblStatus_Click(object sender, EventArgs e) { }
    }
}