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

        private String name;
        private String Name {
            get { return name; }
            set { name = value; }
        }
        public Rectangle hitbox {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        public ButtonSprite(int screenWidth, int screenHeight, string name) : base(screenWidth, screenHeight) {
            this.name = name;
        }

        public void Update(MouseState mousestate, MouseState previousMouseState, ref GameState gameState) {
            
            if(hitbox.Intersects(new Rectangle(mousestate.X,mousestate.Y,10,10))){
                if (previousMouseState.LeftButton == ButtonState.Pressed && mousestate.LeftButton == ButtonState.Released) {
                    
                    switch (name) {
                        case "start":
                            gameState = GameState.PLAYING;
                            break;
                        case "exit":
                            gameState = GameState.EXIT;
                            break;
                        case "resume":
                            gameState = GameState.PLAYING;
                            break;
                        default:
                            break;
                    }

                    
                }
            }

            base.Update();

        }
    }
}
