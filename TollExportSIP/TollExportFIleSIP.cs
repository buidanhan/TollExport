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

namespace TollExportSIP
{
    public partial class TollExportFIleSIP : Form
    {
        public TollExportFIleSIP()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            //openFileDialog1.ShowDialog();
            folderBrowserDialog1.ShowDialog();
            txt_FileFath.Text = folderBrowserDialog1.SelectedPath;
            string path = folderBrowserDialog1.SelectedPath;

            DirectoryInfo dir = new DirectoryInfo(path);

            FileInfo[] filename = dir.GetFiles("*.*");

            DirectoryInfo[] filenaame = dir.GetDirectories("*.*");



        }
    }
}
