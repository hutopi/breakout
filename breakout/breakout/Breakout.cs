// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Thomas Fossati
// Created          : 05-24-2015
//
// Last Modified By : Thomas Fossati
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="Breakout.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//      Breakout is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.IO;
using breakout.GameComponents;
using breakout.Textures;
using breakout.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

/// <summary>
/// The breakout namespace.
/// </summary>
namespace breakout {

    /// <summary>
    /// Class Breakout.
    /// </summary>
    public class Breakout : Game {
        /// <summary>
        /// The graphics
        /// </summary>
        GraphicsDeviceManager graphics;
        /// <summary>
        /// The sprite batch
        /// </summary>
        SpriteBatch spriteBatch;
        /// <summary>
        /// The logo
        /// </summary>
        private Texture2D logo;
        /// <summary>
        /// The sounds
        /// </summary>
        private Sounds sounds;
        /// <summary>
        /// The song start
        /// </summary>
        private bool songStart = false;
        /// <summary>
        /// The Mute song
        /// </summary>
        private bool muteSong = false;

        /// <summary>
        /// The game state
        /// </summary>
        private GameState gameState;
        /// <summary>
        /// The game level
        /// </summary>
        private GameLevel gameLevel;
        /// <summary>
        /// The default levels
        /// </summary>
        private DefaultLevels defaultLevels;

        /// <summary>
        /// The keyboard state
        /// </summary>
        private KeyboardState keyboardState;
        /// <summary>
        /// The previous keyboard state
        /// </summary>
        private KeyboardState previousKeyboardState;
        /// <summary>
        /// The mouse state
        /// </summary>
        private MouseState mouseState;
        /// <summary>
        /// The previous mouse state
        /// </summary>
        private MouseState previousMouseState;

        /// <summary>
        /// The menu arrow
        /// </summary>
        private MenuArrow menuArrow;
        /// <summary>
        /// The score font
        /// </summary>
        private SpriteFont scoreFont;
        /// <summary>
        /// The help control font
        /// </summary>
        private SpriteFont helpControlFont;
        /// <summary>
        /// The lives sprites
        /// </summary>
        private Sprite[] livesSprites;
        /// <summary>
        /// The story mode button
        /// </summary>
        private ButtonSprite storyModeButton;
        /// <summary>
        /// The custom mode button
        /// </summary>
        private ButtonSprite customModeButton;
        /// <summary>
        /// The exit button
        /// </summary>
        private ButtonSprite exitButton;
        /// <summary>
        /// The resume button
        /// </summary>
        private ButtonSprite resumeButton;
        /// <summary>
        /// The restart button
        /// </summary>
        private ButtonSprite restartButton;
        /// <summary>
        /// The next level button
        /// </summary>
        private ButtonSprite nextLevelButton;

        /// <summary>
        /// The custom mode
        /// </summary>
        private bool customMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="Breakout"/> class.
        /// </summary>
        public Breakout() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            Content.RootDirectory = "Content";
            int screenWidth = Window.ClientBounds.Width;
            int screenHeight = Window.ClientBounds.Height;

            storyModeButton = new ButtonSprite(screenWidth, screenHeight, "story");
            customModeButton = new ButtonSprite(screenWidth, screenHeight, "custom");
            exitButton = new ButtonSprite(screenWidth, screenHeight, "exit");
            resumeButton = new ButtonSprite(screenWidth, screenHeight, "resume");
            restartButton = new ButtonSprite(screenWidth, screenHeight, "restart");
            nextLevelButton = new ButtonSprite(screenWidth, screenHeight, "next");
            menuArrow = new MenuArrow(screenWidth, screenHeight);


            livesSprites = new Sprite[5];
            for (int i = 0; i < 5; i++)
            {
                livesSprites[i] = new Sprite(screenWidth, screenHeight);
            }

            this.defaultLevels = new DefaultLevels();
            this.defaultLevels.LoadFiles();

            GameFile levelFile = this.defaultLevels.GetLevel();
            gameLevel = new GameLevel(screenWidth, screenHeight, levelFile, 3, 6, new List<Ball>(), new Bat(screenWidth, screenHeight));
            gameLevel.Balls.Add(new Ball(screenWidth, screenHeight));
            sounds = new Sounds();
        }

