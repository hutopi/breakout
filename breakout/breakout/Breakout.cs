using System;
using System.Collections.Generic;
using System.Linq;
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

        private int lives;
        private GameState gameState;
        private GameLevel gameLevel;

        private KeyboardState keyboardState;
        private KeyboardState previousKeyboardState;
        private MouseState mouseState;
        private MouseState previousMouseState;

        //sprites
        private Bat bat;
        private List<Ball> balls;
        private SpriteFont scoreFont;
        private SpriteFont livesFont;
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

            startButton = new ButtonSprite(screenWidth, screenHeight, "Start");
            exitButton = new ButtonSprite(screenWidth, screenHeight, "Exit");
            resumeButton = new ButtonSprite(screenWidth, screenHeight, "Resume");
            restartButton = new ButtonSprite(screenWidth, screenHeight, "Restart");
            nextLevelButton = new ButtonSprite(screenWidth, screenHeight, "Next level");

            bat = new Bat(screenWidth, screenHeight);
            balls = new List<Ball>();
            balls.Add(new Ball(screenWidth, screenHeight));

            gameLevel = new GameLevel(screenWidth, screenHeight, 1);
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
            lives = 3;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            startButton.LoadContent(Content, "start");
            exitButton.LoadContent(Content, "exit");
            resumeButton.LoadContent(Content, "resume");
            restartButton.LoadContent(Content, "restart");
            nextLevelButton.LoadContent(Content, "next");

            startButton.Position = new Vector2(Window.ClientBounds.Width / 2 - startButton.Texture.Width / 2, Window.ClientBounds.Height * 1 / 3 - startButton.Texture.Height / 2);
            exitButton.Position = new Vector2(Window.ClientBounds.Width / 2 - exitButton.Texture.Width / 2, Window.ClientBounds.Height * 2 / 3 - exitButton.Texture.Height / 2);
            resumeButton.Position = new Vector2(Window.ClientBounds.Width / 2 - resumeButton.Texture.Width / 2, Window.ClientBounds.Height / 2 - resumeButton.Texture.Height / 2);

            restartButton.Position = new Vector2(Window.ClientBounds.Width / 2 - startButton.Texture.Width / 2, Window.ClientBounds.Height * 1 / 3 - startButton.Texture.Height / 2);
            nextLevelButton.Position = new Vector2(Window.ClientBounds.Width / 2 - startButton.Texture.Width / 2, Window.ClientBounds.Height * 1 / 3 - startButton.Texture.Height / 2);

            bat.LoadContent(Content, "bat");
            balls[0].LoadContent(Content, "ball", bat);
            scoreFont = Content.Load<SpriteFont>("Score");
            livesFont = Content.Load<SpriteFont>("Lives");

            gameLevel.brickTexture = new BrickTexture(this.buildTextures());

            foreach (Brick b in gameLevel.BricksMap)
            {
                b.LoadContent(Content, "brick");
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
                    break;
                case GameState.PLAYING:
                    this.IsMouseVisible = false;
                    balls[0].Update(gameTime, bat.hitbox, gameLevel);
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
                    nextLevelButton.Update(mouseState, previousMouseState, ref gameState);
                    exitButton.Update(mouseState, previousMouseState, ref gameState);
                    break;
                case GameState.LOOSE:
                    this.IsMouseVisible = true;
                    restartButton.Update(mouseState, previousMouseState, ref gameState);
                    exitButton.Update(mouseState, previousMouseState, ref gameState);
                    break;
                case GameState.RESTART:
                    break;
                case GameState.NEXT_LEVEL:
                    break;
                case GameState.EXIT:
                    this.Exit();
                    break;
                default:
                    break;
            }

            if (gameState != GameState.PAUSED) {
                // CODE HERE FOR FOREACH BRICKS


            } else {

            }
            if (keyboardState.IsKeyUp(Keys.Space) && previousKeyboardState.IsKeyDown(Keys.Space)) {
                gameState = (gameState == GameState.PAUSED) ? (GameState.PLAYING) : (GameState.PAUSED);
            }

            if (gameLevel.Nb_bricks == 0)
            {
                gameState = GameState.WIN;
            }

            if (lives < 0)
            {
                gameState = GameState.LOOSE;
            }

            previousKeyboardState = keyboardState;
            previousMouseState = mouseState;

            base.Update(gameTime);
        }

        private void CheckIfBallOut() {
            if (balls[0].Position.Y > Window.ClientBounds.Height) {
                lives--;
                balls[0].Initialize();
                bat.Position = new Vector2(Window.ClientBounds.Width / 2 - bat.Texture.Width / 2, Window.ClientBounds.Height - 10 - bat.Texture.Height / 2);
                balls[0].Position = new Vector2(bat.Position.X + bat.Texture.Width / 2 - balls[0].Texture.Width / 2, bat.Position.Y - bat.Texture.Height - balls[0].Texture.Height / 2);

                gameState = GameState.PAUSED;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(Content.Load<Texture2D>("background"),
                             new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                             Color.White);
            switch (gameState) {
                case GameState.STARTMENU:
                    startButton.Draw(spriteBatch, gameTime);
                    exitButton.Draw(spriteBatch, gameTime);
                    break;
                case GameState.PLAYING:
                    bat.Draw(spriteBatch, gameTime);
                    balls[0].Draw(spriteBatch, gameTime);
                    spriteBatch.DrawString(scoreFont, "Score : " + gameLevel.Score.ToString(), new Vector2(10, 10), Color.Blue);
                    spriteBatch.DrawString(livesFont, "Lives : " + lives.ToString(), new Vector2(200, 10), Color.Yellow);
                    foreach (Brick b in gameLevel.BricksMap)
                    {
                        b.Draw(spriteBatch, gameTime);
                    }
                    break;
                case GameState.PAUSED:
                    resumeButton.Draw(spriteBatch, gameTime);
                    spriteBatch.DrawString(scoreFont, "Score : " + gameLevel.Score.ToString(), new Vector2(10, 10), Color.Blue);
                    spriteBatch.DrawString(livesFont, "Lives : " + lives.ToString(), new Vector2(200, 10), Color.Yellow);
                    break;
                case GameState.WIN:
                    nextLevelButton.Draw(spriteBatch, gameTime);
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

        public Texture2D[] buildTextures()
        {
            Texture2D[] textures = new Texture2D[5];
            textures[0] = Content.Load<Texture2D>("brick");
            textures[1] = Content.Load<Texture2D>("brick1");
            textures[2] = Content.Load<Texture2D>("brick2");
            textures[3] = Content.Load<Texture2D>("brick3");
            textures[4] = Content.Load<Texture2D>("brick4");
            return textures;
        }
    }

}
