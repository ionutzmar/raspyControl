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
        int port = 8887;
        NetworkStream stream;
        BackgroundWorker bw = new BackgroundWorker();
        bool connected = false;
        byte[] dataOut = new byte[4];

        

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
            panelul.Location = new Point(Convert.ToInt32(this.Width * 1.5 / 4 - panelul.Width /  2), 80);

            
        }

        private void inOutPressed(object sender, EventArgs e)
        {
            if (connected)
            {
                try
                {
                    var button = (Button)sender;
                    int gpio = Convert.ToInt32(button.Name.Substring(15));
                    Console.WriteLine(gpio);
                    if (button.Text == "INPUT")
                        button.Text = "OUTPUT";
                    else
                        button.Text = "INPUT";
                    dataOut[3] = 101;
                    stream.Write(dataOut, 0, dataOut.Length);
                    
                }
                catch(Exception ext) //it catches the  second click...
                {
                    if (!bw.IsBusy)
                        bw.RunWorkerAsync();
                    MessageBox.Show("You need to be connected first!" + ext);
                }
            }
            else
                MessageBox.Show("You need to be connected first!");
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            setText();
            connectButon.Text = "Trying  to  connect...";
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();
      
            ipAddressTextBox.Text = Properties.Settings.Default.ipAdress;

            mainForm_SizeChanged(sender, e);

            gpioImage.Location = new Point((panelul.Width - gpioImage.Width) / 2, 0);

            //left side
            gpioLabel8.Location = new Point(gpioImage.Location.X - gpioLabel8.Width - 10, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 1.5 / 20 - gpioLabel8.Height / 2));
            gpioStatusButton8.Location = new Point(gpioLabel8.Location.X - gpioStatusButton8.Width - 10, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 1.5 / 20 - gpioStatusButton8.Height / 2));
            inOutButtonGpio8.Location = new Point(gpioStatusButton8.Location.X - inOutButtonGpio8.Width - 10, gpioStatusButton8.Location.Y);

            gpioLabel9.Location = new Point(gpioLabel8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 2.5 / 20 - gpioLabel9.Height / 2));
            gpioStatusButton9.Location = new Point(gpioStatusButton8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 2.5 / 20 - gpioStatusButton9.Height / 2));
            inOutButtonGpio9.Location = new Point(inOutButtonGpio8.Location.X, gpioStatusButton9.Location.Y);

            gpioLabel7.Location = new Point(gpioLabel8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 3.5 / 20 - gpioLabel7.Height / 2));
            gpioStatusButton7.Location = new Point(gpioStatusButton8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 3.5 / 20 - gpioStatusButton9.Height / 2));
            inOutButtonGpio7.Location = new Point(inOutButtonGpio8.Location.X, gpioStatusButton7.Location.Y);

            gpioLabel0.Location = new Point(gpioLabel8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 5.5 / 20 - gpioLabel9.Height / 2));
            gpioStatusButton0.Location = new Point(gpioStatusButton8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 5.5 / 20 - gpioStatusButton9.Height / 2));
            inOutButtonGpio0.Location = new Point(inOutButtonGpio8.Location.X, gpioStatusButton0.Location.Y);

            gpioLabel2.Location = new Point(gpioLabel8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 6.5 / 20 - gpioLabel9.Height / 2));
            gpioStatusButton2.Location = new Point(gpioStatusButton8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 6.5 / 20 - gpioStatusButton9.Height / 2));
            inOutButtonGpio2.Location = new Point(inOutButtonGpio8.Location.X, gpioStatusButton2.Location.Y);

            gpioLabel3.Location = new Point(gpioLabel8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 7.5 / 20 - gpioLabel9.Height / 2));
            gpioStatusButton3.Location = new Point(gpioStatusButton8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 7.5 / 20 - gpioStatusButton9.Height / 2));
            inOutButtonGpio3.Location = new Point(inOutButtonGpio8.Location.X, gpioStatusButton3.Location.Y);

            gpioLabel12.Location = new Point(gpioLabel8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 9.5 / 20 - gpioLabel9.Height / 2));
            gpioStatusButton12.Location = new Point(gpioStatusButton8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 9.5 / 20 - gpioStatusButton9.Height / 2));
            inOutButtonGpio12.Location = new Point(inOutButtonGpio8.Location.X, gpioStatusButton12.Location.Y);

            gpioLabel13.Location = new Point(gpioLabel8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 10.5 / 20 - gpioLabel9.Height / 2));
            gpioStatusButton13.Location = new Point(gpioStatusButton8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 10.5 / 20 - gpioStatusButton9.Height / 2));
            inOutButtonGpio13.Location = new Point(inOutButtonGpio8.Location.X, gpioStatusButton13.Location.Y);

            gpioLabel14.Location = new Point(gpioLabel8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 11.5 / 20 - gpioLabel9.Height / 2));
            gpioStatusButton14.Location = new Point(gpioStatusButton8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 11.5 / 20 - gpioStatusButton9.Height / 2));
            inOutButtonGpio14.Location = new Point(inOutButtonGpio8.Location.X, gpioStatusButton14.Location.Y);

            gpioLabel21.Location = new Point(gpioLabel8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 14.5 / 20 - gpioLabel9.Height / 2));
            gpioStatusButton21.Location = new Point(gpioStatusButton8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 14.5 / 20 - gpioStatusButton9.Height / 2));
            inOutButtonGpio21.Location = new Point(inOutButtonGpio8.Location.X, gpioStatusButton21.Location.Y);

            gpioLabel22.Location = new Point(gpioLabel8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 15.5 / 20 - gpioLabel9.Height / 2));
            gpioStatusButton22.Location = new Point(gpioStatusButton8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 15.5 / 20 - gpioStatusButton9.Height / 2));
            inOutButtonGpio22.Location = new Point(inOutButtonGpio8.Location.X, gpioStatusButton22.Location.Y);

            gpioLabel23.Location = new Point(gpioLabel8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 16.5 / 20 - gpioLabel9.Height / 2));
            gpioStatusButton23.Location = new Point(gpioStatusButton8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 16.5 / 20 - gpioStatusButton9.Height / 2));
            inOutButtonGpio23.Location = new Point(inOutButtonGpio8.Location.X, gpioStatusButton23.Location.Y);

            gpioLabel24.Location = new Point(gpioLabel8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 17.5 / 20 - gpioLabel9.Height / 2));
            gpioStatusButton24.Location = new Point(gpioStatusButton8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 17.5 / 20 - gpioStatusButton9.Height / 2));
            inOutButtonGpio24.Location = new Point(inOutButtonGpio8.Location.X, gpioStatusButton24.Location.Y);

            gpioLabel25.Location = new Point(gpioLabel8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 18.5 / 20 - gpioLabel9.Height / 2));
            gpioStatusButton25.Location = new Point(gpioStatusButton8.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 18.5 / 20 - gpioStatusButton9.Height / 2));
            inOutButtonGpio25.Location = new Point(inOutButtonGpio8.Location.X, gpioStatusButton25.Location.Y);

            //right side
            gpioLabel15.Location = new Point(gpioImage.Location.X + gpioImage.Width + 8, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 3.5 / 20 - gpioLabel8.Height / 2));
            gpioStatusButton15.Location = new Point(gpioLabel15.Location.X + gpioLabel15.Width + 10, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 3.5 / 20 - gpioStatusButton8.Height / 2));
            inOutButtonGpio15.Location = new Point(gpioStatusButton15.Location.X + gpioStatusButton15.Width + 10, gpioStatusButton15.Location.Y);

            gpioLabel16.Location = new Point(gpioLabel15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 4.5 / 20 - gpioLabel8.Height / 2));
            gpioStatusButton16.Location = new Point(gpioStatusButton15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 4.5 / 20 - gpioStatusButton8.Height / 2));
            inOutButtonGpio16.Location = new Point(inOutButtonGpio15.Location.X, gpioStatusButton16.Location.Y);

            gpioLabel1.Location = new Point(gpioLabel15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 5.5 / 20 - gpioLabel8.Height / 2));
            gpioStatusButton1.Location = new Point(gpioStatusButton15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 5.5 / 20 - gpioStatusButton8.Height / 2));
            inOutButtonGpio1.Location = new Point(inOutButtonGpio15.Location.X, gpioStatusButton1.Location.Y);

            gpioLabel4.Location = new Point(gpioLabel15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 7.5 / 20 - gpioLabel8.Height / 2));
            gpioStatusButton4.Location = new Point(gpioStatusButton15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 7.5 / 20 - gpioStatusButton8.Height / 2));
            inOutButtonGpio4.Location = new Point(inOutButtonGpio15.Location.X, gpioStatusButton4.Location.Y);

            gpioLabel5.Location = new Point(gpioLabel15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 8.5 / 20 - gpioLabel8.Height / 2));
            gpioStatusButton5.Location = new Point(gpioStatusButton15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 8.5 / 20 - gpioStatusButton8.Height / 2));
            inOutButtonGpio5.Location = new Point(inOutButtonGpio15.Location.X, gpioStatusButton5.Location.Y);

            gpioLabel6.Location = new Point(gpioLabel15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 10.5 / 20 - gpioLabel8.Height / 2));
            gpioStatusButton6.Location = new Point(gpioStatusButton15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 10.5 / 20 - gpioStatusButton8.Height / 2));
            inOutButtonGpio6.Location = new Point(inOutButtonGpio15.Location.X, gpioStatusButton6.Location.Y);

            gpioLabel10.Location = new Point(gpioLabel15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 11.5 / 20 - gpioLabel8.Height / 2));
            gpioStatusButton10.Location = new Point(gpioStatusButton15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 11.5 / 20 - gpioStatusButton8.Height / 2));
            inOutButtonGpio10.Location = new Point(inOutButtonGpio15.Location.X, gpioStatusButton10.Location.Y);

            gpioLabel11.Location = new Point(gpioLabel15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 12.5 / 20 - gpioLabel8.Height / 2));
            gpioStatusButton11.Location = new Point(gpioStatusButton15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 12.5 / 20 - gpioStatusButton8.Height / 2));
            inOutButtonGpio11.Location = new Point(inOutButtonGpio15.Location.X, gpioStatusButton11.Location.Y);

            gpioLabel26.Location = new Point(gpioLabel15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 15.5 / 20 - gpioLabel8.Height / 2));
            gpioStatusButton26.Location = new Point(gpioStatusButton15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 15.5 / 20 - gpioStatusButton8.Height / 2));
            inOutButtonGpio26.Location = new Point(inOutButtonGpio15.Location.X, gpioStatusButton26.Location.Y);

            gpioLabel27.Location = new Point(gpioLabel15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 17.5 / 20 - gpioLabel8.Height / 2));
            gpioStatusButton27.Location = new Point(gpioStatusButton15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 17.5 / 20 - gpioStatusButton8.Height / 2));
            inOutButtonGpio27.Location = new Point(inOutButtonGpio15.Location.X, gpioStatusButton27.Location.Y);

            gpioLabel28.Location = new Point(gpioLabel15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 18.5 / 20 - gpioLabel8.Height / 2));
            gpioStatusButton28.Location = new Point(gpioStatusButton15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 18.5 / 20 - gpioStatusButton8.Height / 2));
            inOutButtonGpio28.Location = new Point(inOutButtonGpio15.Location.X, gpioStatusButton28.Location.Y);

            gpioLabel29.Location = new Point(gpioLabel15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 19.5 / 20 - gpioLabel8.Height / 2));
            gpioStatusButton29.Location = new Point(gpioStatusButton15.Location.X, Convert.ToInt32(gpioImage.Location.Y + gpioImage.Height * 19.5 / 20 - gpioStatusButton8.Height / 2));
            inOutButtonGpio29.Location = new Point(inOutButtonGpio15.Location.X, gpioStatusButton29.Location.Y);

        }

        private void connectButon_Click(object sender, EventArgs e)
        {
            connectButon.Text = "Trying to connect...";
            Properties.Settings.Default.ipAdress = ipAddressTextBox.Text;
            Properties.Settings.Default.Save();

            if (!bw.IsBusy)
                bw.RunWorkerAsync();
            else
                MessageBox.Show("Try again in a few seconds!");
        }
    }
}
