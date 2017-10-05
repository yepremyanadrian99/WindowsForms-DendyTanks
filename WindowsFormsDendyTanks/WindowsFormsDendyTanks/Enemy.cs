using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace WindowsFormsDendyTanks
{
    class Enemy
    {
        Form1 fr;
        Field fd;
        Tank tk;
        Weapon wp;
        public Rectangle rec;
        public Thread th;
        int num = 1;
        public int Health { get; set; }
        public string Way { get; set; }
        public int Spawn { get; set; }
        //weapon
        Star st;
        public Rectangle wrec;
        public Thread wth;
        public bool move = false;
        string uxx;
        bool show = false;

        public Enemy(Form1 fr, Field fd, Star st, Tank tk, Weapon wp, int num, int x)
        {
            this.fr = fr;
            this.fd = fd;
            this.num = num;
            this.st = st;
            this.tk = tk;
            this.wp = wp;
            rec = new Rectangle(x, fd.y + fd.w, fd.w * 2, fd.w * 2);
            wrec = new Rectangle();
            wrec.Width = 15;
            wrec.Height = 15;
            Health = 100;
            Way = "_d";
            Spawn = 0;
            th = new Thread(new ThreadStart(Move));
            th.Start();
            wth = new Thread(new ThreadStart(Fire));
            wth.Start();
        }

        void Move()
        {
            if (Way == "")
            {
                if (rec.Y - fd.w > fd.w)
                {

                }
            }
            else if (Way == "_r")
            {

            }
            else if (Way == "_d")
            {

            }
            else if (Way == "_l")
            {

            }
        }

        private void Fire()
        {
            for (; ; )
            {
                if (fr.start)
                {
                    Thread.Sleep(50);
                    Shoot();
                    Bullet(uxx);
                }
            }
        }

        internal void Shoot()
        {
            if (move) return;
            try { th.Start(); }
            catch { move = false; }
            uxx = Way;
            wrec.X = rec.X + rec.Width / 2 - wrec.Width / 2;
            wrec.Y = rec.Y + rec.Height / 2 - wrec.Height / 2;
            move = true;
            show = true;
        }

        private void Bullet(string uxx)
        {
            if (uxx == "")
            {
                if (wrec.Y + wrec.Height > 0)
                {
                    wrec.Y -= fd.w;
                }
                else move = false;
            }
            else if (uxx == "_d")
            {
                if (wrec.Y < fr.Height)
                {
                    wrec.Y += fd.w;
                }
                else move = false;
            }
            else if (uxx == "_l")
            {
                if (wrec.X + wrec.Width > fd.x)
                {
                    wrec.X -= fd.w;
                }
                else move = false;
            }
            else if (uxx == "_r")
            {
                if (wrec.X < fr.Width)
                {
                    wrec.X += fd.w;
                }
                else move = false;
            }
            for (int i = 0; i < fd.nx; i++)
            {
                for (int j = 0; j < fd.ny; j++)
                {
                    if ((fd.mas[i, j] == 2 || fd.mas[i, j] == 1) && (
                        new Rectangle(fd.x + i * fd.w, fd.y + j * fd.w, fd.w, fd.w).Contains(wrec.X, wrec.Y) ||
                        new Rectangle(fd.x + i * fd.w, fd.y + j * fd.w, fd.w, fd.w).Contains(wrec.X + wrec.Width, wrec.Y) ||
                        new Rectangle(fd.x + i * fd.w, fd.y + j * fd.w, fd.w, fd.w).Contains(wrec.X, wrec.Y + wrec.Height) ||
                        new Rectangle(fd.x + i * fd.w, fd.y + j * fd.w, fd.w, fd.w).Contains(wrec.X + wrec.Width, wrec.Y + wrec.Height)))
                    {
                        if (fd.mas[i, j] == 1)
                        {
                            fd.mas[i, j] = 0;
                        }
                        move = false;
                        wrec.X = -100;
                        wrec.Y = -100;
                    }
                }
            }
            if (st.rec.Contains(wrec.X, wrec.Y) ||
                st.rec.Contains(wrec.X + wrec.Width, wrec.Y) ||
                st.rec.Contains(wrec.X, wrec.Y + wrec.Height) ||
                st.rec.Contains(wrec.X + wrec.Width, wrec.Y + wrec.Height))
            {
                st.rec.X += 5;
                st.rec.Width -= 10;
                st.Health -= 40;
                wrec.X = -100;
                wrec.Y = -100;
                move = false;
            }
            if (tk.rec.Contains(wrec.X, wrec.Y) ||
                tk.rec.Contains(wrec.X + wrec.Width, wrec.Y) ||
                tk.rec.Contains(wrec.X, wrec.Y + wrec.Height) ||
                tk.rec.Contains(wrec.X + wrec.Width, wrec.Y + wrec.Height))
            {
                tk.Health -= new Random().Next(16, 20);
                move = false;
            }
            if (wp.rec.Contains(wrec) ||
                wp.rec.Contains(wrec.X, wrec.Y) ||
                wp.rec.Contains(wrec.X + wrec.Width, wrec.Y) ||
                wp.rec.Contains(wrec.X, wrec.Y + wrec.Height) ||
                wp.rec.Contains(wrec.X + wrec.Width, wrec.Y + wrec.Height) ||
                wp.rec.Contains(wrec.X + fd.w, wrec.Y + fd.w) ||
                wp.rec.Contains(wrec.X, wrec.Y + fd.w) ||
                wp.rec.Contains(wrec.X + fd.w, wrec.Y) ||
                wp.rec.Contains(wrec.X + fd.w + wrec.Width, wrec.Y + fd.w + wrec.Height) ||
                wp.rec.Contains(wrec.X - fd.w, wrec.Y) ||
                wp.rec.Contains(wrec.X, wrec.Y - fd.w))
            {
                move = false;
                wrec.X = -100;
                wrec.Y = -100;
                wp.move = false;
                wp.rec.X = -100;
                wp.rec.Y = -100;
            }
        }

        public void WPaint(Graphics g)
        {
            if (show)
            {
                g.FillEllipse(Brushes.White, wrec);
            }
        }

        public void Paint(Graphics g)
        {
            g.DrawImage(Image.FromFile("Pics/" + num + Way + ".png"), rec);
        }
    }
}