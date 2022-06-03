using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace TollExportSIP
{
    public partial class TollExportFIleSIP : Form
    {
        public TollExportFIleSIP()
        {
            InitializeComponent();
            progressBar1.Visible = false;
            labelProgess.Visible = false;
            label_checkbox.Visible = false;
            checkBoxALL.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            folderBrowserDialog1.ShowDialog();
            txt_FileFath.Text = folderBrowserDialog1.SelectedPath;
            string path = folderBrowserDialog1.SelectedPath;
            string[] arrListStr = path.Split('\\');
            TreeNode node = new TreeNode();
            node.Text = arrListStr[arrListStr.Length - 1];
            treeView1.CheckBoxes = true;
            checkBoxALL.Checked = false;
            treeView1.Nodes.Add(node);
            LoadExplorer(node, path);

        }
        void LoadExplorer(TreeNode root, string pPath)
        {
            if(root == null)
            {
                return;
            }
            try
            {
                DirectoryInfo dir = new DirectoryInfo(pPath);
                var file = dir.GetFileSystemInfos("*.*");
                foreach (var item in file)
                {
                    if (!backgroundWorker1.IsBusy)
                    {
                        backgroundWorker1.RunWorkerAsync();
                        labelProgess.Show();
                        progressBar1.Show();
                    }
                    if (Directory.Exists(item.FullName))
                    {
                        TreeNode node = new TreeNode() { Text = item.Name };
                        root.Nodes.Add(node);
                        LoadExplorer(node, item.FullName);
                    }
                    else
                    {
                        TreeNode node = new TreeNode() { Text = item.Name };
                        root.Nodes.Add(node);
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for(int i = 0; i <= 100; i++)
            {
                Thread.Sleep(10);
                backgroundWorker1.WorkerReportsProgress = true;
                backgroundWorker1.ReportProgress(i);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            labelProgess.Text = e.ProgressPercentage.ToString() + "%";
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 100;
            progressBar1.Visible = false;
            labelProgess.Visible = false;
            label_checkbox.Show();
            checkBoxALL.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var tree = treeView1;
            foreach (TreeNode item in tree.Nodes)
            {
                item.Checked = !item.Checked;
                if(item.Nodes.Count > 0)
                {
                    foreach (TreeNode item2 in item.Nodes)
                    {
                        CheckNode(item2);
                    }
                }
            }
        }
        void CheckNode(TreeNode node)
        {
            node.Checked = !node.Checked;
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode item2 in node.Nodes)
                {
                    CheckNode(item2);
                }
            }
        }
    }
}
