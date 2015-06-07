// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Pierre Defache
// Created          : 06-07-2015
//
// Last Modified By : Pierre Defache
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="GameLevel.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//      Breakout is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using breakout.GameComponents;
using breakout.Textures;
using breakout.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

/// <summary>
/// The breakout namespace.
/// </summary>
namespace breakout
{
    /// <summary>
    /// Class GameLevel.
    /// </summary>
    public class GameLevel
    {
        /// <summary>
        /// The screen width
        /// </summary>
        private int screenWidth;
        /// <summary>
        /// The screen height
        /// </summary>
        private int screenHeight;

        /// <summary>
        /// The lives
        /// </summary>
        private int lives;
        /// <summary>
        /// Gets or sets the lives.
        /// </summary>
        /// <value>The lives.</value>
        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        /// <summary>
        /// The nb_bricks
        /// </summary>
        private int nb_bricks;
        /// <summary>
        /// Gets or sets the nb_bricks.
        /// </summary>
        /// <value>The nb_bricks.</value>
        public int Nb_bricks
        {
            get { return nb_bricks; }
            set { nb_bricks = value; }
        }

        /// <summary>
        /// The nb bonus
        /// </summary>
        private int nbBonus;

        /// <summary>
        /// The bat
        /// </summary>
        private Bat bat;
        /// <summary>
        /// Gets or sets the bat.
        /// </summary>
        /// <value>The bat.</value>
        public Bat Bat
        {
            get { return bat; }
            private set { bat = value; }
        }

        /// <summary>
        /// The balls
        /// </summary>
        private List<Ball> balls;
        /// <summary>
        /// Gets or sets the balls.
        /// </summary>
        /// <value>The balls.</value>
        public List<Ball> Balls
        {
            get { return balls; }
            private set { balls = value; }
        }


        /// <summary>
        /// Gets or sets the level file.
        /// </summary>
        /// <value>The level file.</value>
        private GameFile levelFile { get; set; }

        /// <summary>
        /// The score
        /// </summary>
        private int score;
        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>The score.</value>
        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        /// <summary>
        /// The brick texture
        /// </summary>
        private BrickTexture brickTexture;
        /// <summary>
        /// Gets or sets the brick texture.
        /// </summary>
        /// <value>The brick texture.</value>
        public BrickTexture BrickTexture
        {
            get { return brickTexture; }
            set { brickTexture = value; }
        }

        /// <summary>
        /// The bat texture
        /// </summary>
        private BatTextures batTexture;
        /// <summary>
        /// Gets or sets the bat texture.
        /// </summary>
        /// <value>The bat texture.</value>
        public BatTextures BatTexture
        {
            get { return batTexture; }
            set { batTexture = value; }
        }


        /// <summary>
        /// The bricks map
        /// </summary>
        private List<Brick> bricksMap;
        /// <summary>
        /// Gets or sets the bricks map.
        /// </summary>
        /// <value>The bricks map.</value>
        public List<Brick> BricksMap
        {
            get { return bricksMap; }
            private set { bricksMap = value; }
        }

        /// <summary>
        /// Gets or sets the background.
        /// </summary>
        /// <value>The background.</value>
        public Texture2D Background { get; private set; }
        /// <summary>
        /// Gets or sets the song.
        /// </summary>
        /// <value>The song.</value>
        public Song Song { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLevel"/> class.
        /// </summary>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="level">The level.</param>
        /// <param name="lines">The lines.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="balls">The balls.</param>
        /// <param name="bat">The bat.</param>
        public GameLevel(int screenWidth, int screenHeight, GameFile level, int lines, int columns, List<Ball> balls, Bat bat)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = (int)(0.6 * screenHeight);
            this.levelFile = level;
            this.Balls = balls;
            this.Bat = bat;
            this.BricksMap = new List<Brick>();
            this.nb_bricks = lines * columns;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            this.Score = 0;
            this.Lives = 3;

            this.levelFile.Load();
            this.LoadLevel();
           
            this.SetBonus();
            this.InitializeBonus();
        }

