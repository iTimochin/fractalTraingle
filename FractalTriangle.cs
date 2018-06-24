using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;


namespace FractalTriangle
{
    public partial class FactorialTraiangle : Form
    {
        public FactorialTraiangle()
        {
            InitializeComponent();
            value2 = new Point(rnd.Next(0, C.X), rnd.Next(0, B.Y));
            WindowState = FormWindowState.Maximized;
            BackColor = Color.White;
        }

        Point A;
        Point B;
        Point C;
        int i = 0;
        int pointSize = 1;
        int delayTime = 1;
        int Lenght = 100;
        Random rnd = new Random();
        Point value2;

        private void button1_Click(object sender, EventArgs e)
        {
            A = new Point(this.Width / 2, 40);
            B = new Point(200, this.Height - 100);
            C = new Point(this.Width - 200, this.Height - 100);
            Lenght =(int)numericUDpointsNum.Value * 100;
            pointSize = (int)numericUDPointSize.Value;
            Form1_Paint(null, null);
        }

        private async void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics Print = CreateGraphics();
            Print.Clear(this.BackColor);
            Print.FillRectangle(new SolidBrush(Color.Black), new Rectangle(A.X, A.Y, 5, 5));
            Print.FillRectangle(new SolidBrush(Color.Black), new Rectangle(B.X, B.Y, 5, 5));
            Print.FillRectangle(new SolidBrush(Color.Black), new Rectangle(C.X - 5, C.Y - 5, 5, 5));
            Task val = Task.Run(() =>
            {
             
                for (i = 0; i < Lenght; i++)
                {                   
                    Thread.Sleep(delayTime);
                    switch (rnd.Next(1, 4))
                    {
                        case 1:
                            value2 = new Point((int)((value2.X + A.X) / 2), (int)((value2.Y + A.Y) / 2));
                            break;
                        case 2:
                            value2 = new Point((int)((value2.X + B.X) / 2), (int)((value2.Y + B.Y) / 2));
                            break;
                        case 3:
                            value2 = new Point((int)((value2.X + C.X) / 2), (int)((value2.Y + C.Y) / 2));
                            break;
                    }
                    Print.FillEllipse(new SolidBrush(Color.Black), new Rectangle(value2.X, value2.Y, pointSize,pointSize));

                }
            });
            await val;                 
        }

        private void numericUDDelay_ValueChanged(object sender, EventArgs e)
        {
            delayTime = (int)numericUDDelay.Value;
        }
    }
}
