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
    public class MovingSprite : Sprite {

        protected Vector2 direction;
        public Vector2 Direction {
            get { return direction; }
            set { direction = Vector2.Normalize(value); }
        }

        protected float speed;
        public float Speed {

            get { return speed; }

            set { speed = value; }

        }

        public MovingSprite(int screenWidth, int screenHeight) : base(screenWidth, screenHeight) { }
       
        public virtual void Update(GameTime gameTime) {
            this.position += direction * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public override void Initialize()
        {
            base.Initialize();
            direction = Vector2.Zero;
            speed = 0;
        }

    }
}
