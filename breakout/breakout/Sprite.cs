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
    class Sprite {
        private Texture2D texture;
        public Texture2D Texture {
            get { return texture; }
            set { texture = value; }
        }

        private Vector2 position;
        public Vector2 Position {
            get { return position; }
            set { position = value; }
        }

        private Vector2 direction;
        public Vector2 Direction {
            get { return direction; }
            set { direction = Vector2.Normalize(value); }
        }

        private float speed;
        public float Speed {

            get { return speed; }

            set { speed = value; }

        }

        public virtual void Initialize() {
            position = Vector2.Zero;
            direction = Vector2.Zero;
            speed = 0;
        }

        public virtual void LoadContent(ContentManager content, string assetName) {
            texture = content.Load<Texture2D>(assetName);
        }

        public virtual void Update(GameTime gameTime) {
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public virtual void HandleInput(KeyboardState keyboardState, MouseState mouseState) {
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            spriteBatch.Draw(texture, position, Color.White);
        }




    }
}
