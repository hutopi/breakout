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

        private ButtonSprite currentButtonSelected;
        public ButtonSprite CurrentButtonSelected {
            get { return currentButtonSelected; }
            set {  currentButtonSelected = value;
                position = new Vector2(currentButtonSelected.Position.X + currentButtonSelected.Texture.Width / 2 - texture.Width / 2, currentButtonSelected.Position.Y + currentButtonSelected.Texture.Height + 5);
            }
        }

        private int currentButtonSelectedIndex = 0;
        public int CurrentButtonSelectedIndex { get { return currentButtonSelectedIndex; } set { currentButtonSelectedIndex = value; } }
        private List<ButtonSprite> buttonGroup = new List<ButtonSprite>();
        public List<ButtonSprite> ButtonGroup {
            get { return buttonGroup; }
            set { buttonGroup = value; } }

        public MenuArrow(int screenWidth, int screenHeight) : base(screenWidth, screenHeight) { }

        public void Update(KeyboardState keyboardState, KeyboardState previousKeyboardState, ref GameState gameState) {
            if (keyboardState.IsKeyUp(Keys.Right) && previousKeyboardState.IsKeyDown(Keys.Right)) {
                if (currentButtonSelectedIndex < buttonGroup.Count-1) {
                    currentButtonSelectedIndex++;
                }else{
                    currentButtonSelectedIndex = 0;                
                }
                CurrentButtonSelected = buttonGroup.ElementAt(currentButtonSelectedIndex);

                Console.WriteLine(currentButtonSelectedIndex);
                Console.WriteLine(position);
            }
            if (keyboardState.IsKeyUp(Keys.Left) && previousKeyboardState.IsKeyDown(Keys.Left)) {
                if (currentButtonSelectedIndex > 0) {
                    currentButtonSelectedIndex--;
                } else {
                    currentButtonSelectedIndex = buttonGroup.Count - 1;
                }
                CurrentButtonSelected = buttonGroup.ElementAt(currentButtonSelectedIndex);

                Console.WriteLine(currentButtonSelectedIndex);

            }

            
            if (keyboardState.IsKeyUp(Keys.Enter) && previousKeyboardState.IsKeyDown(Keys.Enter)) {
                switch (currentButtonSelected.Name) {
                    case "start":
                        gameState = GameState.READYTOSTART;
                        break;
                    case "ex":
                        gameState = GameState.RESTART;
                        break;
                    case "exit":
                        gameState = GameState.EXIT;
                        break;
                    case "resume":
                        gameState = GameState.PLAYING;
                        break;
                    case "restart":
                        gameState = GameState.RESTART;
                        break;
                    case "next":
                        gameState = GameState.NEXT_LEVEL;
                        break;
                    default:
                        break;
                }

            }

        }

    }
}
