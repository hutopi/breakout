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
        private int nb_lines = 4;
        private int screenWidth;
        private int screenHeight;

        public int level { get; set; }

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
            this.level = level;
            this.BricksMap = new Brick[this.nb_lines, this.nb_columns];
        }

        public void constructLevel()
        {
            Random rnd = new Random();
            switch (this.level)
            {
                case 1: // test
                    for (int i = 0; i < this.nb_lines; i++)
                    {
                        for (int j = 0; j < this.nb_columns; j++)
                        {
                            this.BricksMap[i,j] = new Brick(this.screenWidth, this.screenHeight, new Vector2(rnd.Next(0, this.screenWidth),rnd.Next(0, this.screenHeight)), 19, 45);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void Initialize()
        {
            this.constructLevel();
        }
    }
}
