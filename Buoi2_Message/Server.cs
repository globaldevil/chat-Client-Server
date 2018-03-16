using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Buoi2_Message
{
    public partial class Server : Form
    {
        Socket server, client;
        byte[] data;
        public Server()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            listBox1.Items.Add(text);
            textBox1.Text = "";
            data = new byte[1024];
            data = Encoding.ASCII.GetBytes(text);
            client.Send(data);
            data = new byte[1024];
            client.Receive(data);
            listBox1.Items.Add(Encoding.ASCII.GetString(data));
        }

        private void Server_Load(object sender, EventArgs e)
        {
            IPEndPoint ipServer = new IPEndPoint(IPAddress.Any, 9050);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ipServer);
            server.Listen(5);
            client = server.Accept();
            IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
            textBox2.Text = ip.Address.ToString();
            data = new byte[1024];
            client.Receive(data);
            listBox1.Items.Add(Encoding.ASCII.GetString(data));
        }
    }
}
