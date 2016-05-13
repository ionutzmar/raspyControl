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

        TcpClient client;
        Int32 port = 8887;
        NetworkStream stream;
        BackgroundWorker bw = new BackgroundWorker();
        Boolean connected = false;
        

        public mainForm()
        {
            InitializeComponent();
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (!connected)
                {
                    client = new TcpClient(Properties.Settings.Default.ipAdress, port);
                    stream = client.GetStream();
                    connected = true;
                    setText();
                }
                else
                {
                    client.Close();
                    stream.Close();
                    connected = false;
                    setText();
                }
            }
            catch (Exception ext)
            {
                connected = false;
                setText();
                MessageBox.Show("Something went wrong: " + ext.Message);

            }
        }
      
        private void setText()
        {
            if(connectButon.InvokeRequired)
            {
                connectButon.Invoke((Action)delegate{setText();});
                return;
            }
            if(!connected)
            {
                connectButon.Text = "Press to connect";
                status.Text = "Disconnected";
            }
            else
            {
                connectButon.Text = "Disconnect";
                status.Text = "Connected";
            }

        }
        private void mainForm_SizeChanged(object sender, EventArgs e)
        {
            gpioImage.Location = new Point((this.Width - gpioImage.Width) / 2, 80);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            setText();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();
      
            gpioImage.Location = new Point((this.Width - gpioImage.Width) / 2, 80);
            ipAddressTextBox.Text = Properties.Settings.Default.ipAdress;
        }

        private void connectButon_Click(object sender, EventArgs e)
        {
            connectButon.Text = "Wait...";
            Properties.Settings.Default.ipAdress = ipAddressTextBox.Text;
            Properties.Settings.Default.Save();

            if (!bw.IsBusy)
                bw.RunWorkerAsync();
            else
                MessageBox.Show("Try again in a few seconds!");
        }
    }
}
