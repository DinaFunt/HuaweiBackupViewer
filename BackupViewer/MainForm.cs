using System;
using System.IO;
using System.Windows.Forms;
using Ookii.Dialogs;

namespace BackupViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void pathIn_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();
            dlg.ShowNewFolderButton = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pathIn.Text = dlg.SelectedPath;
            }
        }

        private void pathOut_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();
            dlg.ShowNewFolderButton = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pathOut.Text = dlg.SelectedPath;
            }
        }

        private void StartDecrypting(object sender, EventArgs e)
        {
            if (pathIn.Text != "" && pathOut.Text != "" && password.Text != "")
            {
                String message = "";
                if (BackupDecryptor.TryDecrypt(pathIn.Text, pathOut.Text, password.Text, ref message))
                {
                    Form mod = new ViewDB(pathOut.Text);
                    mod.Location = Location;
                    mod.Owner = this;
                    mod.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Invalid parameters: " + message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Please fill the gaps", "Error");
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Activate();
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            password.PasswordChar = '\0';
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            password.PasswordChar = '*';
        }
    }
}