using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsDendyTanks
{
    class Star
    {
        private Form1 fr;
        private Field fd;
        public Rectangle rec;
        public int Health { get; set; }

        public Star(Form1 fr, Field fd)
        {
            this.fr = fr;
            this.fd=fd;
            rec = new Rectangle(fd.x + fd.w * 21, fd.y + fd.w * 20, 60, 60);
            Health = 100;
        }

        public void Paint(Graphics g)
        {
            g.DrawImage(Image.FromFile("Pics/star.png"), rec);
        }
    }
}