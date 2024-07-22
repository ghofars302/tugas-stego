using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip;

namespace Stego.UI
{
    public partial class DecodeForm : Form
    {
        public Bitmap bmp;

        public DecodeForm()
        {
            InitializeComponent();

            radioButton1.Checked = true;
            radioButton1.CheckedChanged += RadioButton1_CheckedChanged;
            radioButton2.CheckedChanged += RadioButton1_CheckedChanged;
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;
            }
            else if (radioButton2.Checked)
            {
                radioButton1.Checked = false;
            }
        }

        public void Init(Bitmap bmp)
        {
            this.bmp = bmp;
        }

        private void button1_Click(object sender, EventArgs e)
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

                var zipResultByte = Utils.Extract(bmp, lsbMode, textBox1.Text);
                using (var zipStream = new System.IO.MemoryStream(zipResultByte))
                {
                    using (var zip = ZipFile.Read(zipStream))
                    {
                        var list = new List<ResultItem>();
                        foreach (var entry in zip)
                        {
                            var item = new ResultItem
                            {
                                FileName = entry.FileName,
                                FileByte = new byte[entry.UncompressedSize]
                            };

                            using (var stream = entry.OpenReader())
                            {
                                stream.Read(item.FileByte, 0, item.FileByte.Length);
                            }

                            list.Add(item);
                        }

                        Invoke(new Action(() =>
                        {
                            var form = new DecodeResultForm();
                            form.ShowList(list);
                            form.Show();
                            form.BringToFront();
                        }));

                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
