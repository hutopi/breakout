﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace breakout {
    class Ball : MovingSprite {

        public Circle hitbox {
            //the Postions are taken from the top left of the circle and not the center so you have to to move the postion with half the texture of the sprite to a have a circle hitbox that fits correclty the circle
            get { return new Circle((int)Position.X + Texture.Width / 2, (int)Position.Y + Texture.Height / 2, (double)Texture.Width / 2); }
        }


        public Ball(int screenWidth, int screenHeight) : base(screenWidth, screenHeight) { }
        public override void Initialize() {

            base.Initialize();


            Direction = new Vector2(0, -1);

            Speed = 0.3f;

        }


        public void LoadContent(ContentManager content, String assetName, Bat bat) {

            base.LoadContent(content, assetName);
            Position = new Vector2(bat.Position.X + bat.Texture.Width / 2 - Texture.Width / 2, bat.Position.Y - bat.Texture.Height - Texture.Height / 2);
            //Position = new Vector2(100, 100);
        }

        public void Update(GameTime gameTime, Rectangle batHitBox, GameLevel gameLevel, bool isGameStarted) {

            if (isGameStarted) {
                //bouncing on the walls
                if ((Position.Y <= 0 && Direction.Y < 0)) {

                    Direction = new Vector2(Direction.X, -Direction.Y);

                }
                if (Position.X <= 0 && Direction.X < 0 || Position.X > screenWidth - Texture.Height && Direction.X > 0) {
                    Direction = new Vector2(-Direction.X, Direction.Y);
                }

                //bouncing on the bat
                //Pour cette part je commente en FR pour le moment parce que c'est compliqué
                // grosso le merdo le but est de faire rebondir la balle de manière différente pour chaque coté de la raquette (gauche, droite ou au mileu)
                // puis grace aux cours de trigo, je donne une direction à la balle plus moins verticale en fonction de son rapprochement avec le centre de la raquette
                // du coup si vous tapez pile au milieu, la balle repart verticalement, ensuite plus vous tapez en vous éloignant du milieu plus la composante horizontale X du vecteur de la direction sera importante
                // c'est pas parfait mais ça permet de faire plus de mouvements que de simplement inverser la direction Y et donc de ne pas subir les mouvements de la balles dues au rebons sur les autres composants du jeu.
                if ((Direction.Y > 0 && this.hitbox.IntersectsRec(batHitBox))) {
                    if (this.hitbox.X <= batHitBox.X + batHitBox.Width && this.hitbox.X > batHitBox.X + batHitBox.Width / 2) {

                        double gapBetweenBatAndBall = batHitBox.X + batHitBox.Width - this.hitbox.X; //détermine l'écart X de la balle par rapport à l'extrémité droite de la raquette
                        double theta = (gapBetweenBatAndBall * (Math.PI / 2)) / (batHitBox.Width / 2); // produit en croix pour une valeur théta en radian de l'angle que l'on souhaite obtenir en fonction de l'écart gapBetweenBatAndBall
                        if (theta <= 0.2) {
                            Direction = new Vector2((float)Math.Cos(theta), -(float)Math.Sin(Math.PI / 6));

                        } else {
                            Direction = new Vector2((float)Math.Cos(theta), -(float)Math.Sin(theta));
                        }

                        Console.WriteLine(Direction);
                    }
                    if (this.hitbox.X < batHitBox.X + batHitBox.Width / 2 && this.hitbox.X >= batHitBox.X) {

                        double gapBetweenBatAndBall = this.hitbox.X - batHitBox.X;
                        double theta = (gapBetweenBatAndBall * (Math.PI / 2)) / (batHitBox.Width / 2);
                        if (theta <= 0.2) {
                            Direction = new Vector2(-(float)Math.Cos(theta), -(float)Math.Sin(Math.PI / 6));
                        } else {
                            Direction = new Vector2(-(float)Math.Cos(theta), -(float)Math.Sin(theta));
                        }



                        Console.WriteLine(Direction);
                    }
                    if (this.hitbox.X == batHitBox.X + batHitBox.Width / 2) {
                        Direction = new Vector2(0, -1);
                        Console.WriteLine(Direction);
                    }



                }

                // boucing on the bricks
                /*Thread thread = new Thread(() => bouncingOnTheBricks(gameLevel));
                thread.Start();
                thread.I*/
                bouncingOnTheBricks(gameLevel);
                base.Update(gameTime);

            } else {
                Position = new Vector2(batHitBox.X + batHitBox.Width / 2 - this.Texture.Width/2, batHitBox.Y - this.Texture.Height);
                
            }
        }

        public void bouncingOnTheBricks(GameLevel gameLevel) {
            foreach (Brick b in gameLevel.BricksMap) {
                if (this.hitbox.IntersectsRec(b.hitbox) && b.resistance > 0) {

                    int scoreIncrement = b.Touched(gameLevel.brickTexture);
                    gameLevel.Score += scoreIncrement;
                    if (scoreIncrement == 100) {
                        gameLevel.Nb_bricks--;
                    }
                    Rectangle topRectangle = new Rectangle(b.hitbox.X, b.hitbox.Top, b.hitbox.Width, 0);
                    Rectangle bottomRectangle = new Rectangle(b.hitbox.X, b.hitbox.Bottom, b.hitbox.Width, 0);
                    Rectangle leftRectangle = new Rectangle(b.hitbox.Left, b.hitbox.Y, 0, b.hitbox.Height);
                    Rectangle rightRectangle = new Rectangle(b.hitbox.Right, b.hitbox.Y, 0, b.hitbox.Height);

                    if (hitbox.IntersectsRec(topRectangle) || hitbox.IntersectsRec(bottomRectangle)) {

                        Speed = 0f;
                        Direction = new Vector2(Direction.X, -Direction.Y);
                        Speed = 0.3f;

                    } else if (hitbox.IntersectsRec(leftRectangle) || hitbox.IntersectsRec(rightRectangle)) {
                        Speed = 0f;
                        Direction = new Vector2(-Direction.X, Direction.Y);
                        Speed = 0.3f;

                    }


                    /*double wy = (hitbox.Radius * 2 + b.hitbox.Width) * (hitbox.Y - b.hitbox.Y + b.hitbox.Height / 2);
                    double hx = (hitbox.Radius * 2 + b.hitbox.Height) * (hitbox.X - b.hitbox.X + b.hitbox.Width / 2);

                    //somme de Minkowski
                    if (wy > hx) {
                        if (wy > -hx) {
                            //top 
                            Direction = new Vector2(Direction.X, -Direction.Y);
                            Console.WriteLine("top");
                        } else {
                            //left 
                            Direction = new Vector2(-Direction.X, Direction.Y);
                            Console.WriteLine("left");
                        }
                    } else {
                        if (wy > -hx) {
                            //right
                            Direction = new Vector2(-Direction.X, Direction.Y);
                            Console.WriteLine("right");
                        } else {
                            //bottom 
                            Direction = new Vector2(Direction.X, -Direction.Y);
                            Console.WriteLine("down");
                        }

                    }
                    */


                }
            }
        }
    }
}
