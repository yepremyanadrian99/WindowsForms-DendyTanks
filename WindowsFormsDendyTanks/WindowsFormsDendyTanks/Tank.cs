using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsDendyTanks
{
    class Tank
    {
        private Form1 fr;
        private Field fd;
        public Rectangle rec;

        public int Health { get; set; }
        public string Way { set; get; }

        public Tank(Form1 fr, Field fd)
        {
            this.fr = fr;
            this.fd = fd;
            rec = new Rectangle(fd.x + fd.w, fd.y + fd.w * 20, fd.w * 2, fd.w * 2);
            Way = "up";
            Health = 100;
        }

        public void Paint(Graphics g)
        {
            if (Way == "up") g.DrawImage(Image.FromFile("Pics/0.png"), rec);
            else if (Way == "down") g.DrawImage(Image.FromFile("Pics/0_d.png"), rec);
            else if (Way == "right") g.DrawImage(Image.FromFile("Pics/0_r.png"), rec);
            else if (Way == "left") g.DrawImage(Image.FromFile("Pics/0_l.png"), rec);
            //g.FillRectangle(Brushes.Black, rec);
        }
    }
}