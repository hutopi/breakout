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
    class ButtonSprite : Sprite {
        public Rectangle hitbox {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        public ButtonSprite(int screenWidth, int screenHeight) : base(screenWidth, screenHeight) { }

        public override void Update(MouseState mousestate) {
            
            if(hitbox.Intersects(new Rectangle(mousestate.X,mousestate.Y,10,10)){
                //something
            }

            base.Update();

        }
    }
}
