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
    public partial class ListItemPanelResult : UserControl
    {
        private byte[] Data = new byte[0];
        private string FileName { get; set; }

        public ListItemPanelResult()
        {
            InitializeComponent();

            // right click context
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Save file");
            toolStripMenuItem.Click += (sender, e) =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = FileName;
                saveFileDialog.Filter = "Bitmap Image|*.bmp";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllBytes(saveFileDialog.FileName, Data);
                    MessageBox.Show("File saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            contextMenuStrip.Items.Add(toolStripMenuItem);
        }

        public void SetIndex(int index)
        {
            label1.Text = index.ToString() + ".";
        }

        public void SetFile(string file)
        {
            label2.Text = file;
            FileName = file;
        }

        public void SetFileByte(byte[] fileByte)
        {
            Data = fileByte;
        }

        public byte[] GetFileByte()
        {
            return Data;
        }

        public string GetFileName()
        {
            return FileName;
        }
    }
}