        /// <summary>
        /// Called after the Game and GraphicsDevice are created, but before LoadContent.  Reference page contains code sample.
        /// </summary>
        protected override void Initialize() {
            storyModeButton.Initialize();
            customModeButton.Initialize();
            exitButton.Initialize();
            resumeButton.Initialize();
            restartButton.Initialize();
            nextLevelButton.Initialize();
            gameLevel.Bat.Initialize();
            menuArrow.Initialize();

            foreach (var ball in gameLevel.Balls) {
                ball.Initialize();
            }

            gameLevel.Initialize();

            gameState = GameState.STARTMENU;
            foreach (Sprite p in livesSprites)
            {
                p.Initialize();
                p.Position = new Vector2(Window.ClientBounds.Width-100, 10);
            }

            gameLevel.InitializeBonus();

            base.Initialize();
            gameLevel.CreateBackground(GraphicsDevice);
            gameLevel.CreateSong();
        }


        /// <summary>
        /// Loads the content.
        /// </summary>
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.logo = Content.Load<Texture2D>("logo");
            storyModeButton.LoadContent(Content, "story");
            customModeButton.LoadContent(Content, "custom");

            exitButton.LoadContent(Content, "exit");
            resumeButton.LoadContent(Content, "resume");
            restartButton.LoadContent(Content, "restart");
            nextLevelButton.LoadContent(Content, "next");
            menuArrow.LoadContent(Content, "arrow");

            storyModeButton.Position = new Vector2(Window.ClientBounds.Width / 2 - 200, Window.ClientBounds.Height / 2);
            customModeButton.Position = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
            exitButton.Position = new Vector2(Window.ClientBounds.Width / 2 - 100, Window.ClientBounds.Height / 2 + 120);

            resumeButton.Position = new Vector2(Window.ClientBounds.Width / 2 - resumeButton.Texture.Width / 2, Window.ClientBounds.Height / 2 - resumeButton.Texture.Height / 2);
            restartButton.Position = new Vector2(Window.ClientBounds.Width / 2 - 100, Window.ClientBounds.Height / 2);
            nextLevelButton.Position = new Vector2(Window.ClientBounds.Width / 2 - 100, Window.ClientBounds.Height / 2);


            menuArrow.ButtonGroup.Add(storyModeButton);
            menuArrow.ButtonGroup.Add(customModeButton);
            menuArrow.ButtonGroup.Add(exitButton);

            gameLevel.Bat.LoadContent(Content, "bat");
            foreach (Ball b in gameLevel.Balls)
            {
                b.LoadContent(Content, "ball", gameLevel.Bat);
            }

            scoreFont = Content.Load<SpriteFont>("Score");
            helpControlFont = Content.Load<SpriteFont>("helpControls");
            for (int i = 0; i < 5; i++)
            {
                livesSprites[i].LoadContent(Content, "lives" + i);
            }

            gameLevel.BrickTexture = new BrickTexture(this.BuildBrickTextures());
            gameLevel.BatTexture = this.BuildBatTextures();

            this.PutBricksTexture();

