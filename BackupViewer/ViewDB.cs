using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace BackupViewer
{
    public partial class ViewDB : Form
    {
        private const String GetTableNames = "Select name From sqlite_master where type='table' order by name;";
        private string ConnectionString { get; set; }
        public ViewDB(String path)
        {
            InitializeComponent();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (directoryInfo.Exists)
            {
                BuildTree(directoryInfo, treeView.Nodes);
            }
        }

        private void BuildTree(DirectoryInfo directoryInfo, TreeNodeCollection addInMe)
        {
            TreeNode curNode = addInMe.Add(directoryInfo.Name);

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                curNode.Nodes.Add(file.FullName, file.Name);
            }

            foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
            {
                BuildTree(subdir, curNode.Nodes);
            }
        }


        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Name.EndsWith(".db"))
            {
                String file = e.Node.Name;
                ConnectionString = $"Data Source={file};Compress=True;";
                SQLiteConnection myConnection = new SQLiteConnection(ConnectionString);
                
                myConnection.Open();
                
                SQLiteCommand getTables = new SQLiteCommand(GetTableNames, myConnection);
                
                SQLiteDataAdapter myCountAdapter = new SQLiteDataAdapter(getTables);
                DataTable f = new DataTable();
                myCountAdapter.Fill(f);
                bindingSource.DataSource = f;
                listBox.DataSource = bindingSource;
                listBox.DisplayMember = "name";

                String selectFromFirstTable = $"SELECT * FROM {((DataRowView) bindingSource.Current)["name"]}";
                SQLiteCommand getTables1 = new SQLiteCommand(selectFromFirstTable, myConnection);
                SQLiteDataAdapter myCountAdapter1 = new SQLiteDataAdapter(getTables1);
                DataSet myCountDataSet = new DataSet();
                myCountAdapter1.Fill(myCountDataSet);
                dataGridView.DataSource = myCountDataSet.Tables[0];
                
                myConnection.Close();
            }
        }

        private void FillDataGrid(String tableName)
        {
            SQLiteConnection myconnection = new SQLiteConnection(ConnectionString);
            myconnection.Open();
            
            SQLiteCommand getTableData =
                new SQLiteCommand($"Select * From {tableName};", myconnection);

            SQLiteDataAdapter myCountAdapter1 = new SQLiteDataAdapter(getTableData);
            DataSet myCountDataSet = new DataSet();
            myCountAdapter1.Fill(myCountDataSet);
            dataGridView.DataSource = myCountDataSet.Tables[0];
            myconnection.Close(); 
        }

        private bool IsReturningToMainForm { get; set; } 
        private void New_Click(object sender, EventArgs e)
        {
            IsReturningToMainForm = true;
            Owner.Show();
            Close();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                if (bindingSource.Current is DataRowView drv)
                {
                    String str = drv["name"].ToString();
                    FillDataGrid(str);
                }
            }
        }


        private void ViewDB_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsReturningToMainForm) return;
            if(e.CloseReason!= CloseReason.FormOwnerClosing)
                Owner.Close();
        }
    }
}