// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Pierre Defache
// Created          : 06-07-2015
//
// Last Modified By : Pierre Defache
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="Bonus.cs" company="Hutopi">
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

/// <summary>
/// The breakout namespace.
/// </summary>
namespace breakout
{
    /// <summary>
    /// Class Bonus.
    /// </summary>
    public class Bonus : MovingSprite
    {
        /// <summary>
        /// The name
        /// </summary>
        private string name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The type
        /// </summary>
        private BonusType type;
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public BonusType Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// The activated
        /// </summary>
        private bool activated = false;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Bonus"/> is activated.
        /// </summary>
        /// <value><c>true</c> if activated; otherwise, <c>false</c>.</value>
        public bool Activated
        {
            get { return activated; }
            set { activated = value; }
        }

        /// <summary>
        /// The sm
        /// </summary>
        private SoundManager sm = new SoundManager();

        /// <summary>
        /// Gets the hitbox.
        /// </summary>
        /// <value>The hitbox.</value>
        public Rectangle Hitbox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite" /> class.
        /// </summary>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        public Bonus(int screenWidth, int screenHeight) : base(screenWidth, screenHeight) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bonus"/> class.
        /// </summary>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="type">The type.</param>
        public Bonus(int screenWidth, int screenHeight, BonusType type)
            : base(screenWidth, screenHeight)
        {
            this.Type = type;
            Speed = 0.2f;
        }

        /// <summary>
        /// Loads the content common to every sprite of our game, the texture 2D
        /// </summary>
        /// <param name="content">The content manager.</param>
        /// <param name="assetName">Name of the asset.</param>
        public override void LoadContent(ContentManager content, String assetName)
        {
            sm.LoadContent(content);
            base.LoadContent(content, assetName);
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="batHitBox">The bat hit box.</param>
        /// <param name="gameLevel">The game level.</param>
        /// <param name="brick">The brick.</param>
        public void Update(GameTime gameTime, Rectangle batHitBox, GameLevel gameLevel, Brick brick)
        {
            Direction = new Vector2(0, 1);
            if (this.Hitbox.Intersects(batHitBox))
            {
                switch (this.Type)
                {
                    case BonusType.HIGH_SPEED:
                        this.sm.powerDown.Play();
                        foreach (Ball b in gameLevel.Balls)
                        {
                            b.Speed *= 1.5f;
                        }
                        break;
                    case BonusType.HIGH_RESISTANCE:
                        this.sm.powerDown.Play();
                        foreach (Brick b in gameLevel.BricksMap)
                        {
                            if (b.Resistance != 0 && b.Resistance != 4)
                            {
                                b.Resistance = 3;
                                b.UpdateTexture(gameLevel.BrickTexture);
                            }
                        }
                        break;
                    case BonusType.BAT_REDUCED:
                        this.sm.powerDown.Play();
                        gameLevel.Bat.Texture = gameLevel.BatTexture.Reduced;
                        break;
                    case BonusType.BAT_EXTENDED:
                        this.sm.powerUp.Play();
                        gameLevel.Bat.Texture = gameLevel.BatTexture.Extended;
                        break;
                    case BonusType.DOWN_LIFE:
                        this.sm.powerDown.Play();
                        if (gameLevel.Lives > 0)
                        {
                            gameLevel.Lives--;
                        }
                        break;
                    case BonusType.LOW_RESISTANCE:
                        this.sm.powerUp.Play();
                        foreach (Brick b in gameLevel.BricksMap)
                        {
                            if (b.Resistance != 0 && b.Resistance != 4)
                            {
                                b.Resistance = 1;
                                b.UpdateTexture(gameLevel.BrickTexture);
                            }
                        }
                        break;
                    case BonusType.LOW_SPEED:
                        this.sm.powerUp.Play();
                        foreach (Ball b in gameLevel.Balls)
                        {
                            b.Speed /= 1.5f;
                        }
                        break;
                    case BonusType.UP_LIFE:
                        this.sm.powerUp.Play();
                        if (gameLevel.Lives < 4)
                        {
                            gameLevel.Lives++;
                        }
                        break;
                    case BonusType.MULTIPLICATE_BALL:
                        Ball newBall = null;
                        foreach (Ball b in gameLevel.Balls)
                        {
                            newBall = new Ball(b);
                        }
                        gameLevel.Balls.Add(newBall);
                        break;
                }

                Type = BonusType.NONE;
                Activated = false;
            } else if(Position.Y >= screenHeight/0.6){
                Activated = false;
            }

            base.Update(gameTime);
        }
    }
}
