using System;
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
        /*public Rectangle hitbox {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }

        }*/


        public Ball(int screenWidth, int screenHeight) : base(screenWidth, screenHeight) { }
        public override void Initialize() {

            base.Initialize();


            Direction = new Vector2(0, -1);

            Speed = 0.3f;

        }


        public void LoadContent(ContentManager content, String assetName, Bat bat) {

            base.LoadContent(content, assetName);
            Position = new Vector2(bat.Position.X + bat.Texture.Width / 2 - Texture.Width / 2, bat.Position.Y - bat.Texture.Height - Texture.Height / 2);
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
                Position = new Vector2(batHitBox.X + batHitBox.Width / 2 - this.Texture.Width / 2, batHitBox.Y - this.Texture.Height);

            }
        }

        public void bouncingOnTheBricks(GameLevel gameLevel) {
            Brick lastBrick = null;
            foreach (Brick b in gameLevel.BricksMap) {
                if (this.hitbox.IntersectsRec(b.hitbox) && b.resistance > 0) {

                    Rectangle[] sideRectangles = buildSideRectangles(b);
                    int scoreIncrement = b.Touched(gameLevel.brickTexture);
                    gameLevel.Score += scoreIncrement;
                    if (scoreIncrement == 100) {
                        gameLevel.Nb_bricks--;
                    }


                    if (lastBrick != null) {
                        Rectangle[] sideRectanglesLast = buildSideRectangles(lastBrick);

                        if ((hitbox.IntersectsRec(sideRectangles[0]) && hitbox.IntersectsRec(sideRectangles[3])) && (hitbox.IntersectsRec(sideRectanglesLast[0]) && hitbox.IntersectsRec(sideRectanglesLast[2]))) {
                            Direction = new Vector2(Direction.X, -Direction.Y);
                            Console.WriteLine("coin haut droit A + coin haut gauche B :" + Direction);
                            break;
                        } else if ((hitbox.IntersectsRec(sideRectangles[1]) && hitbox.IntersectsRec(sideRectangles[3])) && (hitbox.IntersectsRec(sideRectanglesLast[1]) && hitbox.IntersectsRec(sideRectanglesLast[2]))) {
                            Direction = new Vector2(Direction.X, -Direction.Y);
                            Console.WriteLine("coin bas droit A + coin bas gauche B :" + Direction);
                            break;
                        } else if ((hitbox.IntersectsRec(sideRectangles[0]) && hitbox.IntersectsRec(sideRectangles[3])) && (hitbox.IntersectsRec(sideRectanglesLast[1]) && hitbox.IntersectsRec(sideRectanglesLast[3]))) {
                            Direction = new Vector2(-Direction.X, -Direction.Y);
                            Console.WriteLine("coin haut droit A + coin bas droit B :" + Direction);
                            break;
                        } else if ((hitbox.IntersectsRec(sideRectangles[0]) && hitbox.IntersectsRec(sideRectangles[2])) && (hitbox.IntersectsRec(sideRectanglesLast[1]) && hitbox.IntersectsRec(sideRectanglesLast[3]))) {
                            Direction = new Vector2(-Direction.X, Direction.Y);
                            Console.WriteLine("coin haut gauche A + coin bas gauche B :" + Direction);
                            break;
                        }
                    }

                    if (singleCornerHit(sideRectangles)) {
                        Direction = new Vector2(-Direction.X, -Direction.Y);
                        Console.WriteLine("un coin " + Direction);
                        break;
                    } else if (hitbox.IntersectsRec(sideRectangles[0]) || hitbox.IntersectsRec(sideRectangles[1])) {

                        Direction = new Vector2(Direction.X, -Direction.Y);
                        Console.WriteLine("top || bottom : " + Direction);
                        break;
                    } else if (hitbox.IntersectsRec(sideRectangles[2]) || hitbox.IntersectsRec(sideRectangles[3])) {
                        Direction = new Vector2(-Direction.X, Direction.Y);
                        Console.WriteLine("right || left : " + Direction);
                        break;



                    }
                }
                
                lastBrick = b;


            }

        }

        public Rectangle[] buildSideRectangles(Brick b) {
            Rectangle[] sideRectangles = new Rectangle[4];
            sideRectangles[0] = new Rectangle(b.hitbox.X, b.hitbox.Top, b.hitbox.Width, 0); //TOP
            sideRectangles[1] = new Rectangle(b.hitbox.X, b.hitbox.Bottom, b.hitbox.Width, 0); //Bottom
            sideRectangles[2] = new Rectangle(b.hitbox.Left, b.hitbox.Y, 0, b.hitbox.Height); //Left
            sideRectangles[3] = new Rectangle(b.hitbox.Right, b.hitbox.Y, 0, b.hitbox.Height); //Right


            return sideRectangles;
        }

        public bool singleCornerHit(Rectangle[] sideRectangles) {
            return ((hitbox.IntersectsRec(sideRectangles[0]) && hitbox.IntersectsRec(sideRectangles[3])) || (hitbox.IntersectsRec(sideRectangles[0]) && hitbox.IntersectsRec(sideRectangles[2])) || (hitbox.IntersectsRec(sideRectangles[1]) && hitbox.IntersectsRec(sideRectangles[3])) || (hitbox.IntersectsRec(sideRectangles[1]) && hitbox.IntersectsRec(sideRectangles[2])));
        }

    }
}