        /// <summary>
        /// Creates the background.
        /// </summary>
        /// <param name="device">The device.</param>
        public void CreateBackground(GraphicsDevice device)
        {
            try
            {
                byte[] bgbitmap = Convert.FromBase64String((string) this.levelFile.Data.Background["file"]);
                var stream = new MemoryStream(bgbitmap);
                this.Background = Texture2D.FromStream(device, stream);
            }
            catch (ArgumentNullException)
            {
                return;
            }
        }

        /// <summary>
        /// Creates the song.
        /// </summary>
        public void CreateSong()
        {
            try
            {
                byte[] songBytes = Convert.FromBase64String((string) this.levelFile.Data.Music["file"]);
                string temp = Path.GetTempFileName();
                File.WriteAllBytes(temp, songBytes);

                var ctor = typeof(Song).GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.Instance, null,
                    new[] { typeof(string), typeof(string), typeof(int) }, null);
                this.Song = (Song)ctor.Invoke(new object[] { "level", temp, 0 });
            }
            catch (ArgumentNullException)
            {
                return;
            }
        }

        /// <summary>
        /// Updates the specified restart.
        /// </summary>
        /// <param name="restart">if set to <c>true</c> [restart].</param>
        /// <param name="levels">The levels.</param>
        public void Update(bool restart, DefaultLevels levels)
        {
            if (!restart)
            {
                levels.NextLevel();
                this.levelFile = levels.GetLevel();
            }

            this.Initialize();
        }

        /// <summary>
        /// Updates the specified restart.
        /// </summary>
        /// <param name="restart">if set to <c>true</c> [restart].</param>
        /// <param name="file">The file.</param>
        public void Update(bool restart, GameFile file)
        {
            if (!restart)
            {
                this.levelFile = file;
            }

            this.Initialize();
        }

        /// <summary>
        /// Initializes the bonus.
        /// </summary>
        public void InitializeBonus()
        {
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
                }
            }
        }

        /// <summary>
        /// Initializes the board.
        /// </summary>
        /// <param name="bricks">The bricks.</param>
        /// <param name="indestructible">The indestructible.</param>
        /// <param name="percentBonus">The percent bonus.</param>
        private void InitializeBoard(int bricks, int indestructible, double percentBonus)
        {
            this.nb_bricks = bricks - indestructible;
            this.nbBonus = (int)(percentBonus * (double)(bricks));
            this.BricksMap = new List<Brick>();
        }

        /// <summary>
        /// Loads the level.
        /// </summary>
        private void LoadLevel()
        {
            int indestructibles = CountIndestructibles(this.levelFile.Data.Bricks);
            this.InitializeBoard(this.levelFile.Data.Bricks.Count, indestructibles, 0.1);
            
            int margin_h = 20;
            int margin_w = 50;
            int x = 0;
            int y = 2 * margin_h;

            foreach (Dictionary<string, object> brick in this.levelFile.Data.Bricks)
            {
                var line = (double) brick["line"];
                var column = (double) brick["column"];
                var resistance = (double) brick["resistance"];
                this.BricksMap.Add(new Brick(this.screenWidth, this.screenHeight, new Vector2(x + (int) column * margin_w, y + (int) line * margin_h), (int) resistance));
            }
        }

        /// <summary>
        /// Counts the indestructibles.
        /// </summary>
        /// <param name="bricksData">The bricks data.</param>
        /// <returns>System.Int32.</returns>
        private static int CountIndestructibles(List<object> bricksData)
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

        /// <summary>
        /// Sets the bonus.
        /// </summary>
        private void SetBonus()
        {
            Random rnd = new Random();
            Random x_rnd = new Random();

            for (int i = 0; i < this.nbBonus; i++)
            {
                int x = x_rnd.Next(0, this.BricksMap.Count);
                if (this.BricksMap[x].Bonus.Type == BonusType.NONE && this.BricksMap[x].Resistance > 0)
                {
                    Array values = Enum.GetValues(typeof(BonusType));
                    this.BricksMap[x].Bonus.Type = (BonusType)values.GetValue(rnd.Next(1, values.Length));
                }
                else
                {
                    i--;
                }
            }
        }
    }
}
