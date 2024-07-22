using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stego.UI
{
    public partial class EmbedForm : Form
    {
        readonly List<string> files = new List<string>();
        private Bitmap bmp;

        public EmbedForm()
        {
            InitializeComponent();

            // make flowLayoutPanel1 scrollable
            flowLayoutPanel1.AutoScroll = true;

            // make flowLayoutPanel1 wrap contents
            flowLayoutPanel1.WrapContents = true;

            // make flowLayoutPanel1 files draggable
            flowLayoutPanel1.AllowDrop = true;
            flowLayoutPanel1.DragEnter += FlowLayoutPanel1_DragEnter;
            flowLayoutPanel1.DragDrop += FlowLayoutPanel1_DragDrop;

            button2.Click += Button2_Click;
        }

        public void SetBMP(Bitmap bmp)
        {
            this.bmp = bmp;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                int lsbMode;
                if (radioButton1.Checked)
                {
                    lsbMode = 1;
                }
                else if (radioButton2.Checked)
                {
                    lsbMode = 2;
                }
                else
                {
                    throw new Exception("Invalid lsb mode");
                }

                string tmpZipPath = System.IO.Path.GetTempFileName();
                using (var zip = new Ionic.Zip.ZipFile())
                {
                    foreach (var file in files)
                    {
                        zip.AddFile(file, "");
                    }
                    zip.Save(tmpZipPath);
                }

                var zipBytes = System.IO.File.ReadAllBytes(tmpZipPath);
                var resultBitmap = Utils.Embed(bmp, zipBytes, lsbMode, textBox1.Text);

                MainForm.MakeNewWindow(resultBitmap);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FlowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            Init(files);
        }

        private void FlowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        public void Init(string[] files)
        {

            this.files.AddRange(files);
            int index = 1;
            flowLayoutPanel1.Controls.Clear();
            foreach (var file in this.files)
            {
                ListItemPanel panel = new ListItemPanel();
                panel.SetFile(file);
                panel.SetIndex(index++);

                flowLayoutPanel1.Controls.Add(panel);
            }
        }
    }
}
