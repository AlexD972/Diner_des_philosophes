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
        public bool OK = true;
        TextBox box;
        Graphics g;
        Form1 F;
        int i;
        public Philosophe(Panel panel1, Semaphore fourchette_g, Semaphore fourchette_d, Graphics g, int i, TextBox box, Form1 F)
        {
            this.fourchette_d = fourchette_d;
            this.fourchette_g = fourchette_g;
            this.box = box;
            this.F = F;
            this.i = i;
            this.g = g;
            TextBox.CheckForIllegalCrossThreadCalls = false;
            t = new Thread(CodeThread);
            t.Start();
        }
        void CodeThread()
        {
            while (OK)
            {
                //Penser
                box.Text = "Penser";
                Thread.Sleep(1000);
                fourchette_d.WaitOne();
				//Assiettes
				g.FillEllipse(Brushes.Red, (float)(F.R * Math.Cos(2 * i * Math.PI / 5) + F.X0 - F.X0 / (F.tailleA * 2)), (float)(F.R * Math.Sin(2 * i * Math.PI / 5) + F.Y0 - F.Y0 / (2 * F.tailleA)), F.X0 / F.tailleA, F.Y0 / F.tailleA);
				//Baguettes
				g.FillEllipse(Brushes.Red, (float)(F.R * Math.Cos(2 * i * Math.PI / 5) + F.X0 - F.X0 / (F.tailleBH * 2)), (float)(F.R * Math.Sin(2 * i * Math.PI / 5) + F.Y0 - F.Y0 / (2 * F.tailleBH)), F.X0 / (F.tailleBE), F.Y0 / (3 * F.tailleBE));
				fourchette_g.WaitOne();
                //Manger
                box.Text = "Manger";
                Thread.Sleep(1000);
                fourchette_d.Release();
                fourchette_g.Release();
            }
        }

    }
}
