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
    public partial class ListItemPanel : UserControl
    {
        public byte[] Data = new byte[0];

        public ListItemPanel()
        {
            InitializeComponent();
        }

        public void SetIndex(int index)
        {
            label1.Text = index.ToString() + ".";
        }

        public void SetFile(string file)
        {
            label2.Text = file;
        }

        public void SetFileByte(byte[] fileByte)
        {
            Data = fileByte;
        }
    }
}
