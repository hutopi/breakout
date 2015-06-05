using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using breakout.Textures;
using breakout.Util;
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


        private List<Brick> bricksMap;
        public List<Brick> BricksMap
        {
            get { return bricksMap; }
            set { bricksMap = value; }
        }

        public GameFile CurrentLevelData { get; set; }

        public Texture2D Background { get; set; }

        public GameLevel(int screenWidth, int screenHeight, int level, int lines, int columns, List<Ball> balls, Bat bat)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = (int)(0.6 * (double)screenHeight);
            this.Level = level;
            this.Balls = balls;
            this.Bat = bat;
            this.lines = lines;
            this.columns = columns;
            this.BricksMap = new List<Brick>();
            this.nb_bricks = this.lines * this.columns;
        }

        public void constructLevel()
        {
            this.Score = 0;
            this.Lives = 3;

            this.CurrentLevelData = new GameFile(@"..\..\..\..\..\levels\level4.json");
            this.CurrentLevelData.Load();

            this.LevelOne();

            /*switch (this.Level)
            {
                case 1:
                    this.LevelOne();
                    this.Background = "background_1";
                    break;
                default:
                    this.Background = "background_1";
                    break;
            }*/
           
            this.SetBonus();
            this.InitializeBonus();
        }

        public void CreateBackground(GraphicsDevice device)
        {
            byte[] bgbitmap = Convert.FromBase64String((string)this.CurrentLevelData.Data.Background["file"]);
            var stream = new MemoryStream(bgbitmap);
            this.Background = Texture2D.FromStream(device, stream);
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

                    if (brick.Bonus.Type == BonusType.LOW_RESISTANCE || brick.Bonus.Type == BonusType.LOW_SPEED || brick.Bonus.Type == BonusType.UP_LIFE || brick.Bonus.Type == BonusType.BAT_EXTENDED || brick.Bonus.Type == BonusType.MULTIPLICATE_BALL)
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

        public void InitializeBoard(int bricks, int indestructible, double percentBonus)
        {
            this.nb_bricks = bricks - indestructible;
            this.nbBonus = (int)(percentBonus * (double)(bricks));
            this.BricksMap = new List<Brick>();
        }

        public void LevelOne()
        {
            int indestructibles = countIndestructibles(this.CurrentLevelData.Data.Bricks);
            this.InitializeBoard(this.CurrentLevelData.Data.Bricks.Count, indestructibles, 0.1);
            
            int margin_h = 20;
            int margin_w = 50;
            int x = 0;
            int y = 2 * margin_h;

            foreach (Dictionary<string, object> brick in this.CurrentLevelData.Data.Bricks)
            {
                var line = (double) brick["line"];
                var column = (double) brick["column"];
                var resistance = (double) brick["resistance"];
                this.BricksMap.Add(new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (int) column * margin_w, y + (int) line * margin_h), 20, 50, (int) resistance));
            }
        }

        public static int countIndestructibles(List<object> bricksData)
        {
            int result = 0;
            foreach (Dictionary<string, object> brick in bricksData)
            {
                if ((double) brick["resistance"] == (double) 4)
                {
                    result++;
                }
            }
            return result;
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
                x = x_rnd.Next(0, this.BricksMap.Count);

                

                if (this.BricksMap[x].Bonus.Type == BonusType.NONE && this.BricksMap[x].Resistance > 0)
                {
                    Console.WriteLine("{0}, {1}", x, y);
                    Array values = Enum.GetValues(typeof(BonusType));
                    this.BricksMap[x].Bonus.Type = (BonusType)values.GetValue(rnd.Next(1, values.Length));
                }
                else
                {
                    Console.WriteLine("i--");
                    i--;
                }
            }
        }
    }
}
