using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace RaspberryControl
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            Int32 port = 8887;
            string ip = "192.168.1.200";
            TcpClient client = new TcpClient(ip, port);
            NetworkStream stream = client.GetStream();
            byte[] data = {1, 2, 5, 55, 45, 48};
            for(int i = 0; i < 10; i++)
                stream.Write(data, 0, data.Length);
        }

        private void mainForm_SizeChanged(object sender, EventArgs e)
        {
            gpioImage.Location = new Point(this.Width / 2, 100);
        }
    }
}
