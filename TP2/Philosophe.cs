using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace TP2
{
    class Philosophe
    {
        public Thread t;
        Semaphore fourchette_g;
        Semaphore fourchette_d;
        bool OK = true;
        TextBox box;
		Panel panel;
		Form1 F;
        int i;

        public Philosophe(Semaphore fourchette_g, Semaphore fourchette_d, Panel panel, int i, TextBox box, Form1 F)
        {
            this.fourchette_d = fourchette_d;
            this.fourchette_g = fourchette_g;
            this.box = box;
            this.i = i;
            this.panel = panel;
            this.F = F;
            TextBox.CheckForIllegalCrossThreadCalls = false;
            t = new Thread(CodeThread);
            t.Start();
        }

		public void Stop()
		{
			OK = false;
		}

		void CodeThread()
        {
            while (OK)
            {
                //Penser
                box.Text = "Penser";
                Thread.Sleep(1000);
                fourchette_d.WaitOne();
				fourchette_g.WaitOne();
				//Manger
				box.Text = "Manger";
				using (Graphics g = panel.CreateGraphics())
				{
                    //Assiettes
                    g.FillEllipse(Brushes.Red, (float)(F.R * Math.Cos(2 * i * Math.PI / 5) + F.X0 - F.X0 / (F.tailleA * 2)), (float)(F.R * Math.Sin(2 * i * Math.PI / 5) + F.Y0 - F.Y0 / (2 * F.tailleA)), F.X0 / F.tailleA, F.Y0 / F.tailleA);
                    //Baguettes
                    g.FillEllipse(Brushes.Red, (float)(F.R * Math.Cos(2 * i * Math.PI / 5) + F.X0 - F.X0 / (F.tailleBH * 2)), (float)(F.R * Math.Sin(2 * i * Math.PI / 5) + F.Y0 - F.Y0 / (2 * F.tailleBH)), F.X0 / (F.tailleBE), F.Y0 / (3 * F.tailleBE));
                }
                Thread.Sleep(1000);
                fourchette_d.Release();
                fourchette_g.Release();
            }
        }

    }
}
