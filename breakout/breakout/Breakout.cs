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
        private bool isPaused;

        //sprites
        private Bat bat;
        private Ball ball;

        public Breakout() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            bat = new Bat(Window.ClientBounds.Width, Window.ClientBounds.Height);
            bat.Initialize();

            ball = new Ball(Window.ClientBounds.Width, Window.ClientBounds.Height);
            ball.Initialize();

            isPaused = true;
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
            bat.LoadContent(Content, "bat");
            ball.LoadContent(Content, "ball", bat);

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

            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (!isPaused) {
                ball.Update(gameTime, bat.hitbox);
                bat.HandleInput(keyboardState, mouseState);
                bat.Update(gameTime);
                CheckIfBallOut();

            }
            if (keyboardState.IsKeyDown(Keys.Space)) {
                isPaused = !isPaused;
            }

            base.Update(gameTime);
        }

        private void CheckIfBallOut() {
            if (ball.Position.Y > Window.ClientBounds.Height) {
                score--;
                ball.Initialize();
                bat.Position = new Vector2(Window.ClientBounds.Width / 2 - bat.Texture.Width / 2, Window.ClientBounds.Height - 10 - bat.Texture.Height / 2);
                ball.Position = new Vector2(bat.Position.X + bat.Texture.Width / 2 - ball.Texture.Width / 2, bat.Position.Y - bat.Texture.Height - ball.Texture.Height / 2);

                isPaused = true;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            bat.Draw(spriteBatch, gameTime);
            ball.Draw(spriteBatch, gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
