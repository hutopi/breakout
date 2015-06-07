﻿// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Thomas Fossati
// Created          : 06-07-2015
//
// Last Modified By : Thomas Fossati
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="Bat.cs" company="Hutopi">
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
    /// Class Bat.
    /// Describes the bat behavior during the game.
    /// </summary>
    public class Bat : MovingSprite {

        /// <summary>
        /// The velocity
        /// </summary>
        private Vector2 velocity = Vector2.Zero;
        /// <summary>
        /// The acceleration
        /// </summary>
        private Vector2 acceleration = Vector2.Zero;
        /// <summary>
        /// Gets or sets the acceleration.
        /// </summary>
        /// <value>The acceleration.</value>
        public Vector2 Acceleration { get { return acceleration; } set { acceleration = value; } }
        /// <summary>
        /// Gets the hitbox.
        /// </summary>
        /// <value>The hitbox.</value>
        public Rectangle Hitbox {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite" /> class.
        /// </summary>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        public Bat(int screenWidth, int screenHeight) : base(screenWidth, screenHeight) { }

        /// <summary>
        /// Initializes this instance and sets a default position at the top right of the screen and a zero direction/speed.
        /// </summary>
        public override void Initialize() {
            base.Initialize();
        }

        /// <summary>
        /// Loads the content common to every sprite of our game, the texture 2D
        /// </summary>
        /// <param name="content">The content manager.</param>
        /// <param name="assetName">Name of the asset.</param>
        public override void LoadContent(ContentManager content, string assetName) {
            base.LoadContent(content, assetName);
            Position = new Vector2(screenWidth / 2 - Texture.Width / 2, screenHeight - 10 - Texture.Height / 2);
        }

        /// <summary>
        /// Handles the input.
        /// Sets the speed, the direction, the velocity and the acceleration of the bat according to the user's input (left or right).
        /// </summary>
        /// <param name="keyboardState">State of the keyboard.</param>
        /// <param name="previousKeyboardState">State of the previous keyboard.</param>
        public override void HandleInput(KeyboardState keyboardState, KeyboardState previousKeyboardState) {

            if (keyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left)) {
                direction = -Vector2.UnitX;
                speed = 0.2f;
                acceleration = Vector2.Zero;
                velocity = new Vector2(-speed, 0);
            } else if (keyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right)) {
                direction = Vector2.UnitX;
                speed = 0.2f;
                acceleration = Vector2.Zero;
                velocity = new Vector2(+speed, 0);
            } else if (keyboardState.IsKeyDown(Keys.Left) && previousKeyboardState.IsKeyDown(Keys.Left)) {
                direction = -Vector2.UnitX;
                speed = 0.2f;
                acceleration = -(new Vector2(2f, 0)); 
                
            } else if (keyboardState.IsKeyDown(Keys.Right) && previousKeyboardState.IsKeyDown(Keys.Right)) {
                direction = Vector2.UnitX;
                speed = 0.2f;
                acceleration = new Vector2(2f, 0); 
            } else {
                speed = 0;
                acceleration = Vector2.Zero;
            }


        }

        /// <summary>
        /// Updates the position of the moving sprite with velocity and acceleration if needed.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime) {
            if ((position.X <= 0  && direction.X < 0) || (position.X >= screenWidth - Texture.Width  && direction.X > 0)) { 
                speed = 0;
                acceleration = Vector2.Zero;
            }
            if (acceleration == Vector2.Zero) {
                base.Update(gameTime);

            }else{
                velocity = velocity + (acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds);
                this.position = position + ( velocity *(float)gameTime.ElapsedGameTime.TotalMilliseconds);

            }
        }

    }

}
