using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace H_1_c_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int c = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //label1.Text = "gesu";
            label3.Text = "That's a circle";
            label2.Text = "That's a line";
            label4.Text = "That's a Point";
            label1.Text = "That's a Rectangle";



            // for future use
            //Graphics gra = this.panel3.CreateGraphics();
            //Pen blackPen = new Pen(Color.Red, 3);
            //PointF pnt1 = new PointF(50.0F, 50.0F);
            //PointF pnt2 = new PointF(500.0F, 200.0F);
            //gra.DrawLine(blackPen, pnt1, pnt2);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //Graphics gra = this.panel1.CreateGraphics();
            Pen blackPen = new Pen(Color.Black, 3);

            PointF pnt1 = new PointF(100.0F, 100.0F);
            PointF pnt2 = new PointF(500.0F, 200.0F);

            // e.Graphics.DrawLine(blackPen,pnt1, pnt2);
            Rectangle r = new Rectangle(50, 50, 200, 50);
            e.Graphics.DrawRectangle(blackPen,r);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Graphics gra = this.panel3.CreateGraphics();
            
            Pen blackPen = new Pen(Color.Red, 3);

            PointF pnt1 = new PointF(50.0F, 50.0F);
            PointF pnt2 = new PointF(500.0F, 200.0F);

            

            e.Graphics.DrawLine(blackPen, pnt1, pnt2);
            
           
          
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
           
            Pen blackPen = new Pen(Color.Blue, 3);
            //                               x    y    h    w
            e.Graphics.DrawEllipse(blackPen, 0, 0, 150, 150);

           

            SolidBrush brush = new SolidBrush(Color.Blue);

            
            e.Graphics.FillEllipse(brush,50,50,3,3);




        }
    }
}
