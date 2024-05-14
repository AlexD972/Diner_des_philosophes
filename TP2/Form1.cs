using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace TP2
{

    public partial class Form1 : Form
    {
        public Thread t;
        public Graphics g;
		Philosophe[] philosophes = new Philosophe[5];
		Semaphore[] fourchettes = new Semaphore[5];

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
			for (int i = 0; i < 5; i++)
			{
				fourchettes[i] = new Semaphore(1, 1);
			}
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
			TextBox[] boxes = { textBox1, textBox2, textBox3, textBox4, textBox5 };
			for (int i = 0; i < 5; i++)
			{
				philosophes[i] = new Philosophe(fourchettes[i], fourchettes[(i + 1) % 5], panel1, i, boxes[i], this);
			}

		}

        private void bStop_Click(object sender, EventArgs e)
        {
			foreach (var philosophe in philosophes)
			{
				philosophe?.Stop();
			}
		}
    }
}
