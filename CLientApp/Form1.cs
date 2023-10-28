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
        private ClientWebSocket socket;
        private string ClientId;
        public frmMain()
        {
            InitializeComponent();

            txtHost.Text = "ws://localhost:5000/messages";
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                socket = new ClientWebSocket();
                var url = new Uri(txtHost.Text);
                await socket.ConnectAsync(url, CancellationToken.None);
                var buffer = new Byte[1024 * 4];
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                ClientId = Encoding.UTF8.GetString(buffer, 0, result.Count);
                this.Text = ClientId;
                await RecieveMessages();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private async Task RecieveMessages()
        {

            while (socket.State == WebSocketState.Open)
            {
                var buffer = new Byte[1024 * 4];
                btnConnect.Text = "Connected";
                btnConnect.Enabled = false;
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                    btnConnect.Text = "Connect";
                    btnConnect.Enabled = true;
                }
                else
                {
                    lstItems.Items.Add($"Server: {Encoding.UTF8.GetString(buffer)}");
                    lstItems.SelectedIndex = lstItems.Items.Count - 1;
                }

            }
        }

        private async void btnPing_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("ClientId", ClientId);
            var result = await client.PostAsync("http://localhost:5000/ping", null);
        }

        private void btnStartWork_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("ClientId", ClientId);
            var result = client.PostAsync("http://localhost:5000/work/start", null);
        }
    }
}