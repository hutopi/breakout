// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Pierre Defache
// Created          : 06-07-2015
//
// Last Modified By : Pierre Defache
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="Brick.cs" company="Hutopi">
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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The breakout namespace.
/// </summary>
namespace breakout
{
    /// <summary>
    /// Class Brick.
    /// </summary>
    public class Brick : Sprite
    {
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height { get; set; }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the bonus.
        /// </summary>
        /// <value>The bonus.</value>
        public Bonus Bonus { get; set; }

        /// <summary>
        /// Gets or sets the resistance.
        /// </summary>
        /// <value>The resistance.</value>
        public int Resistance { get; set; }

        /// <summary>
        /// Gets the hitbox.
        /// </summary>
        /// <value>The hitbox.</value>
        public Rectangle Hitbox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Brick"/> class.
        /// </summary>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="position">The position.</param>
        /// <param name="h">The h.</param>
        /// <param name="w">The w.</param>
        /// <param name="r">The r.</param>
        /// <param name="b">The b.</param>
        public Brick(int screenWidth, int screenHeight, Vector2 position, int h, int w, int r = 1, BonusType b = BonusType.NONE) : base( screenWidth, screenHeight)
        {
            this.Height = h;
            this.Width = w;
            this.Position = position;
            this.Resistance = r;
            this.Bonus = new Bonus(screenWidth, screenHeight, b);
        }

        /// <summary>
        /// Initializes this instance and sets a default position at the top right of the screen.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Draws the sprite.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }

        /// <summary>
        /// Loads the content common to every sprite of our game, the texture 2D
        /// </summary>
        /// <param name="content">The content manager.</param>
        /// <param name="assetName">Name of the asset.</param>
        public override void LoadContent(ContentManager content, string assetName)
        {
            assetName += this.Resistance;
            base.LoadContent(content, assetName);
        }

        /// <summary>
        /// Toucheds the specified textures.
        /// </summary>
        /// <param name="textures">The textures.</param>
        /// <returns>System.Int32.</returns>
        public int Touched(BrickTexture textures)
        {
            if (Resistance >= 4)
            {
                return 0;
            }
            else
            {
                Resistance--;
                return this.UpdateTexture(textures);
            }
        }

        /// <summary>
        /// Updates the texture.
        /// </summary>
        /// <param name="textures">The textures.</param>
        /// <returns>System.Int32.</returns>
        public int UpdateTexture(BrickTexture textures)
        {
            int newScore = 0;

            switch (Resistance)
            {
                case 0:
                    this.Texture = textures.Zero;
                    newScore = 100;
                    break;
                case 1:
                    this.Texture = textures.One;
                    newScore = 50;
                    break;
                case 2:
                    this.Texture = textures.Two;
                    newScore = 25;
                    break;
                case 3:
                    this.Texture = textures.Three;
                    newScore = 10;
                    break;
            }
            return newScore;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public override void Update()
        {
            base.Update();
        }
    }
}
