// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Thomas Fossati
// Created          : 06-07-2015
//
// Last Modified By : Thomas Fossati
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="MenuArrow.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//      Breakout is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
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

/// <summary>
/// The breakout namespace.
/// </summary>
namespace breakout {
    /// <summary>
    /// Class MenuArrow.
    /// Describes the behavior of the arrow in the menus to allow the user to navigate through the menus with the keyboard
    /// </summary>
    public class MenuArrow : Sprite {

        /// <summary>
        /// The current button selected
        /// </summary>
        private ButtonSprite currentButtonSelected;
        /// <summary>
        /// Gets or sets the current button selected.
        /// the set also place the arrow right under the button sprite.
        /// </summary>
        /// <value>The current button selected.</value>
        public ButtonSprite CurrentButtonSelected {
            get { return currentButtonSelected; }
            set {
                currentButtonSelected = value;
                position = new Vector2(currentButtonSelected.Position.X + currentButtonSelected.Texture.Width / 2 - texture.Width / 2, currentButtonSelected.Position.Y + currentButtonSelected.Texture.Height + 5);
            }
        }

        /// <summary>
        /// The current button selected index
        /// </summary>
        private int currentButtonSelectedIndex = 0;
        /// <summary>
        /// Gets or sets the index of the current button selected.
        /// </summary>
        /// <value>The index of the current button selected.</value>
        public int CurrentButtonSelectedIndex { get { return currentButtonSelectedIndex; } set { currentButtonSelectedIndex = value; } }
        /// <summary>
        /// The button group
        /// </summary>
        private List<ButtonSprite> buttonGroup = new List<ButtonSprite>();
        /// <summary>
        /// Gets or sets the button group.
        /// The button group stocks all the button sprites of the current menu (start menu, loose menu, win menu)
        /// </summary>
        /// <value>The button group.</value>
        public List<ButtonSprite> ButtonGroup {
            get { return buttonGroup; }
            set { buttonGroup = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuArrow"/> class.
        /// </summary>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        public MenuArrow(int screenWidth, int screenHeight) : base(screenWidth, screenHeight) { }

        /// <summary>
        /// Updates the Arrow position and behavior accord to the input keys
        /// </summary>
        /// <param name="keyboardState">State of the keyboard.</param>
        /// <param name="previousKeyboardState">State of the previous keyboard.</param>
        /// <param name="gameState">State of the game.</param>
        public void Update(KeyboardState keyboardState, KeyboardState previousKeyboardState, ref GameState gameState) {
            if (!buttonGroup.Contains(currentButtonSelected)) {
                currentButtonSelectedIndex = 0;
                CurrentButtonSelected = buttonGroup.ElementAt(currentButtonSelectedIndex);
            }

            if ((keyboardState.IsKeyUp(Keys.Right) && previousKeyboardState.IsKeyDown(Keys.Right)) || (keyboardState.IsKeyUp(Keys.Down) && previousKeyboardState.IsKeyDown(Keys.Down))) {
                if (currentButtonSelectedIndex < buttonGroup.Count - 1) {
                    currentButtonSelectedIndex++;
                } else {
                    currentButtonSelectedIndex = 0;
                }
                CurrentButtonSelected = buttonGroup.ElementAt(currentButtonSelectedIndex);

            }
            if ((keyboardState.IsKeyUp(Keys.Left) && previousKeyboardState.IsKeyDown(Keys.Left)) || (keyboardState.IsKeyUp(Keys.Up) && previousKeyboardState.IsKeyDown(Keys.Up))) {
                if (currentButtonSelectedIndex > 0) {
                    currentButtonSelectedIndex--;
                } else {
                    currentButtonSelectedIndex = buttonGroup.Count - 1;
                }
                CurrentButtonSelected = buttonGroup.ElementAt(currentButtonSelectedIndex);
            }


            if (keyboardState.IsKeyUp(Keys.Enter) && previousKeyboardState.IsKeyDown(Keys.Enter)) {
                switch (currentButtonSelected.Name) {
                    case "story":
                        gameState = GameState.READYTOSTART;
                        break;
                    case "custom":
                        gameState = GameState.CUSTOM;
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
