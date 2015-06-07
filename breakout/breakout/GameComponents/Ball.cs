// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Thomas Fossati
// Created          : 06-07-2015
//
// Last Modified By : Thomas Fossati
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="Ball.cs" company="Hutopi">
//     Copyright © 2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//      Breakout is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using breakout.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

/// <summary>
/// The breakout namespace.
/// </summary>
namespace breakout.GameComponents {
    /// <summary>
    /// Class Ball.
    /// Describe the behavior of the ball during the game.
    /// </summary>
    public class Ball : MovingSprite {

        /// <summary>
        /// Gets the hitbox.
        ///The positions are taken from the top left of the circle and not the center so you have to to move the position with half the texture of the sprite to a have a circle hitbox that fits correctly the circle
        /// </summary>
        /// <value>The hitbox.</value>
        private Circle hitbox {
            get { return new Circle((int)Position.X + Texture.Width / 2, (int)Position.Y + Texture.Height / 2, (double)Texture.Width / 2); }
        }
        /// <summary>
        /// The sm
        /// </summary>
        private SoundManager sm = new SoundManager();

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite" /> class.
        /// </summary>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        public Ball(int screenWidth, int screenHeight) : base(screenWidth, screenHeight) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ball"/> class.
        /// </summary>
        /// <param name="ball">The ball.</param>
        public Ball(Ball ball)
            : base(ball.screenWidth, ball.screenHeight) {
            this.Position = ball.Position;
            this.Texture = ball.Texture;
            this.Direction = -ball.Direction;
            this.Speed = ball.Speed;
            this.sm = ball.sm;
        }

        /// <summary>
        /// Initializes this instance and sets a default position at the top right of the screen and a zero direction/speed.
        /// </summary>
        public override void Initialize() {

            base.Initialize();


            Direction = new Vector2(0, -1);

            Speed = 0.3f;

        }


        /// <summary>
        /// Loads the content.
        /// Sets the position of the ball on the top the bat.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="assetName">Name of the asset.</param>
        /// <param name="bat">The bat.</param>
        public void LoadContent(ContentManager content, String assetName, Bat bat) {

            base.LoadContent(content, assetName);
            Position = new Vector2(bat.Position.X + bat.Texture.Width / 2 - Texture.Width / 2, bat.Position.Y - bat.Texture.Height - Texture.Height / 2);
            sm.LoadContent(content);
        }

        /// <summary>
        /// Updates the position, the speed and the direction of the ball.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="batHitBox">The bat hit box.</param>
        /// <param name="gameLevel">The game level.</param>
        /// <param name="isGameStarted">if set to <c>true</c> [is game started].</param>
        public void Update(GameTime gameTime, Rectangle batHitBox, GameLevel gameLevel, bool isGameStarted) {

            if (isGameStarted) {
                bouncingOnTheWalls(gameLevel);
                bouncingOnTheBat(batHitBox);
                bouncingOnTheBricks(gameLevel);

            } else {
                Position = new Vector2(batHitBox.X + batHitBox.Width / 2 - this.Texture.Width / 2, batHitBox.Y - this.Texture.Height / 2);

            }

            base.Update(gameTime);

        }
        /// <summary>
        /// Changes the direction of the ball if it bounces on a wall.
        /// </summary>
        /// <param name="gameLevel">The game level.</param>
        private void bouncingOnTheWalls(GameLevel gameLevel) {
            if ((Position.Y <= 0 && Direction.Y < 0)) {
                this.sm.bump.Play(0.5f, 0.0f, 0.0f);
                Direction = new Vector2(Direction.X, -Direction.Y);
                gameLevel.Score -= 10;
            }
            if (Position.X <= 0 && Direction.X < 0 || Position.X > screenWidth - Texture.Height && Direction.X > 0) {
                this.sm.bump.Play(0.5f, 0.0f, 0.0f);
                Direction = new Vector2(-Direction.X, Direction.Y);
                gameLevel.Score -= 10;
            }
        }
        /// <summary>
        /// Changes the direction of the ball if it bounces on the bat.
        /// </summary>
        /// <param name="batHitBox">The bat hitbox.</param>
        private void bouncingOnTheBat(Rectangle batHitBox) {
            if ((Direction.Y > 0 && this.hitbox.IntersectsRec(batHitBox))) {
                Direction = new Vector2(((float)hitbox.X - batHitBox.Center.X) / (batHitBox.Width / 2), -Direction.Y);
                Direction = Vector2.Normalize(Direction);
                this.sm.bump.Play(0.5f, 0.0f, 0.0f);
            }
        }

