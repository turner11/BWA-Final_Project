using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompareBytes
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog _fDialog;
        public Form1()
        {
            InitializeComponent();
            this._fDialog = new FolderBrowserDialog();
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.Parent;
            

            this.txbFolder1.Text = CompareBytes.Properties.Settings.Default.Folder1;
            this.txbFolder2.Text = CompareBytes.Properties.Settings.Default.Folder2;

            if (Directory.Exists(this.txbFolder1.Text))
            {
                this._fDialog.SelectedPath = this.txbFolder1.Text;
            }
            
            1.ToString();
            //chr18.fa.bwt //19519320

        }

        

        private void btnFolder_Click(object sender, EventArgs e)
        {
            var txb = (sender as Button).Name == this.btnFolder1.Name ?
                this.txbFolder1 : this.txbFolder2;
            if(this._fDialog.ShowDialog() == DialogResult.OK)
            {
                txb.Text = this._fDialog.SelectedPath;
            }

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            var folder1 = this.txbFolder1.Text;
            var folder2 = this.txbFolder2.Text;
            if (!Directory.Exists(folder1))
            {
                this.label1.Text = "Folder 1 does not exist.";
                return;
            }
            if (!Directory.Exists(folder2))
            {
                this.label1.Text = "Folder 2 does not exist.";
                return;
            }

            CompareBytes.Properties.Settings.Default.Folder1 = this.txbFolder1.Text;
            CompareBytes.Properties.Settings.Default.Folder2 = this.txbFolder2.Text;
            CompareBytes.Properties.Settings.Default.Save();
            try
            {


                this.label1.Text = String.Empty;
                List<string> files1 = Directory.EnumerateFiles(folder1).ToList();
                List<string> files2 = Directory.EnumerateFiles(folder2).ToList();
                if (files1.Count != files2.Count)
                {
                    this.label1.Text = String.Format("Folder 1 had {0} files, while folder 2 has {1} files",
                        files1.Count,
                        files2.Count);
                }
                var count = files1.Count;
                var bytes1 = new List<byte[]>(count);
                var bytes2 = new List<byte[]>(count);


                files1.ForEach(f => bytes1.Add(File.ReadAllBytes(f)));
                files2.ForEach(f => bytes2.Add(File.ReadAllBytes(f)));


                var noMatches = bytes1.Where(b => !bytes2.Any(bs=> bs.SequenceEqual(b))).ToList();//bytes1.All(b => bytes2.Contains(b)) && bytes2.All(b => bytes1.Contains(b));
                bool match = noMatches.Count == 0;
                this.label1.Text = match ? "Match" : String.Format("No match! Count ({0})", noMatches.Count);
            }
            catch (Exception ex)
            {

                this.label1.Text = ex.Message;

            }


            

            
        }

       
    }
}