            sounds.LoadContent(Content.Load<SoundEffect>("bump"),
                               Content.Load<SoundEffect>("pause"),
                               Content.Load<SoundEffect>("win"),
                               Content.Load<SoundEffect>("loose"));
        }


        /// <summary>
        /// Called when graphics resources need to be unloaded. Override this method to unload any game-specific graphics resources.
        /// </summary>
        protected override void UnloadContent() {
            
        }


        /// <summary>
        /// Reference page contains links to related conceptual articles.
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Update.</param>
        protected override void Update(GameTime gameTime) {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            switch (gameState) {
                case GameState.STARTMENU:
                    this.IsMouseVisible = true;
                    storyModeButton.Update(mouseState, previousMouseState, ref gameState);
                    customModeButton.Update(mouseState, previousMouseState, ref gameState);
                    exitButton.Update(mouseState, previousMouseState, ref gameState);
                    menuArrow.Update(keyboardState, previousKeyboardState, ref gameState);
                    this.customMode = false;
                    break;
                case GameState.CUSTOM:
                    this.customMode = true;
                    LevelSelection();
                    break;
                case GameState.PLAYING:
                    this.CheckSong();
                    this.IsMouseVisible = false;
                    foreach (Ball b in gameLevel.Balls)
                    {
                        b.Update(gameTime, gameLevel.Bat.Hitbox, gameLevel, true);
                    }

                    foreach(Brick b in gameLevel.BricksMap){
                        if (b.Bonus.Activated == true)
                        {
                            b.Bonus.Update(gameTime, gameLevel.Bat.Hitbox, gameLevel, b);
                        }
                    }
                    gameLevel.Bat.HandleInput(keyboardState, previousKeyboardState);
                    gameLevel.Bat.Update(gameTime);
                    CheckIfBallOut();
                    break;
                case GameState.PAUSED:
                    if (songStart)
                    {
                        MediaPlayer.Pause();
                        this.sounds.Pause.Play();
                        songStart = false;
                    }
                    this.IsMouseVisible = true;
                    resumeButton.Update(mouseState, previousMouseState, ref gameState);
                    break;
                case GameState.WIN:
                    if (songStart)
                    {
                        MediaPlayer.Stop();
                        songStart = false;
                        this.sounds.Win.Play();
                    }
                    this.ResetBat();
                    this.IsMouseVisible = true;
                    exitButton.Update(mouseState, previousMouseState, ref gameState);
                   
                    if (this.defaultLevels.IsLastLevel() && !this.customMode)
                    {
                        nextLevelButton.Update(mouseState, previousMouseState, ref gameState);
                    }
                    else
                    {
                        menuArrow.CurrentButtonSelected = exitButton;
                    }
                    menuArrow.Update(keyboardState, previousKeyboardState, ref gameState);
                    break;
                case GameState.LOOSE:
                    if (songStart)
                    {
                        MediaPlayer.Stop();
                        songStart = false;
                        this.sounds.Loose.Play();
                    }
                    this.IsMouseVisible = true;
                    this.ResetBat();
                    restartButton.Update(mouseState, previousMouseState, ref gameState);
                    exitButton.Update(mouseState, previousMouseState, ref gameState);
                    menuArrow.Update(keyboardState, previousKeyboardState, ref gameState);
                    break;
                case GameState.RESTART:
                    if (songStart)
                    {
                        MediaPlayer.Stop();
                        songStart = false;
                    }
                    this.UpdateLevel(true);
                    break;
                case GameState.READYTOSTART:
                    this.IsMouseVisible = false;
                    this.ResetBalls();
                    foreach (Ball b in gameLevel.Balls)
                    {
                        b.Update(gameTime, gameLevel.Bat.Hitbox, gameLevel, false);
                    }
                    gameLevel.Bat.HandleInput(keyboardState, previousKeyboardState);
                    gameLevel.Bat.Update(gameTime);
                    break;
                case GameState.NEXT_LEVEL:
                    this.UpdateLevel(false);
                    this.PutBricksTexture();
                    break;
                case GameState.EXIT:
                    this.Exit();
                    break;
                default:
                    break;
            }

            
            if (gameState == GameState.READYTOSTART && previousKeyboardState.IsKeyDown(Keys.Space) && keyboardState.IsKeyUp(Keys.Space)) {
                gameState = GameState.PLAYING;
            }
            if (keyboardState.IsKeyUp(Keys.Escape) && previousKeyboardState.IsKeyDown(Keys.Escape) && gameState != GameState.STARTMENU) {

                gameState = (gameState == GameState.PAUSED) ? (GameState.PLAYING) : (GameState.PAUSED);
            }

            if (keyboardState.IsKeyUp(Keys.M) && previousKeyboardState.IsKeyDown(Keys.M) && gameState != GameState.STARTMENU)
            {
                muteSong = (muteSong == false) ? (true) : (false);
                this.Mute();
            }

            if (gameLevel.Nb_bricks == 0 && gameState != GameState.NEXT_LEVEL && gameState != GameState.EXIT) {
                gameState = GameState.WIN;
                menuArrow.ButtonGroup.Clear();
                if (this.defaultLevels.IsLastLevel() && !this.customMode) {
                    menuArrow.ButtonGroup.Add(nextLevelButton);
                }
                menuArrow.ButtonGroup.Add(exitButton);


               
            }

            if (gameLevel.Lives < 0 && gameState != GameState.RESTART && gameState != GameState.EXIT) {
                gameState = GameState.LOOSE;
                menuArrow.ButtonGroup.Clear();
                menuArrow.ButtonGroup.Add(restartButton);
                menuArrow.ButtonGroup.Add(exitButton);
            }

            previousKeyboardState = keyboardState;
            previousMouseState = mouseState;
            base.Update(gameTime);
        }

        /// <summary>
        /// Checks the song.
        /// </summary>
        private void CheckSong()
        {
            if (!songStart)
            {
                if (MediaPlayer.State == MediaState.Paused)
                {
                    MediaPlayer.Resume();
                }
                else
                {
                    MediaPlayer.Play(this.gameLevel.Song);
                    MediaPlayer.IsRepeating = true;
                }
                songStart = true;
            }
        }

        /// <summary>
        /// Mutes this instance.
        /// </summary>
        private void Mute()
        {
            if (muteSong)
            {
                MediaPlayer.IsMuted = true;
            }
            else
            {
                MediaPlayer.IsMuted = false;
            }
        }

        /// <summary>
        /// Updates the level.
        /// </summary>
        /// <param name="restart">if set to <c>true</c> [restart].</param>
        private void UpdateLevel(bool restart)
        {
            gameLevel.Lives = 3;
            gameLevel.Score = 0;
            gameLevel.Update(restart, this.defaultLevels);
            if (!restart)
            {
                gameLevel.CreateBackground(GraphicsDevice);
                gameLevel.CreateSong();
            }

            this.PutBricksTexture();
            gameState = GameState.READYTOSTART;
        }

        /// <summary>
        /// Levels the selection.
        /// </summary>
        private void LevelSelection()
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.DefaultExt = "json";
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            dialog.Filter = "Game level files (*.json)|*.json|All files (*.*)|*.*";
            dialog.ShowDialog();

            if (dialog.FileName == "")
            {
                gameState = GameState.STARTMENU;
            }
            else
            {
                GameFile level = new GameFile(dialog.FileName);
                gameLevel.Lives = 3;
                gameLevel.Score = 0;
                gameLevel.Update(false, level);
                gameLevel.CreateBackground(GraphicsDevice);

                gameState = GameState.READYTOSTART;
                gameLevel.CreateSong();
                this.PutBricksTexture();
                gameState = GameState.READYTOSTART;
            }
        }

        /// <summary>
        /// Checks if ball out.
        /// </summary>
        private void CheckIfBallOut() {
            int numberOfBalls = gameLevel.Balls.Count;
            foreach (Ball b in gameLevel.Balls)
            {
                if (b.Position.Y > gameLevel.Bat.Position.Y - 2)
                {
                    numberOfBalls--;
                }
            }

            if (numberOfBalls == 0)
            {
                gameLevel.Lives--;
                this.ResetBat();
                gameState = GameState.READYTOSTART;
            }
        }

        /// <summary>
        /// Resets the balls.
        /// </summary>
        private void ResetBalls()
        {
            if (gameLevel.Balls.Count > 1)
            {
                gameLevel.Balls.RemoveRange(1, gameLevel.Balls.Count - 1);
            }

            gameLevel.Balls[0].Initialize();
           
        }

        /// <summary>
        /// Resets the bat.
        /// </summary>
        private void ResetBat()
        {
            gameLevel.Bat.Texture = gameLevel.BatTexture.Regular;
            gameLevel.Bat.Position = new Vector2(Window.ClientBounds.Width / 2 - gameLevel.Bat.Texture.Width / 2, Window.ClientBounds.Height - 10 - gameLevel.Bat.Texture.Height / 2);
        }

        /// <summary>
        /// Reference page contains code sample.
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        protected override void Draw(GameTime gameTime) {
            spriteBatch.Begin();
            spriteBatch.Draw(gameLevel.Background,
                             new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                             Color.White);
            switch (gameState) {
                case GameState.STARTMENU:
                    spriteBatch.Draw(this.logo, new Vector2(Window.ClientBounds.Width/2 - 200, 80), Color.White);
                    storyModeButton.Draw(spriteBatch, gameTime);
                    customModeButton.Draw(spriteBatch, gameTime);
                    exitButton.Draw(spriteBatch, gameTime);
                    menuArrow.Draw(spriteBatch, gameTime);
                    break;
                case GameState.PLAYING:
                    gameLevel.Bat.Draw(spriteBatch, gameTime);
                    foreach (Ball b in gameLevel.Balls)
                    {
                        b.Draw(spriteBatch, gameTime);
                    }

                    foreach (Brick b in gameLevel.BricksMap)
                    {
                        b.Draw(spriteBatch, gameTime);
                    }

                    foreach (Brick b in gameLevel.BricksMap)
                    {
                        if (b.Bonus.Activated == true)
                        {
                            b.Bonus.Draw(spriteBatch, gameTime);
                        }
                    }
                    spriteBatch.DrawString(scoreFont, "Score : " + gameLevel.Score.ToString(), new Vector2(10, 10), Color.White);
                    this.GetLives(ref spriteBatch, gameTime);
                    break;
                case GameState.READYTOSTART:
                    gameLevel.Bat.Draw(spriteBatch, gameTime);
                    foreach (Ball b in gameLevel.Balls)
                    {
                        b.Draw(spriteBatch, gameTime);
                    }

                    foreach (Brick b in gameLevel.BricksMap)
                    {
                        b.Draw(spriteBatch, gameTime);
                    }

                    foreach (Brick b in gameLevel.BricksMap)
                    {
                        if (b.Bonus.Activated == true)
                        {
                            b.Bonus.Draw(spriteBatch, gameTime);
                        }
                    }
                    spriteBatch.DrawString(scoreFont, "Score : " + gameLevel.Score.ToString(), new Vector2(10, 10), Color.White);
                    spriteBatch.DrawString(helpControlFont, "Press Space to launch the ball and Escape to pause the game", new Vector2(200,10),Color.White);
                    this.GetLives(ref spriteBatch, gameTime);
                    break;
                case GameState.CUSTOM:
                    //treatment here @TODO
                    break;
                case GameState.PAUSED:
                    gameLevel.Bat.Draw(spriteBatch, gameTime);
                    foreach (Ball b in gameLevel.Balls)
                    {
                        b.Draw(spriteBatch, gameTime);
                    }

                    foreach (Brick b in gameLevel.BricksMap)
                    {
                        b.Draw(spriteBatch, gameTime);
                    }

                    foreach (Brick b in gameLevel.BricksMap)
                    {
                        if (b.Bonus.Activated == true)
                        {
                            b.Bonus.Draw(spriteBatch, gameTime);
                        }
                    }
                    resumeButton.Draw(spriteBatch, gameTime);
                    spriteBatch.DrawString(scoreFont, "Score : " + gameLevel.Score.ToString(), new Vector2(10, 10), Color.White);
                    this.GetLives(ref spriteBatch, gameTime);
                    break;
                case GameState.WIN:
                    spriteBatch.DrawString(scoreFont, "Score : " + gameLevel.Score.ToString(), new Vector2(10, 10), Color.White);
                    if (this.defaultLevels.IsLastLevel() && !this.customMode)
                    {
                        nextLevelButton.Draw(spriteBatch, gameTime);
                    }
                    exitButton.Draw(spriteBatch, gameTime);
                    menuArrow.Draw(spriteBatch, gameTime);
                    break;
                case GameState.LOOSE:
                    restartButton.Draw(spriteBatch, gameTime);
                    exitButton.Draw(spriteBatch, gameTime);
                    menuArrow.Draw(spriteBatch, gameTime);
                    break;
                default:
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Builds the brick textures.
        /// </summary>
        /// <returns>Texture2D[].</returns>
        public Texture2D[] BuildBrickTextures() {
            Texture2D[] textures = new Texture2D[5];
            for (int i = 0; i < 5; i++)
            {
                textures[i] = Content.Load<Texture2D>("brick" + i);
            }
            return textures;
        }

        /// <summary>
        /// Builds the bat textures.
        /// </summary>
        /// <returns>BatTextures.</returns>
        private BatTextures BuildBatTextures() {
            Texture2D reduced = Content.Load<Texture2D>("reduced_bat");
            Texture2D regular = Content.Load<Texture2D>("bat");
            Texture2D extended = Content.Load<Texture2D>("extended_bat");
            return new BatTextures(reduced, regular, extended);

        }

        /// <summary>
        /// Puts the bricks texture.
        /// </summary>
        private void PutBricksTexture()
        {
            foreach (Brick b in gameLevel.BricksMap)
            {
                b.LoadContent(Content, "brick");
                if (b.Bonus.Type != BonusType.NONE)
                {
                    b.Bonus.LoadContent(Content, b.Bonus.Name);
                }
            }
        }

        /// <summary>
        /// Gets the lives.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        private void GetLives(ref SpriteBatch spriteBatch, GameTime gameTime)
        {
            livesSprites[gameLevel.Lives].Draw(spriteBatch, gameTime);

        }
    }

}
