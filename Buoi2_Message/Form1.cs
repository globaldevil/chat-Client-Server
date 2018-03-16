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
    public partial class FrmChat : Form
    {
        Socket client;
        byte[] data;
        IPEndPoint ipServer;
        public FrmChat()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ipServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(ipServer);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            data = new byte[1024];
            data = Encoding.ASCII.GetBytes(text);
            textBox1.Text = "";
            listBox1.Items.Add(text);
            client.Send(data);
            data = new byte[1024];
            client.Receive(data);
            text = Encoding.ASCII.GetString(data);
            listBox1.Items.Add(text);
            IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
            textBox2.Text = ip.Address.ToString();
        }
    }
}
