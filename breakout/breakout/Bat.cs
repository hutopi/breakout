using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace breakout {
    class Bat : MovingSprite {

        private int screenHeight;
        private int screenWidth;

        public Rectangle hitbox {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        public Bat(int screenWidth, int screenHeight ){
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
        }

        public override void Initialize() {
            base.Initialize();
        }

        public override void LoadContent(ContentManager content, string assetName) {
            base.LoadContent(content, assetName);
            Position = new Vector2(screenWidth / 2 - Texture.Width/2, screenHeight - 10 - Texture.Height / 2);
        }

        public override void HandleInput(KeyboardState keyboardState, MouseState mouseState) {

            if (keyboardState.IsKeyDown(Keys.Left)) {
                Direction = -Vector2.UnitX;
                Speed = 0.3f;
            } else if (keyboardState.IsKeyDown(Keys.Right)) {
                Direction = Vector2.UnitX;
                Speed = 0.3f;
            } else {
                Speed = 0;
            }
            //Position = new Vector2(mouseState.X, Position.Y);
            
            base.HandleInput(keyboardState, mouseState);

        }

        public override void Update(GameTime gameTime) {
            if ((Position.X <= 0 && Direction.X < 0) || (Position.X >= screenWidth - Texture.Width && Direction.X > 0))
                Speed = 0;
            base.Update(gameTime);
        }

    }

}
