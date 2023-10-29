using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Text.Unicode;

namespace CLientApp
{
    public partial class frmMain : Form
    {
        string fileName = $"{Application.StartupPath}/host.txt";
        private ClientWebSocket? socket;
        private string ClientId="";
        public frmMain()
        {
            InitializeComponent();
            ReadHostUrl();
        }

        private void ReadHostUrl()
        {
            if (!File.Exists(fileName))
                return;
            string host = File.ReadAllText(fileName);
            txtHost.Text = host;
 
        }
        private void SaveHostUrl()
        {
            File.WriteAllText(fileName,txtHost.Text);
            
        }
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                AddLog("Sending Connection request");
                socket = new ClientWebSocket();
                var url = new Uri($"ws://{txtHost.Text}/messages");
                await socket.ConnectAsync(url, CancellationToken.None);
                var buffer = new Byte[1024 * 4];
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                this.ClientId = Encoding.UTF8.GetString(buffer, 0, result.Count);
                SetUIButtons(true);
                await RecieveMessages();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SetUIButtons(bool isConnected)
        {
            btnConnect.Enabled = !isConnected;
            btnPing.Enabled = btnStartWork.Enabled  = isConnected;
            if (isConnected)
                SaveHostUrl();
        }

        private async Task RecieveMessages()
        {
            try
            {
                while (socket!=null && socket.State == WebSocketState.Open)
                {
                    var buffer = new Byte[1024 * 4];
                    var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        
                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                        SetUIButtons(false);
                    }
                    else
                    {
                        lstItems.Items.Add($"Server: {Encoding.UTF8.GetString(buffer)}");
                        lstItems.SelectedIndex = lstItems.Items.Count - 1;
                    }

                }
            }
            
            catch(Exception ex)
            {
                if (socket?.State == WebSocketState.Closed)
                    SetUIButtons(false);
               else 
                    MessageBox.Show(ex.Message);
            }
        }

        private async void btnPing_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("ClientId", ClientId);
            AddLog("Send Ping Request");
            var result = await client.PostAsync($"http://{txtHost.Text}/ping", null);
            AddLog("Server Responded with code: " + result.StatusCode.ToString());
        }

        private void AddLog(string log)
        {
            string msg = $"{DateTime.Now}: {log}";
            lstLog.Items.Add(msg);
            lstLog.SelectedIndex=lstLog.Items.Count - 1;
               
        }

        private async void btnStartWork_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("ClientId", ClientId);
            AddLog("Sending work/start request");
            var result = await client.PostAsync($"http://{txtHost.Text}/work/start", null);
            AddLog("Server Responded with code: " + result.StatusCode.ToString());
        }

        private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
    }
}