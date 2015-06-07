// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Thomas Fossati
// Created          : 06-07-2015
//
// Last Modified By : Thomas Fossati
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="ButtonSprite.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//      Breakout is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// The breakout namespace.
/// </summary>
namespace breakout.Util {
    /// <summary>
    /// Class ButtonSprite.
    /// Describes the behavior of the button in the menus to allow the user to interact with them.
    /// </summary>
    public class ButtonSprite : Sprite {

        /// <summary>
        /// The name
        /// </summary>
        private String name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public String Name { get { return name; } set { name = value; } }

        /// <summary>
        /// Gets the hitbox.
        /// </summary>
        /// <value>The hitbox.</value>
        public Rectangle Hitbox {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonSprite"/> class.
        /// </summary>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="name">The name.</param>
        public ButtonSprite(int screenWidth, int screenHeight, string name) : base(screenWidth, screenHeight) {
            this.name = name;
        }

        /// <summary>
        /// Updates the gameState
        /// If the user mouse is on the button sprite, depending of the button sprite the gameState is updated.
        /// </summary>
        /// <param name="mousestate">The mousestate.</param>
        /// <param name="previousMouseState">State of the previous mouse.</param>
        /// <param name="gameState">State of the game.</param>
        public void Update(MouseState mouseState, MouseState previousMouseState, ref GameState gameState) {
            
            if(Hitbox.Intersects(new Rectangle(mouseState.X,mouseState.Y,10,10))){
                if (previousMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released) {
                    
                    switch (name) {
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

            base.Update();

        }
    }
}
