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
    public class Sprite {
        protected int screenHeight;
        protected int screenWidth;

        protected Texture2D texture;
        public Texture2D Texture {
            get { return texture; }
            set { texture = value; }
        }

        protected Vector2 position;
        public Vector2 Position {
            get { return position; }
            set { position = value; }
        }

        public Sprite(int screenWidth, int screenHeight) {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

        }

        public virtual void Initialize() {
            position = Vector2.Zero;
        }

        public virtual void LoadContent(ContentManager content, string assetName) {
            texture = content.Load<Texture2D>(assetName);
        }

        public virtual void HandleInput(KeyboardState keyboardState, MouseState mouseState) {
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public virtual void Update() { }
    }
}
