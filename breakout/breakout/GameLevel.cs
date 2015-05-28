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

        public int level { get; set; }

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
            this.level = level;
            this.BricksMap = new Brick[this.nb_lines, this.nb_columns];
        }

        public void constructLevel()
        {
            Random rnd = new Random();
            int marge = 45;
            switch (this.level)
            {
                case 1:
                    this.LevelOne();
                    break;
                default:
                    break;
            }
        }

        public void Initialize()
        {
            this.constructLevel();
        }

        public void LevelOne()
        {
            int x = (int)((0.25)*(double)this.screenWidth);
            int y = 20;
            int margin = 50;

            for (int i = 0; i < 8; i++)
            {
                this.create(i, x + margin*i, y, 3);
            }
        }

        public void create(int coord_x, int x, int y, int r)
        {
            for (int i = 1; i <= 10; i++)
            {
                if (coord_x == 0 || coord_x == 7 || i == 1 || i == 10)
                {
                    this.BricksMap[coord_x, i - 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y * i), 19, 45, r);
                }
                else if((coord_x == 3 || coord_x == 4) && (i == 5 || i == 6))
                {
                    this.BricksMap[coord_x, i - 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y * i), 19, 45, r-1);
                }
                else
                {
                    this.BricksMap[coord_x, i - 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y * i), 19, 45);
                }
            }
        }
    }
}
