using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace WindowsFormsDendyTanks
{
    class EnemyWeapon
    {
        private Form1 fr;
        private Field fd;
        private Enemy en;
        private Star st;
        public Rectangle rec;
        public Thread th;
        public bool move = false;
        string uxx;
        bool show = false;

        public EnemyWeapon(Form1 fr, Field fd, Enemy en, Star st)
        {
            this.fr = fr;
            this.fd = fd;
            this.en = en;
            this.st = st;
            rec = new Rectangle();
            rec.Width = 15;
            rec.Height = 15;
            th = new Thread(new ThreadStart(Run));
        }

        private void Run()
        {
            for (; ; )
            {
                Thread.Sleep(50);
                Move(uxx);
            }
        }

        private void Move(string uxx)
        {
            if (uxx == "up")
            {
                if (rec.Y + rec.Height > 0)
                {
                    rec.Y -= fd.w;
                }
                else move = false;
            }
            else if (uxx == "down")
            {
                if (rec.Y < fr.Height)
                {
                    rec.Y += fd.w;
                }
                else move = false;
            }
            else if (uxx == "left")
            {
                if (rec.X + rec.Width > fd.x)
                {
                    rec.X -= fd.w;
                }
                else move = false;
            }
            else if (uxx == "right")
            {
                if (rec.X < fr.Width)
                {
                    rec.X += fd.w;
                }
                else move = false;
            }
            for (int i = 0; i < fd.nx; i++)
            {
                for (int j = 0; j < fd.ny; j++)
                {
                    if ((fd.mas[i, j] == 2 || fd.mas[i, j] == 1) && (
                        new Rectangle(fd.x + i * fd.w, fd.y + j * fd.w, fd.w, fd.w).Contains(rec.X, rec.Y) ||
                        new Rectangle(fd.x + i * fd.w, fd.y + j * fd.w, fd.w, fd.w).Contains(rec.X + rec.Width, rec.Y) ||
                        new Rectangle(fd.x + i * fd.w, fd.y + j * fd.w, fd.w, fd.w).Contains(rec.X, rec.Y + rec.Height) ||
                        new Rectangle(fd.x + i * fd.w, fd.y + j * fd.w, fd.w, fd.w).Contains(rec.X + rec.Width, rec.Y + rec.Height)))
                    {
                        if (fd.mas[i, j] == 1)
                        {
                            fd.mas[i, j] = 0;
                        }
                        move = false;
                        rec.X = -100;
                        rec.Y = -100;
                    }
                }
            }
            if (st.rec.Contains(rec.X, rec.Y) ||
                st.rec.Contains(rec.X + rec.Width, rec.Y) ||
                st.rec.Contains(rec.X, rec.Y + rec.Height) ||
                st.rec.Contains(rec.X + rec.Width, rec.Y + rec.Height))
            {
                st.rec.X += 5;
                st.rec.Width -= 10;
                st.Health -= 40;
                rec.X = -100;
                rec.Y = -100;
                move = false;
            }
        }

        public void Paint(Graphics g)
        {
            if (show)
            {
                g.FillEllipse(Brushes.White, rec);
            }
        }

        internal void Shoot()
        {
            if (move) return;
            try { th.Start(); }
            catch { move = false; }
            uxx = en.Way;
            rec.X = en.rec.X + en.rec.Width / 2 - rec.Width / 2;
            rec.Y = en.rec.Y + en.rec.Height / 2 - rec.Height / 2;
            move = true;
            show = true;
        }
    }
}