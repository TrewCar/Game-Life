using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Random rand = new Random();

        private Graphics g;

        private sbyte[,] Pixels;

        private sbyte[,] Sosedi; // массив с колвом соседей в каждой кнопке

        private int RectX;
        private int RectY;

        private int sz = 10;

        private void Form1_Load(object sender, EventArgs e)
        {
            RectX = this.Width / sz;
            RectY = this.Height / sz;

            Pixels = new sbyte[RectX, RectY];
            Sosedi = new sbyte[RectX, RectY];

            NewPicture();
            g = Graphics.FromImage(pictureBox1.Image);
        }
        private void Check(sbyte Pixel, ref sbyte num) => num += Pixel != 1 ? (sbyte)0 : (sbyte)1;

        private void Ccheck(int x, int y)
        {
            Sosedi[x, y] = (sbyte)-Pixels[x, y];

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Check(Pixels[x + i, y + j], ref Sosedi[x, y]);
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Parallel.For(1, RectX - 1, x => Parallel.For(1, RectY - 1, y => Ccheck(x, y)));

            for (int i = 1; i < RectX - 1; i++)
            {
                for (int j = 1; j < RectY - 1; j++)
                {
                    if (Sosedi[i, j] == 3)
                    {
                        g.FillRectangle(new SolidBrush(Color.Blue), i * sz, j * sz, sz, sz);
                        Pixels[i, j] = 1;
                    }
                    else if (Sosedi[i, j] < 2 || Sosedi[i, j] > 3)
                    {
                        g.FillRectangle(new SolidBrush(Color.Black), i * sz, j * sz, sz, sz);
                        Pixels[i, j] = 0;
                    }
                }
            }
            pictureBox1.Invalidate();
            Sosedi = new sbyte[RectX, RectY];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        private void NewPicture() => pictureBox1.Image = new Bitmap(this.Width, this.Height);
        private void button3_Click(object sender, EventArgs e)
        {
            NewPicture();

            g = Graphics.FromImage(pictureBox1.Image);
            for (int i = 0; i < RectX; i++)
            {
                for (int j = 0; j < RectY; j++)
                {
                    int r = rand.Next(0, 101);

                    if (r <= 30)
                    {
                        Pixels[i, j] = 1;
                        g.FillRectangle(new SolidBrush(Color.Blue), i * sz, j * sz, sz, sz);
                    }
                }
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int First = e.X / sz;
                int Second = e.Y / sz;

                g.FillRectangle(new SolidBrush(Color.Blue), First * sz, Second * sz, sz, sz);
                pictureBox1.Invalidate();
                Pixels[First, Second] = 1;
            }
            else if (e.Button == MouseButtons.Right)
            {
                int First = e.X / sz;
                int Second = e.Y / sz;

                g.FillRectangle(new SolidBrush(Color.Black), First * sz, Second * sz, sz, sz);
                pictureBox1.Invalidate();
                Pixels[First, Second] = 0;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            NewPicture();

            g = Graphics.FromImage(pictureBox1.Image);
            Pixels = new sbyte[RectX, RectY];
            Sosedi = new sbyte[RectX, RectY];
        }
    }
}
