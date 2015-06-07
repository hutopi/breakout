// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Thomas Fossati
// Created          : 06-07-2015
//
// Last Modified By : Thomas Fossati
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="Sprite.cs" company="Hutopi">
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
    /// Class Sprite.
    /// </summary>
    public class Sprite {
        /// <summary>
        /// The screen height
        /// </summary>
        protected int screenHeight;
        /// <summary>
        /// The screen width
        /// </summary>
        protected int screenWidth;

        /// <summary>
        /// The texture
        /// </summary>
        protected Texture2D texture;
        /// <summary>
        /// Gets or sets the texture.
        /// </summary>
        /// <value>The texture.</value>
        public Texture2D Texture {
            get { return texture; }
            set { texture = value; }
        }

        /// <summary>
        /// The position
        /// </summary>
        protected Vector2 position;
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public Vector2 Position {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        public Sprite(int screenWidth, int screenHeight) {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

        }

        /// <summary>
        /// Initializes this instance and sets a default position at the top right of the screen.
        /// </summary>
        public virtual void Initialize() {
            position = Vector2.Zero;
        }

        /// <summary>
        /// Loads the content common to every sprite of our game, the texture 2D
        /// </summary>
        /// <param name="content">The content manager.</param>
        /// <param name="assetName">Name of the asset.</param>
        public virtual void LoadContent(ContentManager content, string assetName) {
            texture = content.Load<Texture2D>(assetName);
        }

        /// <summary>
        /// Handles the input.
        /// </summary>
        /// <param name="keyboardState">State of the keyboard.</param>
        /// <param name="previousKeyboardState">State of the previous keyboard.</param>
        public virtual void HandleInput(KeyboardState keyboardState, KeyboardState previousKeyboardState) {
        }

        /// <summary>
        /// Draws the sprite.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            spriteBatch.Draw(texture, position, Color.White);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public virtual void Update() { }
    }
}
