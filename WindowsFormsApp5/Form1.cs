using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Ink;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CheckBox[,] s = new CheckBox[50, 50]; // массив с кнопками

        int[,] t = new int[50, 50]; // массив с колвом соседей в каждой кнопке

        private void Form1_Load(object sender, EventArgs e)
        {

            int sz = 15;

            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if (i == 0 || i == 49 || j == 0 || j == 49)
                    {
                        s[i, j] = new CheckBox { Size = new Size(sz, sz), Location = new Point(i * sz, j * sz), Enabled = true, Visible = false };
                        continue;
                    }
                    s[i, j] = new CheckBox { Size = new Size(sz, sz), Location = new Point(i * sz, j * sz), Enabled = true};
                }
            }
            foreach (CheckBox item in s)
            {
                this.Controls.Add(item);
                
            }
        }

        static void Check(CheckBox checkBox, ref int num)
        {
            if (checkBox.CheckState == CheckState.Indeterminate)
                num += 1;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var item in s)
            {
                if (item.CheckState == CheckState.Checked)
                    item.CheckState = CheckState.Indeterminate;
            }
               
            for (int i = 1; i < 49; i++)
            {
                for (int j = 1; j < 49; j++)
                {
                    int num = 0;

                    Check(s[i - 1, j], ref num);

                    Check(s[i - 1, j - 1], ref num);

                    Check(s[i - 1, j + 1], ref num);

                    Check(s[i, j - 1], ref num);

                    Check(s[i, j + 1], ref num);

                    Check(s[i + 1, j], ref num);

                    Check(s[i + 1, j - 1], ref num);

                    Check(s[i + 1, j + 1], ref num);

                    t[i, j] = num;
                }
            }

            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {

                    if (t[i, j] == 3) // если соседей три она появляется
                    {
                        s[i, j].CheckState = CheckState.Indeterminate;
                    }
                    
                    else if (t[i, j] < 2 || t[i, j] > 3)  // если соседей меньше двух или больше то клетка умирает
                    {
                        s[i, j].CheckState = CheckState.Unchecked;
                    }  
                    
                }
            }
            t = null;
            t = new int[50, 50];
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
            timer1.Stop();
            foreach (var item in s)
            {
                item.CheckState = CheckState.Unchecked;
            }
        }
    }
}
