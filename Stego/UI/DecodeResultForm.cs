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
    public partial class DecodeResultForm : Form
    {
        public DecodeResultForm()
        {
            InitializeComponent();
        }

        public void ShowList(List<ResultItem> list)
        {
            // clear existing items
            flowLayoutPanel1.Controls.Clear();

            // add new items
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var lip = new ListItemPanelResult();
                lip.SetIndex(i + 1);
                lip.SetFile(item.FileName);
                lip.SetFileByte(item.FileByte);

                flowLayoutPanel1.Controls.Add(lip);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Open directory dialog
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select a folder to save all the files";
            folderBrowserDialog.ShowNewFolderButton = true;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (ListItemPanelResult listItemPanel in flowLayoutPanel1.Controls)
                {
                    string filePath = System.IO.Path.Combine(folderBrowserDialog.SelectedPath, listItemPanel.GetFileName());
                    System.IO.File.WriteAllBytes(filePath, listItemPanel.GetFileByte());
                }

                MessageBox.Show("Files saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
    public class ResultItem
    {
        public string FileName { set; get; }
        public byte[] FileByte { set; get; }
    }

}
