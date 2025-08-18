using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace RPS_Server
{
    public partial class FormServer : Form
    {
        private TcpListener listener;
        private Thread listenThread;
        private bool isRunning = false;

        public FormServer()
        {
            InitializeComponent();
            lblStatus.Text = "Server chưa khởi động";
            btnStop.Enabled = false;
            btnStart.Click += btnStart_Click;
    
            lstLog.SelectedIndexChanged += lstLog_SelectedIndexChanged;
            lblStatus.Click += lblStatus_Click;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int port = 8888;
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            isRunning = true;
            listenThread = new Thread(new ThreadStart(ListenForClients));
            listenThread.IsBackground = true;
            listenThread.Start();

            lblStatus.Text = "Đang lắng nghe...";
            lstLog.Items.Add("Server started on port " + port);
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void ListenForClients()
        {
            while (isRunning)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Thread clientThread = new Thread(() => HandleClientComm(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
                catch
                {
                    if (!isRunning) break;
                }
            }
        }
        private static readonly Random rand = new Random();

        private void HandleClientComm(TcpClient client)
        {
            Console.WriteLine($"[THREAD] Thread ID: {Thread.CurrentThread.ManagedThreadId}");

            try
            {
                using (NetworkStream clientStream = client.GetStream())
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = clientStream.Read(buffer, 0, buffer.Length);
                    Console.WriteLine($"[FLAG] Đã đọc {bytesRead} byte từ client");

                    if (bytesRead > 0)
                    {
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
                        clientStream.Write(responseData, 0, responseData.Length);
                        

                        Invoke((MethodInvoker)delegate
                        {
                            string logMsg = $"Client chọn: {clientChoice} - Server chọn: {serverChoice} => {result}";
                            
                            lstLog.Items.Add(logMsg);
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)delegate
                {
                    lstLog.Items.Add("Client disconnected hoặc lỗi: " + ex.Message);
                });
                
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
            listener.Stop();
            listenThread?.Join();
            lblStatus.Text = "Server đã dừng";
            lstLog.Items.Add("Server stopped.");
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void lstLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tùy chọn: mở rộng nếu muốn hiện chi tiết log
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {
            MessageBox.Show(lblStatus.Text);
        }
    }
}
