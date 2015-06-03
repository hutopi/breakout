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
        private int columns = 10;
        private int lines = 8;
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

        public GameLevel(int screenWidth, int screenHeight, int level, List<Ball> balls, Bat bat)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = (int)(0.6 * (double)screenHeight);
            this.Level = level;
            this.Balls = balls;
            this.Bat = bat;
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
                    this.nb_bricks -= 8;
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
            this.nb_bricks = this.lines * this.columns;
            this.nbBonus = (int)(0.1 * (double)(this.columns * this.lines));
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

        public void LevelOne()
        {
            int x = (int)((0.25) * (double)this.screenWidth);
            int y = 40;
            int margin_h = 21;
            int margin_w = 48;
            int r = 3;


            for (int coord_x = 0; coord_x < lines; coord_x++)
            {
                if (coord_x != 0)
                {
                    x = x + margin_w;
                }

                for (int j = 1; j <= columns; j++)
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

            this.BricksMap[0, 5] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 1 * margin_w, y + margin_h), 19, 45, 2);
            this.BricksMap[0, 6] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y + margin_h), 19, 45, 2);
            this.BricksMap[0, 7] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y + margin_h), 19, 45, 3);

            this.BricksMap[0, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 1 * margin_w, y + 2 * margin_h), 19, 45, 2);
            this.BricksMap[1, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y + 2 * margin_h), 19, 45, 2);
            this.BricksMap[1, 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y + 2 * margin_h), 19, 45, 3);

            this.BricksMap[1, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + 3 * margin_h), 19, 45, 3);

            this.BricksMap[1, 3] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 1 * margin_w, y + 3 * margin_h), 19, 45, 3);
            this.BricksMap[1, 4] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y + 3 * margin_h), 19, 45, 3);
            this.BricksMap[1, 5] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y + 3 * margin_h), 19, 45, 3);

            this.BricksMap[1, 6] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + 4 * margin_h), 19, 45, 4);

            this.BricksMap[1, 7] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 1 * margin_w, y + 4 * margin_h), 19, 45, 4);
            this.BricksMap[1, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y + 4 * margin_h), 19, 45, 4);
            this.BricksMap[1, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y + 4 * margin_h), 19, 45, 4);

            // Oreille gauche: 4
            this.BricksMap[7, 6] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 2 * margin_w, y + 4 * margin_h), 19, 45, 2);
            this.BricksMap[7, 7] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 4 * margin_w, y + 4 * margin_h), 19, 45, 2);
            this.BricksMap[7, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 3 * margin_w, y + 3 * margin_h), 19, 45, 2);
            this.BricksMap[7, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 3 * margin_w, y + 5 * margin_h), 19, 45, 2);

            x = (int)((0.75) * (double)this.screenWidth) - (4 * margin_w);
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

            this.BricksMap[3, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y + 2 * margin_h), 19, 45, 2);
            this.BricksMap[3, 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y + 2 * margin_h), 19, 45, 3);
            this.BricksMap[3, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + 3 * margin_h), 19, 45, 3);
            this.BricksMap[3, 3] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + margin_w, y + 3 * margin_h), 19, 45, 3);
            this.BricksMap[3, 4] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y + 3 * margin_h), 19, 45, 3);
            this.BricksMap[3, 5] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y + 3 * margin_h), 19, 45, 3);
            this.BricksMap[3, 6] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + 4 * margin_h), 19, 45, 4);
            this.BricksMap[3, 7] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + margin_w, y + 4 * margin_h), 19, 45, 4);
            this.BricksMap[3, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y + 4 * margin_h), 19, 45, 4);
            this.BricksMap[3, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y + 4 * margin_h), 19, 45, 4);

            // Oreille droite: 4
            this.BricksMap[7, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 5 * margin_w, y + 4 * margin_h), 19, 45, 2);
            this.BricksMap[7, 3] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 7 * margin_w, y + 4 * margin_h), 19, 45, 2);
            this.BricksMap[7, 4] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 6 * margin_w, y + 3 * margin_h), 19, 45, 2);
            this.BricksMap[7, 5] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 6 * margin_w, y + 5 * margin_h), 19, 45, 2);
            // next

            x = (int)((0.5) * (double)this.screenWidth) - (int)((0.5) * margin_w);
            y = (int)((0.5) * (double)this.screenHeight / 0.6);

            this.BricksMap[4, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y), 19, 45, 3);
            this.BricksMap[4, 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + margin_w, y - margin_h), 19, 45, 3);
            this.BricksMap[4, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - margin_w, y - margin_h), 19, 45, 3);
            this.BricksMap[4, 3] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y - 2 * margin_h), 19, 45, 3);
            this.BricksMap[4, 4] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 2 * margin_w, y - 2 * margin_h), 19, 45, 3);
            this.BricksMap[4, 5] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y - 3 * margin_h), 19, 45, 3);
            this.BricksMap[4, 6] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 3 * margin_w, y - 3 * margin_h), 19, 45, 3);
            this.BricksMap[4, 7] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 4 * margin_w, y - 4 * margin_h), 19, 45, 3);
            this.BricksMap[4, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 4 * margin_w, y - 4 * margin_h), 19, 45, 3);
            this.BricksMap[4, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y - margin_h), 19, 45, 3);

            this.BricksMap[5, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y - 2 * margin_h), 19, 45, 3);
            this.BricksMap[5, 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 4 * margin_w, y - 3 * margin_h), 19, 45, 3);
            this.BricksMap[5, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 2 * margin_w, y - margin_h), 19, 45, 3);
            this.BricksMap[5, 3] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 3 * margin_w, y - 2 * margin_h), 19, 45, 3);
            this.BricksMap[5, 4] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 4 * margin_w, y - 3 * margin_h), 19, 45, 3);
            this.BricksMap[5, 5] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y - 4 * margin_h), 19, 45, 3);
            this.BricksMap[5, 6] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - margin_w, y - 4 * margin_h), 19, 45, 3);
            this.BricksMap[5, 7] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 2 * margin_w, y - 4 * margin_h), 19, 45, 3);
            this.BricksMap[5, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 3 * margin_w, y - 4 * margin_h), 19, 45, 3);
            this.BricksMap[5, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + margin_w, y - 4 * margin_h), 19, 45, 3);

            this.BricksMap[6, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y - 4 * margin_h), 19, 45, 3);
            this.BricksMap[6, 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y - 4 * margin_h), 19, 45, 3);

            this.BricksMap[6, 2] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y - margin_h), 19, 45, 1);
            this.BricksMap[6, 3] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y - 2 * margin_h), 19, 45, 1);
            this.BricksMap[6, 4] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y - 3 * margin_h), 19, 45, 1);
            this.BricksMap[6, 5] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + margin_w, y - 2 * margin_h), 19, 45, 1);
            this.BricksMap[6, 6] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + margin_w, y - 3 * margin_h), 19, 45, 1);
            this.BricksMap[6, 7] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - margin_w, y - 2 * margin_h), 19, 45, 1);
            this.BricksMap[6, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - margin_w, y - 3 * margin_h), 19, 45, 1);
            this.BricksMap[6, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y - 3 * margin_h), 19, 45, 1);
            this.BricksMap[7, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 2 * margin_w, y - 3 * margin_h), 19, 45, 1);
            this.BricksMap[7, 1] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y - 6 * margin_h), 19, 45, 1);
        }

        public void LevelThree()
        {
            int x = (int)((0.35) * (double)this.screenWidth);
            int y = 50;
            int margin_h = 21;
            int margin_w = 48;
            int count = 0;

            for(int i = 0; i < 2; i++){
                for (int j = 0; j < 10; j++)
                {
                    if (i == 1 && j == 0)
                    {
                        this.BricksMap[i, 0] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + 10 * margin_h), 19, 45, 3);
                        break;
                    }
                    if (j == 1)
                    {
                        this.BricksMap[i, j] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + j * margin_h), 19, 45, 1);
                    }
                    else
                    {
                        this.BricksMap[i, j] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y + j * margin_h), 19, 45, 3);
                    }
                }
            }

            y += margin_h;
            for (int i = 1; i < 8; i++)
            {
                this.BricksMap[1, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+margin_w, y + i*margin_h), 19, 45, 2);
            }

            y -= margin_h;
            int multiplicator = 0;

            for (int i = 1; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 1 && j >= 8)
                    {
                        if (j == 9)
                        {
                            this.BricksMap[i, j] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y + multiplicator * margin_h), 19, 45, 1);
                        }
                        else
                        {
                            this.BricksMap[i, j] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y + multiplicator * margin_h), 19, 45, 3);
                        }
                        
                        multiplicator++;
                    }
                    else if (i == 2)
                    {
                        if (j == 5)
                        {
                            this.BricksMap[i, j] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y + multiplicator * margin_h), 19, 45, 1);
                        }
                        else
                        {
                            this.BricksMap[i, j] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 2 * margin_w, y + multiplicator * margin_h), 19, 45, 3);
                        }
                        
                        multiplicator++;
                    }
                }
            }

            this.BricksMap[2, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+3*margin_w, y), 19, 45, 3);

            for (int i = 0; i < 10; i++)
            {
                if (i == 0 || i == 6)
                {
                    this.BricksMap[3, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y + (i + 1) * margin_h), 19, 45, 1);
                }
                else
                {
                    this.BricksMap[3, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 3 * margin_w, y + (i + 1) * margin_h), 19, 45, 3);
                }
            }

            y += margin_h;
            for (int i = 0; i < 9; i++)
            {
                if ((i <= 3 && i > 0) || (i >= 6 && i < 8))
                {
                    this.BricksMap[4, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 4 * margin_w, y + i * margin_h), 19, 45, 1);
                }
                else
                {
                    this.BricksMap[4, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 4 * margin_w, y + i * margin_h), 19, 45, 3);
                }
            }

            y += margin_h;
            this.BricksMap[4, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 5 * margin_w, y), 19, 45, 3);

            for (int i = 0; i < 6; i++)
            {
                if (i <= 1)
                {
                    this.BricksMap[5, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 5 * margin_w, y + (i + 1) * margin_h), 19, 45, 1);
                }
                else
                {
                    this.BricksMap[5, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x + 5 * margin_w, y + (i + 1) * margin_h), 19, 45, 3);
                }
                
            }

            y += 2 * margin_h;
            for (int i = 6; i < 9; i++)
            {
                this.BricksMap[5, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x+6*margin_w, y+(i-6)*margin_h), 19, 45, 3);
            }

            x -= (int)(2.5 * margin_w);
            y += margin_h;

            this.BricksMap[5, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 3*margin_w, y), 19, 45, 1);

            y -= margin_h; 

            for (int i = 0; i < 8; i++)
            {
                if(i <= 2){
                    this.BricksMap[6, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - 2 * margin_w, y + i * margin_h), 19, 45, 1);
                }
                else
                {
                    if (i == 3)
                    {
                        y -= margin_h;
                    }

                    this.BricksMap[6, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x - margin_w, y + (i-3) * margin_h), 19, 45, 1);
                }
                
            }

            y -= margin_h;

            this.BricksMap[6, 8] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y), 19, 45, 1);
            this.BricksMap[6, 9] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y+margin_h), 19, 45, 1);

            for (int i = 0; i < 5; i++)
            {
                this.BricksMap[7, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y+(i+2)*margin_h), 19, 45, 1);
            }


            x += (int)(1.5*margin_w);
            y += margin_h;

            for (int i = 5; i < 10; i++)
            {
                this.BricksMap[7, i] = new Brick(this.screenWidth, this.screenHeight, new Vector2(x, y+(i-5)*margin_h), 19, 45, 4);
            }

        }

        public void LevelFour()
        {

        }

        public void SetBonus()
        {
            Random rnd = new Random();
            Random x_rnd = new Random();
            Random y_rnd = new Random();
            int x, y = 0;

            for (int i = 0; i < this.nbBonus; i++)
            {
                x = x_rnd.Next(0, this.lines);
                y = y_rnd.Next(0, this.columns);

                if (this.BricksMap[x, y].Bonus.Type == BonusType.NONE)
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
