using System.Net.WebSockets;
using System.Text;

namespace CLientApp
{
    public partial class frmMain : Form
    {
        private ClientWebSocket socket;
        public frmMain()
        {
            InitializeComponent();
            socket = new ClientWebSocket();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            var url = new Uri(txtHost.Text);
            await socket.ConnectAsync(url, CancellationToken.None);
            var buffer = new byte[1024];
            while (socket.State==WebSocketState.Open)
            {
                btnConnect.Text = "Connected";
                btnConnect.Enabled = false;
                var result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                if (result.MessageType==WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                    AddMessage("Client", "Connection Closed");
                    btnConnect.Text = "Connect"; 
                    btnConnect.Enabled = true;
                }
                else
                {
                    AddMessage("Server", Encoding.UTF8.GetString(buffer));
                }
            }

        }

        private void AddMessage(string sender, string message)
        {
            lstItems.Items.Add($"{sender}: {message}");

        }
    }
}