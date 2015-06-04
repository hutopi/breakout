using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using breakout.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace breakout
{
    public class GameLevel
    {
        private int columns;
        private int lines;
        private int screenWidth;
        private int screenHeight;

        public string Background { get; set; }

        private int lives;
        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        private int nb_bricks;
        public int Nb_bricks
        {
            get { return nb_bricks; }
            set { nb_bricks = value; }
        }

        private int nbBonus;
        public int NbBonus
        {
            get { return nbBonus; }
            set { nbBonus = value; }
        }

        private Bat bat;
        public Bat Bat
        {
            get { return bat; }
            set { bat = value; }
        }

        private List<Ball> balls;
        public List<Ball> Balls
        {
            get { return balls; }
            set { balls = value; }
        }


        public int Level { get; set; }

        private int score = 0;
        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        private BrickTexture brickTexture;
        public BrickTexture BrickTexture
        {
            get { return brickTexture; }
            set { brickTexture = value; }
        }

        private BatTextures batTexture;
        public BatTextures BatTexture
        {
            get { return batTexture; }
            set { batTexture = value; }
        }


        private Brick[,] bricksMap;
        public Brick[,] BricksMap
        {
            get { return bricksMap; }
            set { bricksMap = value; }
        }

        public GameLevel() { }

        public GameLevel(int screenWidth, int screenHeight, int level, int lines, int columns, List<Ball> balls, Bat bat)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = (int)(0.6 * (double)screenHeight);
            this.Level = level;
            this.Balls = balls;
            this.Bat = bat;
            this.lines = lines;
            this.columns = columns;
            this.BricksMap = new Brick[this.lines, this.columns];
            this.nb_bricks = this.lines * this.columns;
            this.nbBonus = (int)(0.1 * (double)(this.columns * this.lines));
        }

        public void constructLevel()
        {
            this.Score = 0;
            this.Lives = 3;

            switch (this.Level)
            {
                case 1:
                    this.LevelOne();
                    this.Background = "background_1";
                    break;
                case 2:
                    this.LevelTwo();
                    this.Background = "background_2";
                    break;
                case 3:
                    this.LevelThree();
                    this.Background = "background_3";
                    break;
                case 4:
                    this.LevelFour();
                    this.Background = "background_4";
                    break;
                default:
                    this.Background = "background_1";
                    break;
            }
           
            this.SetBonus();
            this.InitializeBonus();
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

            this.Initialize();
        }

        public void InitializeBonus()
        {
            int index = 0;
            foreach (Brick brick in this.BricksMap)
            {
                if (brick.Bonus.Type != BonusType.NONE)
                {
                    brick.Bonus.Initialize();
                    brick.Bonus.Position = new Vector2(brick.Position.X, brick.Position.Y);
                    brick.Bonus.Speed = 0.3f;

                    if (brick.Bonus.Type == BonusType.LOW_RESISTANCE || brick.Bonus.Type == BonusType.LOW_SPEED || brick.Bonus.Type == BonusType.UP_LIFE || brick.Bonus.Type == BonusType.BAT_EXTENDED)
                    {
                        brick.Bonus.Name = "bonus";
                    }
                    else
                    {
                        brick.Bonus.Name = "malus";
                    }
                    index++;
                }
            }
        }

        public void InitializeBoard(int lines, int columns, int indestructible, double percentBonus)
        {
            this.lines = lines;
            this.columns = columns;
            this.nb_bricks = (this.lines * this.columns) - indestructible;
            this.nbBonus = (int)(percentBonus * (double)(this.columns * this.lines));
            this.BricksMap = new Brick[this.lines, this.columns];
        }

        public void LevelOne()
        {
            this.InitializeBoard(3, 6, 0, 0.1);

            int x = (int)((0.25) * (double)this.screenWidth);
            int y = 80;
            int margin_h = 20;
            int margin_w = 50;

            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (j > 2)
                    {
                        this.BricksMap[i, j] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 48 + j * margin_w, y + i * margin_h), 19, 45, 1);
                    }
                    else
                    {
                        this.BricksMap[i, j] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + j * margin_w, y + i * margin_h), 19, 45, 1);
                    }
                }
            }
        }

        public void LevelTwo()
        {
            this.InitializeBoard(4, 8, 8, 0.1);

            int x = (int)((0.25) * (double)this.screenWidth);
            int y = 40;
            int margin_h = 20;
            int margin_w = 50;
            int r = 4;

            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < columns/2; j++)
                {
                    this.BricksMap[i, j] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+j*margin_w, y+i*margin_h), 19, 45, r-i);
                }
            }

            x = (int)((0.75) * (double)this.screenWidth) - (4 * margin_w);
            y = 40;

            for (int i = 0; i < lines; i++)
            {
                for (int j = columns/2; j < columns; j++)
                {
                    this.BricksMap[i, j] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (j-columns/2) * margin_w, y + i*margin_h), 19, 45, r-i);
                }
            }
        }

        public void LevelThree()
        {
            this.InitializeBoard(6, 10, 20, 0.1);

            int x = (int)((0.35) * (double)this.screenWidth);
            int y = 50;
            int margin_h = 20;
            int margin_w = 50;

            for (int j = 0; j < 10; j++)
            {
                this.BricksMap[0, j] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + j * margin_h), 19, 45, 4);
            }

            this.BricksMap[1, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + 10 * margin_h), 19, 45, 4);

            y += margin_h;
            for (int i = 1; i < 8; i++)
            {
                this.BricksMap[1, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+margin_w, y + i*margin_h), 19, 45, 1);
            }

            y -= margin_h;
            int multiplicator = 0;

            for (int i = 1; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 1 && j >= 8 || i == 2)
                    {
                        this.BricksMap[i, j] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y + multiplicator * margin_h), 19, 45, 2);
                        
                        multiplicator++;
                    }
                }
            }

            this.BricksMap[2, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+3*margin_w, y), 19, 45, 2);

            for (int i = 0; i < 10; i++)
            {
                this.BricksMap[3, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y + (i + 1) * margin_h), 19, 45, 2);
            }

            y += margin_h;
            for (int i = 0; i < 9; i++)
            {
                if ((i <= 3 && i > 0) || (i >= 6 && i < 8))
                {
                    this.BricksMap[4, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 4 * margin_w, y + i * margin_h), 19, 45, 4);
                }
                else
                {
                    this.BricksMap[4, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 4 * margin_w, y + i * margin_h), 19, 45, 2);
                }
            }

            y += margin_h;
            this.BricksMap[4, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 5 * margin_w, y), 19, 45, 2);

            for (int i = 0; i < 6; i++)
            {
                if (i <= 1)
                {
                    this.BricksMap[5, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 5 * margin_w, y + (i + 1) * margin_h), 19, 45, 4);
                }
                else
                {
                    this.BricksMap[5, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 5 * margin_w, y + (i + 1) * margin_h), 19, 45, 2);
                }
                
            }

            y += 2 * margin_h;
            for (int i = 6; i < 9; i++)
            {
                this.BricksMap[5, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+6*margin_w, y+(i-6)*margin_h), 19, 45, 2);
            }

            this.BricksMap[5, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 7*margin_w, y+margin_h), 19, 45, 0);
        }

        public void LevelFour()
        {
            this.InitializeBoard(11, 10, 20, 0.1);

            int margin_h = 20;
            int margin_w = 50;
            int x = (int)((0.5) * (double)this.screenWidth - 22);
            x -= (int)(5.5 * margin_w);
            int y = (int)((0.75) * (double)this.screenHeight - 42);
            y -= 7 * margin_h;

            this.BricksMap[0, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2((int)(x+4.5*margin_w), y), 19, 45, 1);
            this.BricksMap[0, 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2((int)(x + 5.5 * margin_w), y), 19, 45, 1);
            this.BricksMap[0, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2((int)(x + 6.5 * margin_w), y), 19, 45, 1);

            this.BricksMap[0, 3] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + margin_h), 19, 45, 4);
            this.BricksMap[0, 4] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + margin_w, y + margin_h), 19, 45, 4);

            this.BricksMap[0, 5] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 4 * margin_w, y + margin_h), 19, 45, 1);
            this.BricksMap[0, 6] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 5 * margin_w, y + margin_h), 19, 45, 3);
            this.BricksMap[0, 7] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 6 * margin_w, y + margin_h), 19, 45, 3);
            this.BricksMap[0, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 7 * margin_w, y + margin_h), 19, 45, 1);

            this.BricksMap[0, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 10*margin_w, y + margin_h), 19, 45, 4);
            this.BricksMap[1, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 11 * margin_w, y + margin_h), 19, 45, 4);

            int r = 1;
            for(int i = 1; i < 10; i++){
                if (i == 1)
                {
                    r = 4;
                }
                else if (i == 2 || i == 3)
                {
                    r = 2;
                }
                else if (i == 5 || i == 8)
                {
                    r = 1;
                }
                else if (i == 4 || i == 6 || i == 7 || i == 9)
                {
                    r = 3;
                }
                 this.BricksMap[1, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+(i-1)*margin_w, y + 2 * margin_h), 19, 45, r);
            }


            this.BricksMap[2, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+9*margin_w, y + 2 * margin_h), 19, 45, 2);
            this.BricksMap[2, 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 10 * margin_w, y + 2 * margin_h), 19, 45, 2);
            this.BricksMap[2, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 11 * margin_w, y + 2 * margin_h), 19, 45, 4);

            x += (int)(0.5*(double)margin_w);
            for (int i = 3; i < 10; i++)
            {
                if (i == 5)
                {
                    this.BricksMap[2, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 3) * margin_w, y + 3 * margin_h), 19, 45, 3);
                } 
                else if (i == 6)
                {
                    this.BricksMap[2, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 3) * margin_w, y + 3 * margin_h), 19, 45, 0);
                }
                else
                {
                    this.BricksMap[2, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 3) * margin_w, y + 3 * margin_h), 19, 45, 1);
                }
                
            }

            for(int i = 0; i < 4; i++){
                if (i == 0)
                {
                    this.BricksMap[3, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (7 + i) * margin_w, y + 3 * margin_h), 19, 45, 0);
                }
                else if(i == 1)
                {
                    this.BricksMap[3, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (7 + i) * margin_w, y + 3 * margin_h), 19, 45, 3);
                }
                else
                {
                    this.BricksMap[3, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (7 + i) * margin_w, y + 3 * margin_h), 19, 45, 1);
                }

            }

            x += (int)(0.5 * (double)margin_w);

            for (int i = 4; i < 10; i++)
            {
                if (i == 6 || i == 7)
                {
                    this.BricksMap[3, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 4) * margin_w, y + 4 * margin_h), 19, 45, 0);
                }
                else
                {
                    this.BricksMap[3, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 4) * margin_w, y + 4 * margin_h), 19, 45, 1);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (i == 0 || i == 1)
                {
                    this.BricksMap[4, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (6 + i) * margin_w, y + 4 * margin_h), 19, 45, 0);
                }
                else
                {
                    this.BricksMap[4, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (6 + i) * margin_w, y + 4 * margin_h), 19, 45, 1);
                }
            }

            x += (int)(0.5 * (double)margin_w);
            for (int i = 4; i < 10; i++)
            {
                this.BricksMap[4, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 4) * margin_w, y + 5 * margin_h), 19, 45, 2);
            }

            for (int i = 0; i < 3; i++)
            {
                this.BricksMap[5, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i+6)*margin_w, y+5*margin_h), 19, 45, 2);
            }

            for (int i = 3; i < 10; i++)
            {
                if (i == 6 || i == 8)
                {
                    this.BricksMap[5, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 3) * margin_w, y + 6 * margin_h), 19, 45, 1);
                }
                else
                {
                    this.BricksMap[5, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 3) * margin_w, y + 6 * margin_h), 19, 45, 2);
                }  
            }

            for (int i = 0; i < 2; i++)
            {
                this.BricksMap[6, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+(i+7)*margin_w, y+6*margin_h), 19, 45, 2);
            }

            for (int i = 2; i < 10; i++)
            {
                if (i == 2)
                {
                    this.BricksMap[6, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 2) * margin_w, y + 7 * margin_h), 19, 45, 4);
                }
                else
                {
                    this.BricksMap[6, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 2) * margin_w, y + 7 * margin_h), 19, 45, 2);
                }
            }

            this.BricksMap[7, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 8 * margin_w, y + 7 * margin_h), 19, 45, 4);


            x += (int)(0.5 * (double)margin_w);

            for (int i = 1; i < 5; i++)
            {
                if (i > 2)
                {
                    this.BricksMap[7, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 3) * margin_w, y + 9 * margin_h), 19, 45, 2);
                }
                else
                {
                    this.BricksMap[7, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 1) * margin_w, y + 8 * margin_h), 19, 45, 2);
                }
            }

            for (int i = 5; i < 9; i++)
            {
                if (i > 6)
                {
                    this.BricksMap[7, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i-1)*margin_w, y+9*margin_h), 19, 45, 2);
                }
                else
                {
                    this.BricksMap[7, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+ (i+1)*margin_w, y+8*margin_h), 19, 45, 2);
                }
                
            }

            x += margin_w;
            this.BricksMap[7, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x,y+10*margin_h), 19, 45, 2);

            for (int i = 0; i < 2; i++){
                this.BricksMap[8, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+(2+i)*margin_w, y + 10 * margin_h), 19, 45, 1);
            }

            this.BricksMap[8, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+5*margin_w, y + 10 * margin_h), 19, 45, 2);

            for (int i = 3; i < 9; i++)
            {
                if (i == 8)
                {
                    this.BricksMap[8, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 3) * margin_w, y + 11 * margin_h), 19, 45, 2);
                }
                else
                {
                    if (i == 3)
                    {
                        this.BricksMap[8, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 3) * margin_w, y + 11 * margin_h), 19, 45, 2);
                    }
                    else
                    {
                        this.BricksMap[8, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 3) * margin_w, y + 11 * margin_h), 19, 45, 1);
                    }
                    
                }     
            }

            this.BricksMap[8, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + 12 * margin_h), 19, 45, 2);

            for (int i = 0; i < 5; i++)
            {
                if (i == 4)
                {
                    this.BricksMap[9, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+(i+1)*margin_w, y + 12 * margin_h), 19, 45, 2);
                }
                else
                {
                    this.BricksMap[9, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i + 1) * margin_w, y + 12 * margin_h), 19, 45, 1);
                }
                
            }

            x += (int)(0.5 * (double)margin_w);

            for (int i = 5; i < 10; i++)
            {
                if (i == 5 || i == 9)
                {
                    this.BricksMap[9, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 5) * margin_w, y + 13 * margin_h), 19, 45, 4);
                }
                else if( i == 7)
                {
                    this.BricksMap[9, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 5) * margin_w, y + 13 * margin_h), 19, 45, 1);
                }
                else
                {
                    this.BricksMap[9, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 5) * margin_w, y + 13 * margin_h), 19, 45, 2);
                }
            }

            x += (int)(0.5 * (double)margin_w);
            for (int i = 0; i < 4; i++)
            {
                this.BricksMap[10, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + i * margin_w, y + 14 * margin_h), 19, 45, 2);
            }

            for (int i = 4; i < 8; i++)
            {
                if (i == 5 || i == 6)
                {
                    this.BricksMap[10, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 4) * margin_w, y + 15 * margin_h), 19, 45, 4);
                }
                else
                {
                    this.BricksMap[10, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (i - 4) * margin_w, y + 15 * margin_h), 19, 45, 2);
                }
            }

            x = (int)((0.25) * (double)this.screenWidth - 22);

            this.BricksMap[10, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x,30), 19, 45, 4);

             x = (int)((0.75) * (double)this.screenWidth - 22);
            this.BricksMap[10, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x,30), 19, 45, 4);
                
        }

        public void SetBonus()
        {
            Random rnd = new Random();
            Random x_rnd = new Random();
            Random y_rnd = new Random();
            int x, y = 0;

            Console.WriteLine("Nombre de bonus:" + nbBonus);

            for (int i = 0; i < this.nbBonus; i++)
            {
                x = x_rnd.Next(0, this.lines);
                y = y_rnd.Next(0, this.columns);

                Console.WriteLine("{0}, {1}", x, y);

                if (this.BricksMap[x, y].Bonus.Type == BonusType.NONE && this.BricksMap[x,y].Resistance > 0)
                {
                    Array values = Enum.GetValues(typeof(BonusType));
                    this.BricksMap[x, y].Bonus.Type = (BonusType)values.GetValue(rnd.Next(1, values.Length));
                }
                else
                {
                    i--;
                }
            }
        }
    }
}
