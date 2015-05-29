using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace breakout
{
    public class GameLevel
    {
        private int nb_columns = 10;
        private int nb_lines = 8;
        private int screenWidth;
        private int screenHeight;

        private int nb_bricks;

        public int Nb_bricks
        {
            get { return nb_bricks; }
            set { nb_bricks = value; }
        }

        public int Level { get; set; }

        private int score = 0;

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public BrickTexture brickTexture { get; set; }

        private Brick[,] bricksMap;

        public Brick[,] BricksMap
        {
            get { return bricksMap; }
            set { bricksMap = value; }
        }

        public GameLevel() { }

        public GameLevel(int screenWidth, int screenHeight, int level)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = (int)(0.6*(double)screenHeight);
            this.Level = level;
            this.BricksMap = new Brick[this.nb_lines, this.nb_columns];
            this.nb_bricks = this.nb_lines*this.nb_columns;
        }

        public void constructLevel()
        {
            this.Score = 0;
            switch (this.Level)
            {
                case 1:
                    this.LevelOne();
                    break;
                case 2:
                    this.LevelTwo();
                    this.nb_bricks -= 8;
                    break;
                case 3:
                    this.LevelThree();
                    break;
                default:
                    break;
            }
            this.setBonus();
        }

        public void Initialize()
        {
            this.constructLevel();
        }

        public void Update(bool restart)
        {
            if (!restart)
            {
                this.Level++;
            }
            this.nb_bricks = this.nb_lines * this.nb_columns;
            this.Initialize();
        }

        public void LevelOne()
        {
            int x = (int)((0.25)*(double)this.screenWidth);
            int y = 40;
            int margin_h = 21;
            int margin_w = 48;
            int r = 3;


            for (int coord_x = 0; coord_x < nb_lines; coord_x++)
            {
                if (coord_x != 0)
                {
                    x = x + margin_w;
                }

                for (int j = 1; j <= nb_columns; j++)
                {
                    if (j == 1)
                    {
                        this.BricksMap[coord_x, j - 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y), 19, 45, r);
                    }
                    else if (coord_x == 0 || coord_x == 7 || j == 10)
                    {
                        this.BricksMap[coord_x, j - 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + (j - 1) * margin_h), 19, 45, r);
                    }
                    else if ((coord_x == 3 || coord_x == 4) && (j == 5 || j == 6))
                    {
                        this.BricksMap[coord_x, j - 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + (j - 1) * margin_h), 19, 45, r - 1);
                    }
                    else
                    {
                        this.BricksMap[coord_x, j - 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + (j - 1) * margin_h), 19, 45);
                    }
                }
            }
        }

        public void LevelTwo()
        {
            int x = (int)((0.25) * (double)this.screenWidth);
            int y = 40;
            int margin_h = 21;
            int margin_w = 48;

            // oeil gauche
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    this.BricksMap[i, j] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y), 19, 45, 3);
                }
            }

            this.BricksMap[0, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y), 19, 45, 3);
            this.BricksMap[0, 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 1 * margin_w, y), 19, 45, 3);
            this.BricksMap[0, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y), 19, 45, 3);
            this.BricksMap[0, 3] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y), 19, 45, 3);

            this.BricksMap[0, 4] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + 1 * margin_h), 19, 45, 3);
            this.BricksMap[0, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + 2 * margin_h), 19, 45, 3);

            this.BricksMap[0, 5] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+1*margin_w, y+margin_h), 19, 45, 2);
            this.BricksMap[0, 6] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+2*margin_w, y+margin_h), 19, 45, 2);
            this.BricksMap[0, 7] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+3*margin_w, y+margin_h), 19, 45, 3);

            this.BricksMap[0, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+1*margin_w, y + 2 * margin_h), 19, 45, 2);
            this.BricksMap[1, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+2*margin_w, y + 2 * margin_h), 19, 45, 2);
            this.BricksMap[1, 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+3*margin_w, y + 2 * margin_h), 19, 45, 3);

            this.BricksMap[1, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y+3*margin_h), 19, 45, 3);

            this.BricksMap[1, 3] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+1*margin_w, y+3 * margin_h), 19, 45, 3);
            this.BricksMap[1, 4] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+2*margin_w, y+3 * margin_h), 19, 45, 3);
            this.BricksMap[1, 5] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+3*margin_w, y+3 * margin_h), 19, 45, 3);

            this.BricksMap[1, 6] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y+4*margin_h), 19, 45, 4);

            this.BricksMap[1, 7] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 1*margin_w, y + 4 * margin_h), 19, 45, 4);
            this.BricksMap[1, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2*margin_w, y + 4 * margin_h), 19, 45, 4);
            this.BricksMap[1, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3*margin_w, y + 4 * margin_h), 19, 45, 4);

            // Oreille gauche: 4
            this.BricksMap[7, 6] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 2*margin_w, y + 4 * margin_h), 19, 45, 2);
            this.BricksMap[7, 7] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 4*margin_w, y + 4 * margin_h), 19, 45, 2);
            this.BricksMap[7, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 3*margin_w, y + 3 * margin_h), 19, 45, 2);
            this.BricksMap[7, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 3*margin_w, y + 5 * margin_h), 19, 45, 2);

            x = (int)((0.75) * (double)this.screenWidth) - (4*margin_w);
            // oeil droit
            this.BricksMap[2, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y), 19, 45, 3);
            this.BricksMap[2, 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + margin_w, y), 19, 45, 3);
            this.BricksMap[2, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y), 19, 45, 3);
            this.BricksMap[2, 3] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y), 19, 45, 3);
            this.BricksMap[2, 4] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + margin_h), 19, 45, 3);
            this.BricksMap[2, 5] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + margin_w, y + margin_h), 19, 45, 2);
            this.BricksMap[2, 6] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y + margin_h), 19, 45, 2);
            this.BricksMap[2, 7] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y + margin_h), 19, 45, 3);
            this.BricksMap[2, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + 2 * margin_h), 19, 45, 3);
            this.BricksMap[2, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + margin_w, y + 2 * margin_h), 19, 45, 2);

            this.BricksMap[3, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2*margin_w, y + 2 * margin_h), 19, 45, 2);
            this.BricksMap[3, 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3*margin_w, y + 2 * margin_h), 19, 45, 3);
            this.BricksMap[3, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + 3 * margin_h), 19, 45, 3);
            this.BricksMap[3, 3] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + margin_w, y + 3 * margin_h), 19, 45, 3);
            this.BricksMap[3, 4] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2*margin_w, y + 3 * margin_h), 19, 45, 3);
            this.BricksMap[3, 5] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3*margin_w, y + 3 * margin_h), 19, 45, 3);
            this.BricksMap[3, 6] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + 4 * margin_h), 19, 45, 4);
            this.BricksMap[3, 7] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + margin_w, y + 4 * margin_h), 19, 45, 4);
            this.BricksMap[3, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2*margin_w, y + 4 * margin_h), 19, 45, 4);
            this.BricksMap[3, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3*margin_w, y + 4 * margin_h), 19, 45, 4);

            // Oreille droite: 4
            this.BricksMap[7, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+5*margin_w, y + 4 * margin_h), 19, 45, 2);
            this.BricksMap[7, 3] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+7*margin_w, y + 4 * margin_h), 19, 45, 2);
            this.BricksMap[7, 4] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+6*margin_w, y + 3 * margin_h), 19, 45, 2);
            this.BricksMap[7, 5] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+6*margin_w, y + 5 * margin_h), 19, 45, 2);
            // next

            x = (int)((0.5) * (double)this.screenWidth) - (int)((0.5)* margin_w);
            y = (int)((0.5) * (double)this.screenHeight/0.6);

            this.BricksMap[4, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x,y), 19, 45, 3);
            this.BricksMap[4, 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+margin_w,y-margin_h), 19, 45, 3);
            this.BricksMap[4, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x-margin_w,y-margin_h), 19, 45, 3);
            this.BricksMap[4, 3] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2*margin_w, y - 2 * margin_h), 19, 45, 3);
            this.BricksMap[4, 4] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x-2*margin_w,y-2*margin_h), 19, 45, 3);
            this.BricksMap[4, 5] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+3*margin_w, y-3*margin_h), 19, 45, 3);
            this.BricksMap[4, 6] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 3*margin_w, y - 3 * margin_h), 19, 45, 3);
            this.BricksMap[4, 7] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 4*margin_w, y - 4 * margin_h), 19, 45, 3);
            this.BricksMap[4, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 4*margin_w, y - 4 * margin_h), 19, 45, 3);
            this.BricksMap[4, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+2*margin_w, y-margin_h), 19, 45, 3);

            this.BricksMap[5, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y - 2*margin_h), 19, 45, 3);
            this.BricksMap[5, 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 4 * margin_w, y - 3*margin_h), 19, 45, 3);
            this.BricksMap[5, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 2 * margin_w, y - margin_h), 19, 45, 3);
            this.BricksMap[5, 3] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 3*margin_w, y - 2 * margin_h), 19, 45, 3);
            this.BricksMap[5, 4] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 4*margin_w, y - 3 * margin_h), 19, 45, 3);
            this.BricksMap[5, 5] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x,y-4*margin_h), 19, 45, 3);
            this.BricksMap[5, 6] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x-margin_w, y - 4 * margin_h), 19, 45, 3);
            this.BricksMap[5, 7] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 2*margin_w, y - 4 * margin_h), 19, 45, 3);
            this.BricksMap[5, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 3*margin_w, y - 4 * margin_h), 19, 45, 3);
            this.BricksMap[5, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + margin_w, y - 4 * margin_h), 19, 45, 3);

            this.BricksMap[6, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2*margin_w, y - 4 * margin_h), 19, 45, 3);
            this.BricksMap[6, 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3*margin_w, y - 4 * margin_h), 19, 45, 3);

            this.BricksMap[6, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y-margin_h), 19, 45, 1);
            this.BricksMap[6, 3] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y-2*margin_h), 19, 45, 1);
            this.BricksMap[6, 4] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y-3*margin_h), 19, 45, 1);
            this.BricksMap[6, 5] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+margin_w, y-2*margin_h), 19, 45, 1);
            this.BricksMap[6, 6] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+margin_w, y-3*margin_h), 19, 45, 1);
            this.BricksMap[6, 7] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - margin_w, y - 2 * margin_h), 19, 45, 1);
            this.BricksMap[6, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - margin_w, y - 3 * margin_h), 19, 45, 1);
            this.BricksMap[6, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2*margin_w, y - 3 * margin_h), 19, 45, 1);
            this.BricksMap[7, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 2*margin_w, y - 3 * margin_h), 19, 45, 1);
            this.BricksMap[7, 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y-6*margin_h), 19, 45, 1);
        }

        public void LevelThree()
        {// @TODO
            for (int i = 0; i < nb_lines; i++)
            {
                for (int j = 0; j < nb_columns; j++)
                {
                    this.BricksMap[i, j] = new Brick(this.screenWidth, this.screenHeight, new Vector2(0,0), 19, 45, 1);
                }
            }
        }

        public void setBonus()
        {
            Random rnd = new Random();
            Random x_rnd = new Random();
            Random y_rnd = new Random();
            int x, y = 0;
            int nbBonus = (int)(0.1 * (double)(this.nb_columns * this.nb_lines));
            for (int i = 0; i < nbBonus; i++)
            {
                x = x_rnd.Next(0, this.nb_lines);
                y = y_rnd.Next(0, this.nb_columns);

                if (this.BricksMap[x, y].bonus == Bonus.NONE)
                {
                    Array values = Enum.GetValues(typeof(Bonus));
                    this.BricksMap[x, y].bonus = (Bonus)values.GetValue(rnd.Next(1, values.Length));
                }
                else
                {
                    i--;
                }
            }
        }
    }
}
