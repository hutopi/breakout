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
    public class Bat : MovingSprite {

        private Vector2 velocity = Vector2.Zero;
        private Vector2 acceleration = Vector2.Zero;
        public Vector2 Acceleration { get { return acceleration; } set { acceleration = value; } }
        public Rectangle Hitbox {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        public Bat(int screenWidth, int screenHeight) : base(screenWidth, screenHeight) { }

        public override void Initialize() {
            base.Initialize();
        }

        public override void LoadContent(ContentManager content, string assetName) {
            base.LoadContent(content, assetName);
            Position = new Vector2(screenWidth / 2 - Texture.Width / 2, screenHeight - 10 - Texture.Height / 2);
        }

        public override void HandleInput(KeyboardState keyboardState, KeyboardState previousKeyboardState) {

            if (keyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left)) {
                direction = -Vector2.UnitX;
                speed = 0.2f;
                acceleration = Vector2.Zero;
                velocity = new Vector2(-speed, 0);
            } else if (keyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right)) {
                direction = Vector2.UnitX;
                speed = 0.2f;
                acceleration = Vector2.Zero;
                velocity = new Vector2(+speed, 0);
            } else if (keyboardState.IsKeyDown(Keys.Left) && previousKeyboardState.IsKeyDown(Keys.Left)) {
                direction = -Vector2.UnitX;
                speed = 0.2f;
                acceleration = -(new Vector2(2f, 0)); 
                
            } else if (keyboardState.IsKeyDown(Keys.Right) && previousKeyboardState.IsKeyDown(Keys.Right)) {
                direction = Vector2.UnitX;
                speed = 0.2f;
                acceleration = new Vector2(2f, 0); 
            } else {
                speed = 0;
                acceleration = Vector2.Zero;
            }


        }

        public override void Update(GameTime gameTime) {
            if ((position.X <= 0  && direction.X < 0) || (position.X >= screenWidth - Texture.Width  && direction.X > 0)) { 
                speed = 0;
                acceleration = Vector2.Zero;
            }
            if (acceleration == Vector2.Zero) {
                base.Update(gameTime);

            }else{
                velocity = velocity + (acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds);
                this.position = position + ( velocity *(float)gameTime.ElapsedGameTime.TotalMilliseconds);

            }
        }

    }

}
