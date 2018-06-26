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
    public partial class FactorialTraeangle : Form
    {
        /// <summary>
        /// Defoult window constructor
        /// </summary>
        public FactorialTraeangle()
        {
            InitializeComponent();            
            //maximize window size
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(this.Width, this.Height);                        
            //set random point
            newPoint = new Point(rnd.Next(200, this.Width), rnd.Next(0, this.Height));
            //set back color
            BackColor = Color.White;
            radioButtonTreangle.Checked = true;
            radioButtonTreangle_CheckedChanged(null, null);
        }
        /// <summary>
        /// Global 3 starting points
        /// </summary>
        Point A;
        Point B;
        Point C;
        Point D;
        /// <summary>
        /// num of drown points
        /// </summary>
        int iterations = 0;
        /// <summary>
        /// Defoult point size
        /// </summary>
        int pointSize = 1;
        /// <summary>
        /// Defoult delay time
        /// </summary>
        int delayTime = 1;
        /// <summary>
        /// Defoult max num of points
        /// </summary>
        int Lenght = 100;
        Random rnd = new Random();
        Point newPoint;
        /// <summary>
        /// Set all data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //set random point
            newPoint = new Point(rnd.Next(200, this.Width), rnd.Next(0, this.Height));                      
            //set max number of points
            Lenght =(int)numericUDpointsNum.Value * 100;
            //set point size
            pointSize = (int)numericUDPointSize.Value;
            //invoce paint event
            if(!radioButtonPapor.Checked)
            {
                Form_Paint(null, null);
            }
            else
            {
                DrawBerns();
            }
        }
        /// <summary>
        /// Paint event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics Print = CreateGraphics();
            //clear print area with back color
            Print.Clear(this.BackColor);
            Print.FillEllipse(new SolidBrush(Color.Green), new Rectangle(newPoint.X, newPoint.Y, 10, 10));
            //draw 3 swtarted points
            Print.FillEllipse(new SolidBrush(Color.Red), new Rectangle(A.X, A.Y, 10, 10));
            Print.FillEllipse(new SolidBrush(Color.Red), new Rectangle(B.X, B.Y, 10, 10));
            Print.FillEllipse(new SolidBrush(Color.Red), new Rectangle(C.X - 5, C.Y - 5, 10, 10));
            if (radioButtonSquare.Checked)
            {
                Print.FillEllipse(new SolidBrush(Color.Red), new Rectangle(D.X - 5, D.Y - 5, 10, 10));
            }
            //run task asynchronusly
            Task val = Task.Run(() =>
            {
             //drawing async loop
                for (iterations = 0; iterations < Lenght; iterations++)
                {                   
                    //delay time
                    Thread.Sleep(delayTime);
                    //randomly select one of way
                    if (radioButtonTreangle.Checked)
                    {
                        TreanglePoints();
                    }
                    else
                    {
                        SquarePoints();                        
                    }                    
                    LabelUpdate("i = " + iterations.ToString());
                    //draw point
                    Print.FillEllipse(new SolidBrush(Color.Black), new Rectangle(newPoint.X, newPoint.Y, pointSize,pointSize));
                }
            });
            //Waiting of retuning async value - in this cause it NULL
            await val;                 
        }
        /// <summary>
        /// Set delay time 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUDDelay_ValueChanged(object sender, EventArgs e)
        {
            delayTime = (int)numericUDDelay.Value;
        }
        /// <summary>
        /// Random oint treangle select
        /// </summary>
        private void TreanglePoints()
        {
            switch (rnd.Next(1, 4))
            {
                case 1:
                    newPoint = new Point((int)((newPoint.X + A.X) / 2), (int)((newPoint.Y + A.Y) / 2));
                    break;
                case 2:
                    newPoint = new Point((int)((newPoint.X + B.X) / 2), (int)((newPoint.Y + B.Y) / 2));
                    break;
                case 3:
                    newPoint = new Point((int)((newPoint.X + C.X) / 2), (int)((newPoint.Y + C.Y) / 2));
                    break;
            }
        }
        /// <summary>
        /// Random square point select
        /// </summary>
        private void SquarePoints()
        {
            switch (rnd.Next(1, 5))
            {
                case 1:
                    newPoint = new Point((int)((newPoint.X + A.X) / 2.3)+A.X/3, (int)((newPoint.Y + A.Y) / 2.3) + A.Y/2);
                    break;
                case 2:
                    newPoint = new Point((int)((newPoint.X + B.X) /2.3) + A.X / 3, (int)((newPoint.Y + B.Y) / 2.3) +A.Y / 2);
                    break;
                case 3:
                    newPoint = new Point((int)((newPoint.X + C.X) / 2.3) + A.X /3, (int)((newPoint.Y + C.Y) / 2.3) +A.Y / 2);
                    break;
                case 4:
                    newPoint = new Point((int)((newPoint.X + D.X) / 2.3) + A.X / 3, (int)((newPoint.Y + D.Y) / 2.3)+A.Y / 2);
                    break;
            }
        }
        /// <summary>
        /// Step-by-step iterations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonIteration_Click(object sender, EventArgs e)
        {           
            Graphics Print = CreateGraphics();
            if (radioButtonTreangle.Checked)
            {
                TreanglePoints();
            }
            else
            {
                SquarePoints();
            }
            iterations++;
            LabelUpdate("i = " + iterations.ToString());   
            //draw point
            Print.FillEllipse(new SolidBrush(Color.Black), new Rectangle(newPoint.X, newPoint.Y, pointSize, pointSize));
        }
        /// <summary>
        /// Set treangle start data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonTreangle_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTreangle.Checked == true)
            {
                A = new Point(this.Width / 2, 40);
                B = new Point(200, this.Height - 100);
                C = new Point(this.Width - 200, this.Height - 100);
                Graphics Print = CreateGraphics();
                Print.Clear(this.BackColor);
                Print.FillEllipse(new SolidBrush(Color.Green), new Rectangle(newPoint.X, newPoint.Y, 10, 10));
                //draw 3 started points
                Print.FillEllipse(new SolidBrush(Color.Red), new Rectangle(A.X, A.Y, 10, 10));
                Print.FillEllipse(new SolidBrush(Color.Red), new Rectangle(B.X, B.Y, 10, 10));
                Print.FillEllipse(new SolidBrush(Color.Red), new Rectangle(C.X - 5, C.Y - 5, 10, 10));
            }
        }
        /// <summary>
        /// Set square start data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonSquare_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSquare.Checked == true)
            {              
                A = new Point(300, 100);
                B = new Point(this.Width - 100, 100);
                C = new Point(300, this.Height - 100);
                D = new Point(this.Width -100, this.Height - 100);
                Graphics Print = CreateGraphics();
                Print.Clear(this.BackColor);
                Print.FillEllipse(new SolidBrush(Color.Green), new Rectangle(newPoint.X, newPoint.Y, 10, 10));
                //draw 4 started points
                Print.FillEllipse(new SolidBrush(Color.Red), new Rectangle(A.X, A.Y, 10, 10));
                Print.FillEllipse(new SolidBrush(Color.Red), new Rectangle(B.X, B.Y, 10, 10));
                Print.FillEllipse(new SolidBrush(Color.Red), new Rectangle(C.X - 5, C.Y - 5, 10, 10));
                Print.FillEllipse(new SolidBrush(Color.Red), new Rectangle(D.X - 5, D.Y - 5, 10, 10));
            }
        }

        /// <summary>
        /// udate control safe
        /// </summary>
        /// <param name="message"></param>
        private void LabelUpdate(string message)
        {
            if (LabelIterations.InvokeRequired)
            {
                LabelIterations.BeginInvoke(new Action(() => 
                    { LabelIterations.Text = message; }));
            }
            else
            {
                LabelIterations.Text = message;
            }
        }

        //=====================================================================================================
        // Minimum range of current points
        private const float MinX = -6;
        private const float MaxX = 6;
        private const float MinY = 0.1f;
        private const float MaxY = 10;

        /// <summary>
        /// Probability of point certain area hit
        /// </summary>
        private float[] _probability = new float[4]
        {
            0.01f,
            0.06f,
            0.08f,
            0.85f
        };
        // Cooficient matrix
        private float[,] _funcCoef = new float[4, 6]
        {
            //a      b       c      d      e  f
            {0,      0,      0,     0.16f, 0, 0   }, // 1 функция
            {-0.15f, 0.28f,  0.26f, 0.24f, 0, 0.44f},// 2 функция
            {0.2f,  -0.26f,  0.23f, 0.22f, 0, 1.6f}, // 3 функция
            {0.85f,  0.04f, -0.04f, 0.85f, 0, 1.6f}  // 4 функция
        };

        // cooficient of width and height scale
        private int _width;
        private int _height;
        /// <summary>
        /// Async Berns draw
        /// </summary>
        private async void DrawBerns()
        {
            _width = (int)(this.Width / (MaxX - MinX + 1));
            _height = (int)(this.Height / (MaxY - MinY + 1));
            // set start poit (0, 0)
            float xtemp = 0, ytemp = 0;
            // veriable for containing  
            int numF = 0;
            Graphics Print = CreateGraphics();
            Print.Clear(this.BackColor);
            Task val = Task.Run(() =>
            {
                    
                for (iterations = 0; iterations <= Lenght; iterations++)
                {
                    Thread.Sleep(delayTime);
                    // rand number 0 to 1
                    var num = rnd.NextDouble();
                    // choosing of function to compute next point
                    for (int j = 0; j <= 3; j++)
                    {
                        // if random num is <= 0 
                        // then coof of probability,
                        // assign function number
                        num = num - _probability[j];
                        if (num <= 0)
                        {
                            numF = j;
                            break;
                        }
                    }

                    // compute coordinate
                    var x = _funcCoef[numF, 0] * xtemp + _funcCoef[numF, 1] * ytemp + _funcCoef[numF, 4];
                    var y = _funcCoef[numF, 2] * xtemp + _funcCoef[numF, 3] * ytemp + _funcCoef[numF, 5];

                    // safe valus for next iteration
                    xtemp = x;
                    ytemp = y;
                    // compute value of pixel point
                    x = (int)(xtemp * _width + this.Width / 2);
                    y = (int)(ytemp * _height);//+ (int)(this.Height * 0.1);
                    Print.FillEllipse(new SolidBrush(Color.Black), new Rectangle((int)x, (int)y, pointSize, pointSize));
                    LabelUpdate("i = " + iterations.ToString());
                }
            });
            await val;
        }
        /// <summary>
        /// set staring data for DrawBerns function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonPapor_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPapor.Checked)
            {
                Graphics Print = CreateGraphics();
                Print.Clear(this.BackColor);
            }
        }
    }
}
