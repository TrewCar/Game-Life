using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Ink;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;





//g.FillRectangle(new SolidBrush(Color.Blue), (crd - 1) * sz, (crd - 1) * sz,sz,sz);





namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random rand = new Random();

        Graphics g;
 
        int[,] Pixels;

        int[,] Sosedi; // массив с колвом соседей в каждой кнопке

        int RectX;
        int RectY;

        int sz = 10;

        private void Form1_Load(object sender, EventArgs e)
        {                     
            RectX = this.Width / sz;
            RectY = this.Height / sz;

            
            Pixels = new int[RectX, RectY];
            Sosedi = new int[RectX, RectY];

            Bitmap bmp = new Bitmap(this.Width, this.Height);

            pictureBox1.Image = bmp;

            g = Graphics.FromImage(pictureBox1.Image);
                   
        }

        static void Check(int Pixel, ref int num)
        {
            if (Pixel == 1)
                num += 1;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 1; i < RectX-1; i++)
            {
                for (int j = 1; j < RectY-1; j++)
                {
                    int num = 0;

                    Check(Pixels[i - 1, j], ref num);

                    Check(Pixels[i - 1, j - 1], ref num);

                    Check(Pixels[i - 1, j + 1], ref num);

                    Check(Pixels[i, j - 1], ref num);

                    Check(Pixels[i, j + 1], ref num);

                    Check(Pixels[i + 1, j], ref num);

                    Check(Pixels[i + 1, j - 1], ref num);

                    Check(Pixels[i + 1, j + 1], ref num);

                    Sosedi[i, j] = num;
                }
            }

            for (int i = 0; i < RectX; i++)
            {
                for (int j = 0; j < RectY; j++)
                {

                    if (Sosedi[i, j] == 3) // если соседей три она появляется
                        Pixels[i, j] = 1;
                    
                    else if (Sosedi[i, j] < 2 || Sosedi[i, j] > 3)  // если соседей меньше двух или больше то клетка умирает                   
                        Pixels[i, j] = 0;

                    if (Pixels[i, j] == 1)
                        g.FillRectangle(new SolidBrush(Color.Blue), i * sz, j * sz, sz, sz);
                    if (Pixels[i, j] == 0)
                        g.FillRectangle(new SolidBrush(Color.Black), i * sz, j * sz, sz, sz);

                }
            }
            pictureBox1.Invalidate();
            Sosedi = null;
            Sosedi = new int[RectX, RectY];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);

            pictureBox1.Image = bmp;

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
                    else if (r > 50)
                    {
                        Pixels[i, j] = 0;
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);

            pictureBox1.Image = bmp;

            g = Graphics.FromImage(pictureBox1.Image);
            Pixels = new int[RectX, RectY];
            Sosedi = new int[RectX, RectY];
        }
    }
}
