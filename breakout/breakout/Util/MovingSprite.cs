// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Thomas Fossati
// Created          : 06-07-2015
//
// Last Modified By : Thomas Fossati
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="MovingSprite.cs" company="Hutopi">
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
    /// Class MovingSprite.
    /// Inherits of class sprite.
    /// </summary>
    public class MovingSprite : Sprite {

        /// <summary>
        /// The direction
        /// </summary>
        protected Vector2 direction;
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>The direction.</value>
        public Vector2 Direction {
            get { return direction; }
            set { direction = Vector2.Normalize(value); }
        }

        /// <summary>
        /// The speed
        /// </summary>
        protected float speed;
        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>The speed.</value>
        public float Speed {

            get { return speed; }

            set { speed = value; }

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite" /> class.
        /// </summary>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        public MovingSprite(int screenWidth, int screenHeight) : base(screenWidth, screenHeight) { }

        /// <summary>
        /// Updates the position of the moving sprite
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Update(GameTime gameTime) {
            this.position += direction * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        /// <summary>
        /// Initializes this instance and sets a default position at the top right of the screen and a zero direction/speed.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            direction = Vector2.Zero;
            speed = 0;
        }

    }
}
