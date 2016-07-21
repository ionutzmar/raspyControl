using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaspberryControl
{
    public partial class authForm : Form
    {
        private controlForm mainForm = null;

        public authForm(Form callingForm)
        {
            mainForm = callingForm as controlForm;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ipTextBox.Text = Properties.Settings.Default.ipAdress;
            passwordTextBox.Text = Properties.Settings.Default.password;
            usernameTextBox.Text = Properties.Settings.Default.username;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            loginButton.Text = "Trying to connect...";
            Properties.Settings.Default.ipAdress = ipTextBox.Text;
            Properties.Settings.Default.username = usernameTextBox.Text;
            if (rememberPasswordCheckBox.Checked)
                Properties.Settings.Default.password = passwordTextBox.Text;
            else
                Properties.Settings.Default.password = "";
            Properties.Settings.Default.Save();


            if (!mainForm.connected)
                mainForm.setButtons();
            if (!mainForm.bw.IsBusy)
                mainForm.bw.RunWorkerAsync();
            else
                MessageBox.Show("Try again in a few seconds!");

        }

        internal void setLoginButtonText(string text)
        {
            if(loginButton.InvokeRequired)
            {
                loginButton.Invoke((Action) delegate {setLoginButtonText(text);});
                return;
            }
            loginButton.Text = text;
        }
        private void authForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();///////////////////////////////////////////////////////////
        }
    }
}
