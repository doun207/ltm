using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RPS_Server
{
    public partial class FormServer : Form
    {
        private TcpListener listener;
        private Thread listenThread;
        private bool isRunning = false;
        private static readonly Random rand = new Random();

        public FormServer()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                int port = 8888;
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                isRunning = true;

                listenThread = new Thread(ListenForClients);
                listenThread.IsBackground = true;
                listenThread.Start();

                lblStatus.Text = "✅ Server đang lắng nghe...";
                lstLog.Items.Add($"Server started on port {port}");
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể khởi động server: " + ex.Message);
            }
        }

        private void ListenForClients()
        {
            while (isRunning)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
                catch
                {
                    if (!isRunning) break;
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[1024];

                    while (isRunning)
                    {
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0) break; // Client đóng kết nối

                        string clientChoice = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

                        string[] choices = { "Kéo", "Búa", "Bao" };
                        string serverChoice;
                        lock (rand)
                        {
                            serverChoice = choices[rand.Next(choices.Length)];
                        }

                        string result = GetResult(clientChoice, serverChoice);
                        string response = $"Server chọn: {serverChoice} | Kết quả: {result}";

                        byte[] responseData = Encoding.UTF8.GetBytes(response);
                        stream.Write(responseData, 0, responseData.Length);

                        Invoke((MethodInvoker)delegate
                        {
                            lstLog.Items.Add($"Client: {clientChoice} | Server: {serverChoice} => {result}");
                        });
                    }
                }
            }
            catch
            {
                // Không log lỗi khi client đóng kết nối bình thường
            }
            finally
            {
                client.Close();
            }
        }

        private string GetResult(string client, string server)
        {
            if (client == server) return "Hòa";
            if ((client == "Kéo" && server == "Bao") ||
                (client == "Búa" && server == "Kéo") ||
                (client == "Bao" && server == "Búa"))
                return "Bạn thắng";
            else
                return "Server thắng";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            isRunning = false;
            listener?.Stop();
            listenThread?.Join();

            lblStatus.Text = "❌ Server đã dừng";
            lstLog.Items.Add("Server stopped.");
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }
    }
}
