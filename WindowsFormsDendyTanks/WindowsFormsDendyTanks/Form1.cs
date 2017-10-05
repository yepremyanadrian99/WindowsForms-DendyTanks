using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsDendyTanks
{
    public partial class Form1 : Form
    {
        Field fd;
        Tank tk;
        Star st;
        Weapon wp;
        Enemy[] en;
        bool click = false;
        int col;
        public bool start = false;
        string lastmouse;
        int lastcol;
        int win = 0;

        public Form1()
        {
            InitializeComponent();
            fd = new Field(this);
            tk = new Tank(this, fd);
            st = new Star(this, fd);
            en = new Enemy[3];
            wp = new Weapon(this, fd, tk, st);
            for (int i = 0; i < en.Length; i++)
            {
                en[i] = new Enemy(this, fd, st, tk, wp, i + 1, fd.x + (i + 1) * fd.w * 10);
            }
            col = 1;
            lastcol = 1;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (tk.Health > 0)
            {
                tk.Paint(g);
            }
            else
            {
                win = 2;
            }
            int q = 0;
            for (int i = 0; i < en.Length; i++)
            {
                if (en[i].Health > 0)
                {
                    en[i].Paint(g);
                    fd.mas[en[i].rec.X / fd.w, en[i].rec.Y / fd.w] = 10;
                    q++;
                }
                else
                {
                    en[i].rec.Width = 0;
                    en[i].rec.Height = 0;
                    en[i].wrec.Width = 0;
                    en[i].wrec.Height = 0;
                    try
                    {
                        en[i].wth.Abort();
                    }
                    catch { }
                    fd.mas[en[i].rec.X / fd.w, en[i].rec.Y / fd.w] = 0;
                }
                en[i].WPaint(g);
                if (en[i].rec.Contains(wp.rec) ||
                    en[i].rec.Contains(wp.rec.X + wp.rec.Width, wp.rec.Y) ||
                    en[i].rec.Contains(wp.rec.X, wp.rec.Y + wp.rec.Height) ||
                    en[i].rec.Contains(wp.rec.X + wp.rec.Width, wp.rec.Y + wp.rec.Height))
                {
                    en[i].Health -= new Random().Next(16, 20);
                    wp.move = false;
                    wp.rec.X = -100;
                    wp.rec.Y = -100;
                }
            }
            if (q == 0) win = 1;
            wp.Paint(g);
            fd.Paint(g);
            if (st.Health > 0) st.Paint(g);
            else win = 2;
            Check();
            if (win == 1)
            {
                g.DrawString("YOU WON", new Font("arial", 100, FontStyle.Bold), Brushes.White, new Point(Width / 4, (Height - 50) / 2 - 30));
            }
            else if (win == 2)
            {
                g.DrawString("GAME OVER", new Font("arial", 100, FontStyle.Bold), Brushes.White, new Point(Width / 6, (Height - 50) / 2 - 30));
            }
            Invalidate();
        }

        private void Check()
        {
            if (win == 0) return;
            else if (win == 1)
            {
                wp.th.Abort();
                foreach(Enemy enm in en)
                {
                    enm.th.Abort();
                    enm.wth.Abort();
                }
                //MessageBox.Show("Krinq!");
                //Application.Exit();
            }
            else if (win == 2)
            {
                wp.th.Abort();
                foreach (Enemy enm in en)
                {
                    enm.th.Abort();
                    enm.wth.Abort();
                }
                //MessageBox.Show("Krvanq :(((");
                //Application.Exit();
                /*st.Health = 100;
                st.rec.X=fd.x + fd.w * 21;
                st.rec.Y = fd.y + fd.w * 20;
                st.rec.Width = 60;
                st.rec.Height = 60;
                tk.rec.X = fd.x + fd.w;
                tk.rec.Y = fd.y + fd.w * 20;
                win = 0;
                fd.Start();*/
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (win != 0) return;
            if (start)
            {
                wp.Shoot();
            }
            else
            {
                click = true;
                if (e.Button == MouseButtons.Right)
                {
                    col = 0;
                    lastmouse = "right";
                }
                else
                {
                    lastmouse = "";
                }
                for (int i = 0; i < fd.nx; i++)
                {
                    for (int j = 0; j < fd.ny; j++)
                    {
                        if (lastmouse == "right" && new Rectangle(fd.x + i * fd.w, fd.y + j * fd.w, fd.w, fd.w).Contains(e.X, e.Y))
                        {
                            fd.mas[i, j] = col;
                        }
                        else if (lastmouse != "right" && new Rectangle(fd.x + i * fd.w, fd.y + j * fd.w, fd.w, fd.w).Contains(e.X, e.Y))
                        {
                            int q = 0;
                            foreach (Enemy enemy in en)
                            {
                                if (enemy.rec.Contains(e.X, e.Y)) q++;
                            }
                            if (!tk.rec.Contains(e.X, e.Y) && q == 0) fd.mas[i, j] = col;
                        }
                    }
                }
            }
            Invalidate();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            click = false;
            if (lastmouse == "right") col = lastcol;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!click || start) return;
            if (win != 0) return;
            for (int i = 0; i < fd.nx; i++)
            {
                for (int j = 0; j < fd.ny; j++)
                {
                    if (new Rectangle(fd.x + i * fd.w, fd.y + j * fd.w, fd.w, fd.w).Contains(e.X, e.Y))
                    {
                        if (!tk.rec.Contains(e.X, e.Y)) fd.mas[i, j] = col;
                    }
                }
            }
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (win != 0) return;
            if (!start)
            {
                click = false;
                if (e.KeyData == Keys.B)
                {
                    col = 1;
                    lastcol = 1;
                }
                else if (e.KeyData == Keys.S)
                {
                    col = 2;
                    lastcol = 2;
                }
                else if (e.KeyData == Keys.W)
                {
                    col = 3;
                    lastcol = 3;
                }
                else if (e.KeyData == Keys.F)
                {
                    col = 4;
                    lastcol = 4;
                }
                else if (e.KeyData == Keys.Delete)
                {
                    col = 0;
                    lastcol = 0;
                }
            }
            //Controls
            else
            {
                int i = 0;
                int j = 0;
                for (int a = 0; a < fd.nx; a++)
                {
                    if (tk.rec.X - (a * fd.w) > 0)
                    {
                        i++;
                    }
                }
                for (int a = 0; a < fd.ny; a++)
                {
                    if (tk.rec.Y - (a * fd.w) > 0)
                    {
                        j++;
                    }
                }
                if (e.KeyData == Keys.Up || e.KeyData == Keys.W)
                {
                    if (tk.rec.Y > fd.y)
                    {
                        if (fd.mas[i, j - 1] == 0 || fd.mas[i, j - 1] == 4)
                        {
                            if (fd.mas[i + 1, j - 1] == 0 || fd.mas[i + 1, j - 1] == 4)
                            {
                                tk.rec.Y -= fd.w;
                            }
                        }
                    }
                    tk.Way = "up";
                }
                else if (e.KeyData == Keys.Right || e.KeyData == Keys.D)
                {
                    if (tk.rec.X < (fd.nx - 1) * fd.w)
                    {
                        if (fd.mas[i + 2, j] == 0 || fd.mas[i + 2, j] == 4)
                        {
                            if (fd.mas[i + 2, j + 1] == 0 || fd.mas[i + 2, j + 1] == 4)
                            {
                                tk.rec.X += fd.w;
                            }
                        }
                    }
                    tk.Way = "right";
                }
                else if (e.KeyData == Keys.Down || e.KeyData == Keys.S)
                {
                    if (tk.rec.Y < (fd.ny - 1) * fd.w)
                    {
                        if (fd.mas[i, j + 2] == 0 || fd.mas[i, j + 2] == 4)
                        {
                            if (fd.mas[i + 1, j + 2] == 0 || fd.mas[i + 1, j + 2] == 4)
                            {
                                tk.rec.Y += fd.w;
                            }
                        }
                    }
                    tk.Way = "down";
                }
                else if (e.KeyData == Keys.Left || e.KeyData == Keys.A)
                {
                    if (tk.rec.X > fd.x)
                    {
                        if (fd.mas[i - 1, j] == 0 || fd.mas[i - 1, j] == 4)
                        {
                            if (fd.mas[i - 1, j + 1] == 0 || fd.mas[i - 1, j + 1] == 4)
                            {
                                tk.rec.X -= fd.w;
                            }
                        }
                    }
                    tk.Way = "left";
                }
            }
            if (e.KeyData == Keys.Space)
            {
                if (start)
                {
                    wp.Shoot();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (!start)
            {
                start = true;
                label1.Text = "Stop";
                return;
            }
            start = false;
            label1.Text = "Start";
        }

        private void Form1_Closed(object sender, FormClosedEventArgs e)
        {
            wp.th.Abort();
            for (int i = 0; i < en.Length; i++)
            {
                en[i].wth.Abort();
            }
        }
    }
}