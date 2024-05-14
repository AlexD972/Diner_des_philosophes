using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace TP2
{

    public partial class Form1 : Form
    {
        public Thread t;
        public Graphics g;
        Philosophe p1, p2, p3, p4, p5;
        Semaphore f1, f2, f3, f4, f5;

        //Brush
        public SolidBrush redBrush = new SolidBrush(Color.DarkRed);
        public SolidBrush whiteBrush = new SolidBrush(Color.White);
        public SolidBrush tanBrush = new SolidBrush(Color.Tan);

		public int X0;
		public int Y0;

		public int W;
		public int H;

		//Rayon
		public int R = 60;

		//Taille assiettes
		public int tailleA = 2;
		//Taille baguettes
		public int tailleBE = 4;//Epaisseur
		public int tailleBH = 2;//Hauteur

		public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            f1 = new Semaphore(1, 1);
            f2 = new Semaphore(1, 1);
            f3 = new Semaphore(1, 1);
            f4 = new Semaphore(1, 1);
            f5 = new Semaphore(1, 1);
			W = panel1.Width;
			H = panel1.Height;
			X0 = W / 2;
			Y0 = H / 2;
		}

        private void bDraw_Click(object sender, EventArgs e)
        {
            
            g.FillEllipse(redBrush, 0,0,W,H);

            for (int i = 0; i < 5; i++)
            {
                //Assiettes
                g.FillEllipse(whiteBrush, (float)(R * Math.Cos(2 * i * Math.PI / 5) + X0  - X0/(tailleA*2)), (float)(R * Math.Sin(2 * i * Math.PI / 5) + Y0 - Y0/(2*tailleA)), X0/tailleA, Y0/tailleA);
                //Baguettes
                g.FillEllipse(tanBrush, (float)(R * Math.Cos(2 * i * Math.PI / 5) + X0 - X0 / (tailleBH * 2)), (float)(R * Math.Sin(2* i * Math.PI / 5) + Y0 - Y0 / (2 * tailleBH)), X0 / (tailleBE), Y0 / (3*tailleBE));
            }
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            p1 = new Philosophe(f1,  f2, g, 1, textBox1, this);
            p2 = new Philosophe(f2, f3, g, 2, textBox2, this);
            p3 = new Philosophe(f3, f4, g, 3, textBox3, this);
            p4 = new Philosophe(f4, f5, g, 4, textBox4, this);
            p5 = new Philosophe(f5, f1, g, 5, textBox5, this);

        }

        private void bStop_Click(object sender, EventArgs e)
        {
            p1.OK = false;
            p2.OK = false;
            p3.OK = false;
            p4.OK = false;
            p5.OK = false;

        }
    }
}
