using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace paint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            setsize();
        }
        private class arraeypoint
        {
            private int index = 0;
            private Point[] points;

            public arraeypoint(int size)
            {
                if (size <= 0) { size = 2; }
                    points = new Point[size];
            }

            public void setpoint(int x,int y)
            {
                if (index >= points.Length)
                {
                    index = 0;
                }
                points[index] = new Point(x, y);
                index++;
            }
            public void resetpoints()
            {
                index = 0;
            }
          public int Getcountpoints()
            {
                return index;
            }
            public Point[] GetPoints()
            {
                return points;
            }
        }
        private bool ismouse = false;
        private arraeypoint Arraeypoint = new arraeypoint(2);

        Bitmap map = new Bitmap(100, 100);
        Graphics graphics;

        Pen pen = new Pen(Color.Black, 3f);

        private void setsize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(map);

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ismouse = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            ismouse = false;
            Arraeypoint.resetpoints();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!ismouse) { return; }
            Arraeypoint.setpoint(e.X, e.Y);
            if (Arraeypoint.Getcountpoints() >= 2)
            {
                graphics.DrawLines(pen,Arraeypoint.GetPoints());
                pictureBox1.Image = map;
                Arraeypoint.setpoint(e.X, e.Y); 
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                {
                    ((Button)sender).BackColor = colorDialog1.Color;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = map;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG(*.JPG|*.jpg";
            if(saveFileDialog1.ShowDialog()== DialogResult.OK)
            {
                if (pictureBox1.Image == null)
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName);
                }
            }
        }
    }
}