        /// <summary>
        /// Gets the touched bricks by the ball.
        /// </summary>
        /// <param name="gameLevel">The game level.</param>
        /// <returns>List&lt;Brick&gt;.</returns>
        private List<Brick> getTouchedBricks(GameLevel gameLevel) {
            List<Brick> touchedBricks = new List<Brick>();
            foreach (Brick b in gameLevel.BricksMap) {
                if (this.hitbox.IntersectsRec(b.Hitbox) && b.Resistance > 0) {
                    if (b.Resistance == 4) {
                        this.sm.bump.Play(0.5f, 0.0f, 0.0f);
                    } else {
                        this.sm.bumpBrick.Play(0.5f, 0.0f, 0.0f);
                    }

                    if (b.Bonus.Type != BonusType.NONE && b.Bonus.Activated == false) {
                        b.Bonus.Activated = true;
                    }

                    int scoreIncrement = b.Touched(gameLevel.BrickTexture);
                    gameLevel.Score += scoreIncrement;
                    if (scoreIncrement == 100) {
                        gameLevel.Nb_bricks--;
                    }
                    touchedBricks.Add(b);
                }
            }
            return touchedBricks;
        }




        /// <summary>
        /// Changes the direction of the ball if it bounces on a brick.
        /// </summary>
        /// <param name="gameLevel">The game level.</param>
        private void bouncingOnTheBricks(GameLevel gameLevel) {
            List<Brick> touchedBricks = getTouchedBricks(gameLevel);


            foreach (Brick b in touchedBricks) {

                Rectangle topBall = new Rectangle(hitbox.X, (int)(hitbox.Y - hitbox.Radius), 2, 1);
                Rectangle bottomBall = new Rectangle(hitbox.X, (int)(hitbox.Y + hitbox.Radius), 2, 1);
                Rectangle leftBall = new Rectangle((int)(hitbox.X - hitbox.Radius), hitbox.Y, 1, 2);
                Rectangle rightBall = new Rectangle((int)(hitbox.X + hitbox.Radius), hitbox.Y, 1, 2);
                Vector2 centerVector = new Vector2(b.Hitbox.Center.X - this.position.X, b.Hitbox.Center.Y - this.position.X);

                centerVector = Vector2.Normalize(centerVector);
                if (bottomBall.Intersects(b.Hitbox)) {
                    Direction = new Vector2(Direction.X, -Math.Abs(Direction.Y));
                } else if (leftBall.Intersects(b.Hitbox)) {
                    Direction = new Vector2(Math.Abs(Direction.X), Direction.Y);
                } else if (topBall.Intersects(b.Hitbox)) {
                    Direction = new Vector2(Direction.X, Math.Abs(Direction.Y));
                } else if (rightBall.Intersects(b.Hitbox)) {
                    Direction = new Vector2(-Math.Abs(Direction.X), Direction.Y);
                } else if (centerVector.X > 0 && centerVector.Y > 0) {
                    Direction = new Vector2(-Math.Abs(Direction.X), -Math.Abs(Direction.Y));
                } else if (centerVector.X < 0 && centerVector.Y > 0) {
                    Direction = new Vector2(Math.Abs(Direction.X), -Math.Abs(Direction.Y));
                } else if (centerVector.X < 0 && centerVector.Y < 0) {
                    Direction = new Vector2(Math.Abs(Direction.X), Math.Abs(Direction.Y));
                } else if (centerVector.X > 0 && centerVector.Y < 0) {
                    Direction = new Vector2(-Math.Abs(Direction.X), Math.Abs(Direction.Y));
                }
                break;
            }
        }

    }
}
