using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsDendyTanks
{
    class Field
    {
        private Form1 fr;
        public int[,] mas;
        public int w = 30, nx = 45, ny = 23;
        public int x = 0, y = 0;

        public Field(Form1 fr)
        {
            this.fr = fr;
            mas = new int[nx, ny];
            Start();
        }

        public void Start()
        {
            for (int i = 0; i < nx; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    mas[i, j] = 0;//clear everything
                }
            }
            for (int i = 0; i < nx; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    ///////Stone
                    mas[i, 0] = 2;
                    mas[i, ny - 1] = 2;
                    mas[0, j] = 2;
                    mas[nx - 1, j] = 2;
                    mas[1, 12] = 2;
                    mas[2, 12] = 2;
                    mas[nx - 3, 12] = 2;
                    mas[nx - 2, 12] = 2;
                    mas[21, 5] = 2;
                    mas[22, 5] = 2;
                    mas[21, 6] = 2;
                    mas[22, 6] = 2;
                    ////mejteghi arandzin kubikner@
                    ////1
                    mas[11, 11] = 2;
                    mas[12, 11] = 2;
                    mas[11, 12] = 2;
                    mas[12, 12] = 2;
                    ////2
                    mas[15, 11] = 2;
                    mas[16, 11] = 2;
                    mas[15, 12] = 2;
                    mas[16, 12] = 2;
                    ////3
                    mas[27, 11] = 2;
                    mas[28, 11] = 2;
                    mas[27, 12] = 2;
                    mas[28, 12] = 2;
                    ////4
                    mas[31, 11] = 2;
                    mas[32, 11] = 2;
                    mas[31, 12] = 2;
                    mas[32, 12] = 2;
                    ///////Forest
                    if (i < nx - 1)
                    {
                        //mas[i, 1] = 4;
                        //mas[i, 2] = 4;
                    }
                    if (j > 0 && j < ny - 1)
                    {
                        mas[1, j] = 4;
                        mas[2, j] = 4;
                    }
                    if (j > 0 && j < ny - 1 && i > nx - 4 && i < nx - 1)
                    {
                        mas[i, j] = 4;
                    }
                    if (j > ny - 4 && j < ny - 1 && i > 0 && i < 9)
                    {
                        mas[i, j] = 4;
                    }
                    if (j > ny - 4 && j < ny - 1 && i > nx - 10 && i < nx - 1)
                    {
                        mas[i, j] = 4;
                    }
                    if (j > ny - 9 && j < ny - 3)
                    {
                        mas[9, j] = 4;
                        mas[10, j] = 4;
                        mas[17, j] = 4;
                        mas[18, j] = 4;

                        mas[25, j] = 4;
                        mas[26, j] = 4;
                        mas[34, j] = 4;
                        mas[35, j] = 4;
                    }
                    if (i > 10 && i < 19)
                    {
                        mas[i, ny - 8] = 4;
                        mas[i, ny - 4] = 4;
                    }
                    if (i > 24 && i < 37)
                    {
                        mas[i, ny - 8] = 4;
                        mas[i, ny - 4] = 4;
                    }
                    ///verevi jri mot 2 hat forest (brick-in kpats henc)
                    mas[9, 3] = 4;
                    mas[10, 3] = 4;
                    mas[9, 4] = 4;
                    mas[10, 4] = 4;
                    mas[9, 5] = 4;
                    mas[10, 5] = 4;
                    mas[9, 6] = 4;
                    mas[10, 6] = 4;
                    if (i > 9 && i < 19)
                    {
                        mas[i, 6] = 4;
                    }
                    if (i > 24 && i < 36)
                    {
                        mas[i, 6] = 4;
                    }
                    //esel en mi jri mot 2 hat forest
                    mas[34, 3] = 4;
                    mas[35, 3] = 4;
                    mas[34, 4] = 4;
                    mas[35, 4] = 4;
                    mas[34, 5] = 4;
                    mas[35, 5] = 4;
                    ///////Brick
                    if (j > 2 && j < 9)
                    {
                        ////star
                        mas[20, ny - 2] = 1;
                        mas[20, ny - 3] = 1;
                        mas[20, ny - 4] = 1;
                        mas[21, ny - 4] = 1;
                        mas[22, ny - 4] = 1;
                        mas[23, ny - 4] = 1;
                        mas[23, ny - 3] = 1;
                        mas[23, ny - 2] = 1;
                        ////Nergevi 90-na
                        if (i > 18 && i < 25)
                        {
                            mas[i, ny - 7] = 1;
                            mas[i, ny - 11] = 1;
                        }
                        mas[19, ny - 9] = 1;
                        mas[19, ny - 10] = 1;
                        mas[20, ny - 9] = 1;
                        mas[21, ny - 9] = 1;
                        mas[21, ny - 10] = 1;
                        mas[21, ny - 8] = 1;
                        mas[22, ny - 8] = 1;  ////sranqel
                        mas[22, ny - 9] = 1;
                        mas[22, ny - 10] = 1;
                        mas[24, ny - 8] = 1;
                        mas[24, ny - 9] = 1;
                        mas[24, ny - 10] = 1;
                        ////
                        mas[3, j] = 1;
                        mas[4, j] = 1;
                        mas[7, j] = 1;
                        mas[8, j] = 1;
                        mas[19, j] = 1;
                        mas[20, j] = 1;
                        mas[23, j] = 1;
                        mas[24, j] = 1;
                        mas[36, j] = 1;
                        mas[37, j] = 1;
                        mas[40, j] = 1;
                        mas[41, j] = 1;
                        mas[19, j + 1] = 1;
                        mas[20, j + 1] = 1;
                        mas[23, j + 1] = 1;////verevi H-na mejteghne stone
                        mas[24, j + 1] = 1;
                    }
                    if (j < ny - 3 && j > 14)
                    {
                        mas[3, j] = 1;
                        mas[4, j] = 1;
                        mas[7, j] = 1;
                        mas[8, j] = 1;
                        mas[36, j] = 1;
                        mas[37, j] = 1;
                        mas[40, j] = 1;
                        mas[41, j] = 1;
                    }
                    mas[1, 11] = 1;
                    mas[2, 11] = 1;
                    mas[5, 11] = 1;
                    mas[6, 11] = 1;
                    mas[5, 12] = 1;
                    mas[6, 12] = 1;
                    mas[7, 11] = 1;
                    mas[8, 11] = 1;
                    mas[7, 12] = 1;
                    mas[8, 12] = 1;
                    mas[nx - 3, 11] = 1;
                    mas[nx - 2, 11] = 1;
                    mas[nx - 6, 11] = 1;
                    mas[nx - 7, 11] = 1;
                    mas[nx - 6, 12] = 1;
                    mas[nx - 7, 12] = 1;
                    mas[nx - 8, 11] = 1;
                    mas[nx - 9, 11] = 1;
                    mas[nx - 8, 12] = 1;
                    mas[nx - 9, 12] = 1;
                    ///////Water
                    if (i > 10 && i < 21)
                    {
                        mas[i, 3] = 3;
                        mas[i, 4] = 3;
                        mas[i, 5] = 3;
                    }
                    if (i > 24 && i < 34)
                    {
                        mas[i, 3] = 3;
                        mas[i, 4] = 3;
                        mas[i, 5] = 3;
                    }
                    if (i > 10 && i < 17 && j > 15 && j < 19)
                    {
                        mas[i, j] = 3;
                    }
                    if (i > 26 && i < 34 && j > 15 && j < 19)
                    {
                        mas[i, j] = 3;
                    }
                }
            }
        }

        internal void Paint(Graphics g)
        {
            for (int i = 0; i < nx; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    //if (mas[i, j] == 1) g.DrawImage(Image.FromFile("Pics/brick.png"), x + i * w, y + j * w, w, w);     //brick
                    //else if (mas[i, j] == 2) g.DrawImage(Image.FromFile("Pics/stone.png"), x + i * w, y + j * w, w, w);//stone
                    //else if (mas[i, j] == 3) g.DrawImage(Image.FromFile("Pics/water.png"), x + i * w, y + j * w, w, w);//water
                    //else if (mas[i, j] == 4) g.DrawImage(Image.FromFile("Pics/grass.png"), x + i * w, y + j * w, w, w);//grass
                    if (mas[i, j] == 1) g.FillRectangle(Brushes.Red, x + i * w, y + j * w, w, w);     //brick
                    else if (mas[i, j] == 2) g.FillRectangle(Brushes.SlateGray, x + i * w, y + j * w, w, w);//stone
                    else if (mas[i, j] == 3) g.FillRectangle(Brushes.Blue, x + i * w, y + j * w, w, w);//water
                    else if (mas[i, j] == 4) g.FillRectangle(Brushes.Green, x + i * w, y + j * w, w, w);//grass
                }
            }
            for (int i = 0; i <= nx; i++)
            {
                for (int j = 0; j <= ny; j++)
                {
                    //g.DrawLine(Pens.Black, x + i * w, y, x + i * w, y + ny * w);
                    //g.DrawLine(Pens.Black, x, y + j * w, x + nx * w, y + j * w); //bacel menak design-i het popoxutyun katareluc!
                }
            }
        }
    }
}