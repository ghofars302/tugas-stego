using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using Ionic.Zip;
using FastBitmapLib;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Stego.UI;

namespace Stego
{
    public partial class MainForm : Form
    {
        // START: Change window border
        // The enum flag for DwmSetWindowAttribute's second parameter, which tells the function what attribute to set.
        // Copied from dwmapi.h
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }
        // The DWM_WINDOW_CORNER_PREFERENCE enum for DwmSetWindowAttribute's third parameter, which tells the function
        // what value of the enum to set.
        // Copied from dwmapi.h
        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }
        // Import dwmapi.dll and define DwmSetWindowAttribute in C# corresponding to the native function.
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void DwmSetWindowAttribute(IntPtr hwnd,
                                                         DWMWINDOWATTRIBUTE attribute,
                                                         ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute,
                                                         uint cbAttribute);

        static MainForm mainForm;

        public MainForm()
        {
            InitializeComponent();
            mainForm = this;

            // enable drag in panel1
            panel1.AllowDrop = true;
            panel1.DragEnter += Panel1_DragEnter;
            panel1.DragDrop += Panel1_DragDrop;
            panel1.DragLeave += Panel1_DragLeave;

            // disable window round corner
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_DONOTROUND; // DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND
            DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));
        }

        public static void MakeNewWindow(Bitmap bmp)
        {
            mainForm.Invoke(new Action(() =>
            {
                WindowPanel windowPanel = new WindowPanel();
                windowPanel.SetImage(bmp, "Stegano Result");
                mainForm.panel1.Controls.Add(windowPanel);

                windowPanel.BringToFront();
                windowPanel.PutOnMiddle();
            }));
        }

        private void Panel1_DragLeave(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
        }

        private void Panel1_DragDrop(object sender, DragEventArgs e)
        {
            panel1.BackColor = Color.White;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (file.EndsWith(".png") || file.EndsWith(".bmp") || file.EndsWith(".jpg") || file.EndsWith(".jpeg"))
                {
                    WindowPanel windowPanel = new WindowPanel();
                    windowPanel.SetImage(file);
                    panel1.Controls.Add(windowPanel);
                }
                else
                {
                    MessageBox.Show("File yang di-drop harus berformat PNG, BMP, JPG, atau JPEG", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Panel1_DragEnter(object sender, DragEventArgs e)
        {
            panel1.BackColor = Color.LightGray;
            e.Effect = DragDropEffects.Copy;
        }

        private void bukaGambarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png, *.bmp, *.jpeg, *.jpg)|*.png;*.bmp;*.jpeg;*.jpg|PNG Image|*.png|Bitmap Image|*.bmp|JPEG Image|*.jpeg|JPG Image|*.jpg";
            openFileDialog.Title = "Open Image";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                WindowPanel windowPanel = new WindowPanel();
                windowPanel.SetImage(openFileDialog.FileName);
                panel1.Controls.Add(windowPanel);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            Form1 form = new Form1();
            form.ShowDialog();
        }
    }
}
