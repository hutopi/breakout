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

        private int score;
        private GameState gameState;

        private KeyboardState keyboardState;
        private KeyboardState previousKeyboardState;
        private MouseState mouseState;
        private MouseState previousMouseState;

        //sprites
        private Bat bat;
        private List<Ball> balls;
        private List<Brick> bricks;
        private SpriteFont scoreFont;
        private ButtonSprite startButton;
        private ButtonSprite exitButton;
        private ButtonSprite resumeButton;

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

            bat = new Bat(screenWidth, screenHeight);
            balls = new List<Ball>();
            balls.Add(new Ball(screenWidth, screenHeight));
            bricks = new List<Brick>();

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


            bat.Initialize();


            foreach (var ball in balls) {
                ball.Initialize();

            }


            this.AddBricks();

            gameState = GameState.STARTMENU;
            score = 0;

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

            startButton.Position = new Vector2(Window.ClientBounds.Width / 2 - startButton.Texture.Width / 2, Window.ClientBounds.Height * 1 / 3 - startButton.Texture.Height / 2);
            exitButton.Position = new Vector2(Window.ClientBounds.Width / 2 - exitButton.Texture.Width / 2, Window.ClientBounds.Height * 2 / 3 - exitButton.Texture.Height / 2);
            resumeButton.Position = new Vector2(Window.ClientBounds.Width / 2 - resumeButton.Texture.Width / 2, Window.ClientBounds.Height / 2 - resumeButton.Texture.Height / 2);


            bat.LoadContent(Content, "bat");
            balls[0].LoadContent(Content, "ball", bat);
            scoreFont = Content.Load<SpriteFont>("Score");



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
                    balls[0].Update(gameTime, bat.hitbox);
                    bat.HandleInput(keyboardState, mouseState);
                    bat.Update(gameTime);
                    CheckIfBallOut();
                    break;
                case GameState.PAUSED:
                    this.IsMouseVisible = true;
                    resumeButton.Update(mouseState, previousMouseState, ref gameState);
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

            previousKeyboardState = keyboardState;
            previousMouseState = mouseState;

            base.Update(gameTime);
        }

        private void CheckIfBallOut() {
            if (balls[0].Position.Y > Window.ClientBounds.Height) {
                score--;
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
                    spriteBatch.DrawString(scoreFont, "Score : " + score.ToString(), new Vector2(10, 10), Color.Blue);
                    break;
                case GameState.PAUSED:
                    resumeButton.Draw(spriteBatch, gameTime);
                    spriteBatch.DrawString(scoreFont, "Score : " + score.ToString(), new Vector2(10, 10), Color.Blue);
                    break;
                default:
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void AddBricks() {
            //Window.ClientBounds.Width Window.ClientBounds.Height

        }
    }

}
