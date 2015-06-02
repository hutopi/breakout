using System;
using System.Collections.Generic;
using System.Linq;
using breakout.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace breakout {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Breakout : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D logo;

        private GameState gameState;
        private GameLevel gameLevel;

        private KeyboardState keyboardState;
        private KeyboardState previousKeyboardState;
        private MouseState mouseState;
        private MouseState previousMouseState;

        //sprites
        private Arrow arrow;
        private Bat bat;
        private List<Ball> balls;
        private SpriteFont scoreFont;
        private SpriteFont helpControlFont;
        private Sprite[] livesSprites;
        private ButtonSprite startButton;
        private ButtonSprite exitButton;
        private ButtonSprite resumeButton;
        private ButtonSprite restartButton;
        private ButtonSprite nextLevelButton;

        public Breakout() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            Content.RootDirectory = "Content";
            int screenWidth = Window.ClientBounds.Width;
            int screenHeight = Window.ClientBounds.Height;

            startButton = new ButtonSprite(screenWidth, screenHeight, "start");
            exitButton = new ButtonSprite(screenWidth, screenHeight, "exit");
            resumeButton = new ButtonSprite(screenWidth, screenHeight, "resume");
            restartButton = new ButtonSprite(screenWidth, screenHeight, "restart");
            nextLevelButton = new ButtonSprite(screenWidth, screenHeight, "next");
            arrow = new Arrow(screenWidth, screenHeight);

            bat = new Bat(screenWidth, screenHeight);
            balls = new List<Ball>();
            balls.Add(new Ball(screenWidth, screenHeight));
            livesSprites = new Sprite[5];
            for (int i = 0; i < 5; i++)
            {
                livesSprites[i] = new Sprite(screenWidth, screenHeight);
            }

            gameLevel = new GameLevel(screenWidth, screenHeight, 1, balls, bat);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            startButton.Initialize();
            exitButton.Initialize();
            resumeButton.Initialize();
            restartButton.Initialize();
            nextLevelButton.Initialize();


            bat.Initialize();

            foreach (var ball in balls) {
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.logo = Content.Load<Texture2D>("logo");
            startButton.LoadContent(Content, "start");
            exitButton.LoadContent(Content, "exit");
            resumeButton.LoadContent(Content, "resume");
            restartButton.LoadContent(Content, "restart");
            nextLevelButton.LoadContent(Content, "next");

            startButton.Position = new Vector2(Window.ClientBounds.Width / 2 - 200, Window.ClientBounds.Height/2);
            exitButton.Position = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
            resumeButton.Position = new Vector2(Window.ClientBounds.Width / 2 - resumeButton.Texture.Width / 2, Window.ClientBounds.Height / 2 - resumeButton.Texture.Height / 2);

            restartButton.Position = new Vector2(Window.ClientBounds.Width / 2 - startButton.Texture.Width / 2, Window.ClientBounds.Height * 1 / 3 - startButton.Texture.Height / 2);
            nextLevelButton.Position = new Vector2(Window.ClientBounds.Width / 2 - startButton.Texture.Width / 2, Window.ClientBounds.Height * 1 / 3 - startButton.Texture.Height / 2);

            bat.LoadContent(Content, "bat");
            foreach (Ball b in balls)
            {
                b.LoadContent(Content, "ball", bat);
            }
            scoreFont = Content.Load<SpriteFont>("Score");
            helpControlFont = Content.Load<SpriteFont>("helpControls");
            for (int i = 0; i < 5; i++)
            {
                livesSprites[i].LoadContent(Content, "lives" + i);
            }

            gameLevel.BrickTexture = new BrickTexture(this.buildBrickTextures());
            gameLevel.BatTexture = this.buildBatTextures();

            foreach (Brick b in gameLevel.BricksMap) {
                b.LoadContent(Content, "brick");
                if (b.Bonus.Type != BonusType.NONE)
                {
                    b.Bonus.LoadContent(Content, b.Bonus.Name);
                }
            }
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            /* // Allows the game to exit
             if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                 this.Exit();*/

            // TODO: Add your update logic here
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            switch (gameState) {
                case GameState.STARTMENU:
                    this.IsMouseVisible = true;
                    startButton.Update(mouseState, previousMouseState, ref gameState);
                    exitButton.Update(mouseState, previousMouseState, ref gameState);
                    arrow.HandleInput(keyboardState, mouseState);
                    break;
                case GameState.PLAYING:
                    this.IsMouseVisible = false;
                    foreach (Ball b in balls)
                    {
                        b.Update(gameTime, bat.Hitbox, gameLevel, true);
                    }
                    foreach(Brick b in gameLevel.BricksMap){
                        if (b.Bonus.Activated == true)
                        {
                            b.Bonus.Update(gameTime, bat.Hitbox, gameLevel, b);
                        }
                    }
                    bat.HandleInput(keyboardState, mouseState);
                    bat.Update(gameTime);
                    CheckIfBallOut();
                    break;
                case GameState.PAUSED:
                    this.IsMouseVisible = true;
                    resumeButton.Update(mouseState, previousMouseState, ref gameState);
                    break;
                case GameState.WIN:
                    this.IsMouseVisible = true;
                    if (gameLevel.Level < 3)
                    {
                        nextLevelButton.Update(mouseState, previousMouseState, ref gameState);
                    }
                    else
                    {
                        // Bravo, vous avez terminé le jeu ! @TODO
                    }
                    exitButton.Update(mouseState, previousMouseState, ref gameState);
                    break;
                case GameState.LOOSE:
                    this.IsMouseVisible = true;
                    restartButton.Update(mouseState, previousMouseState, ref gameState);
                    exitButton.Update(mouseState, previousMouseState, ref gameState);
                    break;
                case GameState.RESTART:
                    this.UpdateLevel(true);
                    break;
                case GameState.READYTOSTART:
                    this.IsMouseVisible = false;
                    foreach (Ball b in balls)
                    {
                        b.Update(gameTime, bat.Hitbox, gameLevel, false);
                    }
                    bat.HandleInput(keyboardState, mouseState);
                    bat.Update(gameTime);
                    break;
                case GameState.NEXT_LEVEL:
                    this.UpdateLevel(false);
                    break;
                case GameState.EXIT:
                    this.Exit();
                    break;
                default:
                    break;
            }

            
            if (gameState == GameState.READYTOSTART && keyboardState.IsKeyUp(Keys.Enter) && previousKeyboardState.IsKeyDown(Keys.Enter)) {
                gameState = GameState.PLAYING;
            }
            if (keyboardState.IsKeyUp(Keys.Space) && previousKeyboardState.IsKeyDown(Keys.Space) && gameState != GameState.STARTMENU ) {

                gameState = (gameState == GameState.PAUSED) ? (GameState.PLAYING) : (GameState.PAUSED);
            }

            if (gameLevel.Nb_bricks == 0 && gameState != GameState.NEXT_LEVEL && gameState != GameState.EXIT) {
                gameState = GameState.WIN;
            }

            if (gameLevel.Lives < 0 && gameState != GameState.RESTART && gameState != GameState.EXIT) {
                gameState = GameState.LOOSE;
            }

            previousKeyboardState = keyboardState;
            previousMouseState = mouseState;

           

            base.Update(gameTime);
        }

        private void UpdateLevel(bool restart)
        {
            gameLevel.Lives = 3;
            gameLevel.Score = 0;
            gameLevel.Update(restart);
            foreach (Brick b in gameLevel.BricksMap)
            {
                b.LoadContent(Content, "brick");
            }
            gameState = GameState.READYTOSTART;
        }

        private void CheckIfBallOut() {
            int numberOfBalls = balls.Count;
            foreach (Ball b in balls)
            {
                if (b.Position.Y > bat.Position.Y - 2)
                {
                    numberOfBalls--;
                }
            }

            if (numberOfBalls == 0)
            {
                gameLevel.Lives--;
                if (balls.Count > 1)
                {
                    balls.RemoveRange(1, balls.Count - 1);
                }

                balls[0].Initialize();
                bat.Texture = gameLevel.BatTexture.Regular;
                bat.Position = new Vector2(Window.ClientBounds.Width / 2 - bat.Texture.Width / 2, Window.ClientBounds.Height - 10 - bat.Texture.Height / 2);
                balls[0].Position = new Vector2(bat.Position.X + bat.Texture.Width / 2 - balls[0].Texture.Width / 2, bat.Position.Y - bat.Texture.Height - balls[0].Texture.Height / 2);

                gameState = GameState.READYTOSTART;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(Content.Load<Texture2D>("background2"),
                             new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                             Color.White);
            switch (gameState) {
                case GameState.STARTMENU:
                    spriteBatch.Draw(this.logo, new Vector2(Window.ClientBounds.Width/2 - 200, 80), Color.White);
                    startButton.Draw(spriteBatch, gameTime);
                    exitButton.Draw(spriteBatch, gameTime);
                    break;
                case GameState.PLAYING:
                    bat.Draw(spriteBatch, gameTime);
                    foreach (Ball b in balls)
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
                    spriteBatch.DrawString(scoreFont, "Score : " + gameLevel.Score.ToString(), new Vector2(10, 10), Color.Blue);
                    this.getLives(ref spriteBatch, gameTime);
                    break;
                case GameState.READYTOSTART:
                    bat.Draw(spriteBatch, gameTime);
                    foreach (Ball b in balls)
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
                    spriteBatch.DrawString(scoreFont, "Score : " + gameLevel.Score.ToString(), new Vector2(10, 10), Color.Blue);
                    spriteBatch.DrawString(helpControlFont, "Press Enter to launch the ball and Space to pause the game", new Vector2(200,10),Color.White);
                    this.getLives(ref spriteBatch, gameTime);
                    break;
                case GameState.PAUSED:
                    bat.Draw(spriteBatch, gameTime);
                    foreach (Ball b in balls)
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
                    spriteBatch.DrawString(scoreFont, "Score : " + gameLevel.Score.ToString(), new Vector2(10, 10), Color.Blue);
                    this.getLives(ref spriteBatch, gameTime);
                    break;
                case GameState.WIN:
                    if (gameLevel.Level < 3)
                    {
                        nextLevelButton.Draw(spriteBatch, gameTime);
                    }
                    else
                    {
                        // Affiche du SpriteFont : Vous avez terminé le jeu ! @TODO
                    }
                    exitButton.Draw(spriteBatch, gameTime);
                    break;
                case GameState.LOOSE:
                    restartButton.Draw(spriteBatch, gameTime);
                    exitButton.Draw(spriteBatch, gameTime);
                    break;
                default:
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Texture2D[] buildBrickTextures() {
            Texture2D[] textures = new Texture2D[5];
            textures[0] = Content.Load<Texture2D>("brick");
            textures[1] = Content.Load<Texture2D>("brick1");
            textures[2] = Content.Load<Texture2D>("brick2");
            textures[3] = Content.Load<Texture2D>("brick3");
            textures[4] = Content.Load<Texture2D>("brick4");
            return textures;
        }

        public BatTextures buildBatTextures() {
            Texture2D reduced = Content.Load<Texture2D>("reduced_bat");
            Texture2D regular = Content.Load<Texture2D>("bat");
            Texture2D extended = Content.Load<Texture2D>("extended_bat");
            return new BatTextures(reduced, regular, extended);

        }

        public void getLives(ref SpriteBatch spriteBatch, GameTime gameTime)
        {
            livesSprites[gameLevel.Lives].Draw(spriteBatch, gameTime);

        }
    }

}
