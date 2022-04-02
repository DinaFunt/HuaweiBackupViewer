using System;
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
            if (pathIn.Text == "")
            {
                MessageBox.Show("Please enter your backup path", "Error");
                return;
            }

            if (pathOut.Text == "")
            {
                MessageBox.Show("Please enter path for result files", "Error");
                return;
            }

            if (password.Text == "")
            {
                MessageBox.Show("Please enter path for result files", "Error");
                return;
            }

            String message = "";
            if (BackupDecryptor.TryDecrypt(pathIn.Text, pathOut.Text, password.Text, ref message))
            {
                Form view = new ViewDB(pathOut.Text);
                view.Location = Location;
                view.Owner = this;
                view.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Invalid parameters: " + message, "Error");
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