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
    public class MenuArrow : Sprite {

        private LinkedList<ButtonSprite> buttonSprites;
        /*public <ButtonSprite> ButtonSprites {
            get { return buttonSprites; }
            set { buttonSprites = value;
                ButtonSprite firstButton = (ButtonSprite)buttonSprites.First;
                Position = new Vector2(buttonSprites.First., 10) ; }
        }*/

        public MenuArrow(int screenWidth, int screenHeight) : base(screenWidth, screenHeight) { }

        public override void HandleInput(KeyboardState keyboardState, MouseState mouseState) {

            
            base.HandleInput(keyboardState,mouseState);
        }

    }
}
