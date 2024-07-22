using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stego.UI
{
    public partial class WindowPanel : UserControl
    {
        private Point offset;
        private bool Minimized = false;
        public const int ZIndexForNotSelected = 1;
        public const int ZIndexForSelected = 0;

        public WindowPanel()
        {
            InitializeComponent();
            TopBarPanel.MouseDown += TopBarPanel_MouseDown;
            TopBarPanel.MouseMove += TopBarPanel_MouseMove;

            ImagePanel.AllowDrop = true;
            ImagePanel.DragEnter += ImagePanel_DragEnter;
            ImagePanel.DragDrop += ImagePanel_DragDrop;
            ImagePanel.DragLeave += ImagePanel_DragLeave;
            PictureBox.MouseClick += PictureBox_MouseClick;

            CornerResizePanel.MouseDown += CornerResizePanel_MouseDown;
            CornerResizePanel.MouseMove += CornerResizePanel_MouseMove;

            WindowWidth.Value = Width;
            WindowHeight.Value = Height;

            CornerResizePanelBounds = new Rectangle(CornerResizePanel.Location, CornerResizePanel.Size);
        }

        private void CornerResizePanel_MouseDown(object sender, MouseEventArgs e)
        {
            offset = new Point(e.X, e.Y);
        }

        int MinimumWidth = 100;
        int MinimumHeight = 100;

        Rectangle CornerResizePanelBounds;
        Rectangle? mouseDownLocation = new Rectangle(0, 0, 0, 0);

        private void CornerResizePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.X == mouseDownLocation.Value.X && e.Y == mouseDownLocation.Value.Y)
                {
                    offset = new Point(e.X, e.Y);
                    return;
                }
            }
        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Save Gambar");
                toolStripMenuItem.Click += ToolStripMenuItem_Click;
                ToolStripMenuItem DecodeMenuItem = new ToolStripMenuItem("Decode Gambar");
                DecodeMenuItem.Click += DecodeMenuItem_Click;

                contextMenuStrip.Items.Add(toolStripMenuItem);
                contextMenuStrip.Items.Add(DecodeMenuItem);
                contextMenuStrip.Show(Cursor.Position);
            }
        }

        private void DecodeMenuItem_Click(object sender, EventArgs e)
        {
            DecodeForm decodeForm = new DecodeForm();
            decodeForm.Init(PictureBox.Image as Bitmap);
            decodeForm.ShowDialog();
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            // bmp
            saveFileDialog.Filter = "Bitmap Image|*.bmp";

            saveFileDialog.Title = "Save Image";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                PictureBox.Image.Save(saveFileDialog.FileName);
            }
        }

        private void ImagePanel_DragLeave(object sender, EventArgs e)
        {
            ImagePanel.BackColor = Color.White;
        }

        private void ImagePanel_DragDrop(object sender, DragEventArgs e)
        {
            ImagePanel.BackColor = Color.White;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            Task.Run(() =>
            {
                // RunInUI Thread
                Invoke(new Action(() =>
                {
                    EmbedForm embedForm = new EmbedForm();
                    embedForm.SetBMP(PictureBox.Image as Bitmap);
                    embedForm.Init(files);
                    embedForm.ShowDialog();
                }));
            });
        }

        private void ImagePanel_DragEnter(object sender, DragEventArgs e)
        {
            ImagePanel.BackColor = Color.LightGray;
            e.Effect = DragDropEffects.Copy;
        }

        private void TopBarPanel_MouseDown(object sender, MouseEventArgs e)
        {
            offset = new Point(e.X, e.Y);
        }

        private void TopBarPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Parent.Controls.SetChildIndex(this, ZIndexForSelected);
                for (int i = 0; i < Parent.Controls.Count; i++)
                {
                    if (Parent.Controls[i] is WindowPanel && Parent.Controls[i] != this)
                    {
                        Parent.Controls.SetChildIndex(Parent.Controls[i], ZIndexForNotSelected);
                    }
                }

                Point newLocation = Location;
                newLocation.X += e.X - offset.X;
                newLocation.Y += e.Y - offset.Y;

                // Perform bounds check
                if (newLocation.X < 0)
                    newLocation.X = 0;
                if (newLocation.Y < 0)
                    newLocation.Y = 0;

                // Width, and Height visible pixels must not under 100px
                int overflowW = (int)(Width * 0.95);
                int overflowH = (int)(Height * 0.95);

                if (Minimized)
                {
                    overflowH = 0;
                }

                if (newLocation.X + Width > Parent.Width + overflowW)
                {
                    newLocation.X = Parent.Width + overflowW - Width;
                }

                if (newLocation.Y + Height > Parent.Height + overflowH)
                {
                    newLocation.Y = Parent.Height + overflowH - Height;
                }

                Location = newLocation;
            }
        }

        public void SetImage(string bitmap)
        {
            Bitmap bmp = Bitmap.FromFile(bitmap) as Bitmap;
            if (bmp.PixelFormat != PixelFormat.Format24bppRgb)
            {
                Bitmap clone = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb);
                using (Graphics gr = Graphics.FromImage(clone))
                {
                    gr.DrawImage(bmp, new Rectangle(0, 0, clone.Width, clone.Height));
                }
                bmp = clone;
            }

            PictureBox.Image = bmp;
            PictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            string fileName = bitmap.Substring(bitmap.LastIndexOf("\\") + 1);
            label1.Text = fileName;

            // for (int i = 0; i < Parent.Controls.Count; i++)
            // {
            //     if (Parent.Controls[i] is WindowPanel && Parent.Controls[i] != this)
            //     {
            //         Parent.Controls.SetChildIndex(Parent.Controls[i], ZIndexForNotSelected);
            //     }
            // }
        }

        public void SetImage(Bitmap bitmap, string title = "")
        {
            PictureBox.Image = bitmap;
            PictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            label1.Text = title;

            // for (int i = 0; i < Parent.Controls.Count; i++)
            // {
            //     if (Parent.Controls[i] is WindowPanel && Parent.Controls[i] != this)
            //     {
            //         Parent.Controls.SetChildIndex(Parent.Controls[i], ZIndexForNotSelected);
            //     }
            // }
        }

        public void PutOnMiddle()
        {
            Location = new Point((Parent.Width - Width) / 2, (Parent.Height - Height) / 2);
        }

        internal int Clamp(int value, int min, int max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        private void WindowWidth_ValueChanged(object sender, EventArgs e)
        {
            WindowWidth.Value = Clamp((int)WindowWidth.Value, 100, 1920);
            Width = (int)WindowWidth.Value;
        }

        private void WindowHeight_ValueChanged(object sender, EventArgs e)
        {
            WindowHeight.Value = Clamp((int)WindowHeight.Value, 100, 1080);
            if (Minimized) return;

            Height = (int)WindowHeight.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // close user control
            Parent.Controls.Remove(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Size topBar = TopBarPanel.Size;
            Minimized = !Minimized;

            if (Minimized)
            {
                Size = topBar;
            }
            else
            {
                Size = new Size((int)WindowWidth.Value, (int)WindowHeight.Value);
            }
        }
    }
}
