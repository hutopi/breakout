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
    public class Ball : MovingSprite {

        public Circle Hitbox {
            //the Postions are taken from the top left of the circle and not the center so you have to to move the postion with half the texture of the sprite to a have a circle hitbox that fits correclty the circle
            get { return new Circle((int)Position.X + Texture.Width / 2, (int)Position.Y + Texture.Height / 2, (double)Texture.Width / 2); }
        }
        private Vector2 lastPosition;
        private Brick lastBrick = new Brick(-1, -1, new Vector2(-1, -1), -1, -1);
        private SoundManager sm = new SoundManager();




        public Ball(int screenWidth, int screenHeight) : base(screenWidth, screenHeight) { }
        public override void Initialize() {

            base.Initialize();


            Direction = new Vector2(0, -1);

            Speed = 0.3f;

        }


        public void LoadContent(ContentManager content, String assetName, Bat bat) {

            base.LoadContent(content, assetName);
            Position = new Vector2(bat.Position.X + bat.Texture.Width / 2 - Texture.Width / 2, bat.Position.Y - bat.Texture.Height - Texture.Height / 2);
            sm.LoadContent(content);
            lastPosition = Position;
        }

        public void Update(GameTime gameTime, Rectangle batHitBox, GameLevel gameLevel, bool isGameStarted) {

            if (isGameStarted) {
                //bouncing on the walls
                bouncingOnTheWalls();
                //bouncing on the bat
                bouncingOnTheBat(batHitBox);
                // boucing on the bricks
                bouncingOnTheBricks(gameLevel);

            } else {
                Position = new Vector2(batHitBox.X + batHitBox.Width / 2 - this.Texture.Width / 2, batHitBox.Y - this.Texture.Height);

            }

            base.Update(gameTime);

        }
        private void bouncingOnTheWalls() {
            if ((Position.Y <= 0 && Direction.Y < 0)) {
                this.sm.bump.Play();
                Direction = new Vector2(Direction.X, -Direction.Y);

            }
            if (Position.X <= 0 && Direction.X < 0 || Position.X > screenWidth - Texture.Height && Direction.X > 0) {
                this.sm.bump.Play();
                Direction = new Vector2(-Direction.X, Direction.Y);
            }
        }
        private void bouncingOnTheBat(Rectangle batHitBox) {
            if ((Direction.Y > 0 && this.Hitbox.IntersectsRec(batHitBox))) {
                Direction = new Vector2(((float)Hitbox.X - batHitBox.Center.X) / (batHitBox.Width / 2), -Direction.Y);
                Direction = Vector2.Normalize(Direction);
                Console.WriteLine(Direction);
                this.sm.bump.Play();
                if (this.Hitbox.X <= batHitBox.X + batHitBox.Width && this.Hitbox.X > batHitBox.X + batHitBox.Width / 2) {


                }
            }
        }
        private void bouncingOnTheBricks(GameLevel gameLevel) {
            Vector2 centerTopBall = new Vector2((float)Hitbox.Radius / 2, 0);
            Vector2 centerLeftBall = new Vector2(0, (float)Hitbox.Radius / 2);
            Vector2 centerRightBall = new Vector2((float)Hitbox.Radius, (float)Hitbox.Radius / 2);
            Vector2 centerBottomBall = new Vector2((float)Hitbox.Radius / 2, (float)Hitbox.Radius);

            Vector2 newDirection = new Vector2(Direction.X, Direction.Y);

            List<Brick> destroyedBricks = getDestroyedBricks(gameLevel);



            foreach (Brick b in destroyedBricks) {
                bool changed = false;
                Vector2 topLeftCorner = new Vector2(b.Hitbox.Left, b.Hitbox.Top);
                Vector2 topRightCorner = new Vector2(b.Hitbox.Right, b.Hitbox.Top);
                Vector2 bottomLeftCorner = new Vector2(b.Hitbox.Left, b.Hitbox.Bottom);
                Vector2 bottomRightCorner = new Vector2(b.Hitbox.Right, b.Hitbox.Bottom);

                if (Direction.X > 0 && LineIntersects(lastPosition + centerRightBall, position + centerRightBall, topLeftCorner, bottomLeftCorner)) {
                    newDirection = new Vector2(-Direction.X, Direction.Y);
                    Console.WriteLine("left");
                    changed = true;
                } else if (Direction.X < 0 && LineIntersects(lastPosition + centerLeftBall, position + centerLeftBall, topRightCorner, bottomRightCorner)) {
                    newDirection = new Vector2(-Direction.X, Direction.Y);
                    Console.WriteLine("right");
                    changed = true;
                }

                if (Direction.Y > 0 && LineIntersects(lastPosition + centerBottomBall, position + centerBottomBall, topLeftCorner, topRightCorner)) {
                    newDirection = new Vector2(Direction.X, -Direction.Y);
                    Console.WriteLine("top");
                    changed = true;
                } else if (Direction.Y < 0 && LineIntersects(lastPosition + centerTopBall, position + centerTopBall, bottomLeftCorner, bottomRightCorner)) {
                    newDirection = new Vector2(Direction.X, -Direction.Y);
                    Console.WriteLine("bottom");
                    changed = true;
                }

                // Hit a corner  bounce it back
                if (!changed) {
                    Console.WriteLine("corner");
                    Direction = new Vector2(-Direction.X, -Direction.Y);

                }


                Console.WriteLine(Direction);
                break;

            }
            Direction = newDirection;
        }
        private bool LineIntersects(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4) {
            float a = (p4.X - p3.X) * (p1.Y - p3.Y) - (p4.Y - p4.Y) * (p1.X - p3.X);
            float b = (p2.X - p1.X) * (p1.Y - p3.Y) - (p2.Y - p1.Y) * (p1.X - p3.X);
            float c = (p4.Y - p3.Y) * (p2.X - p1.X) - (p4.X - p3.X) * (p2.Y - p1.Y);
            bool intersects = false;

            if (Math.Abs(c) >= 0.00001f) {
                a /= c;
                b /= c;

                if (a >= 0 && a <= 1 && b >= 0 && b <= 1) {
                    intersects = true;
                }
            }

            return intersects;
        }

        public List<Brick> getDestroyedBricks(GameLevel gameLevel) {
            List<Brick> destroyedBricks = new List<Brick>();
            foreach (Brick b in gameLevel.BricksMap) {
                if (this.Hitbox.IntersectsRec(b.Hitbox) && b.Resistance > 0) {
                    this.sm.bumpBrick.Play();
                    Console.WriteLine(lastBrick.Position);
                    Console.WriteLine(b.Position);

                    if (b.Bonus.Type != BonusType.NONE && b.Bonus.Activated == false) {
                        b.Bonus.Activated = true;
                    }

                    int scoreIncrement = b.Touched(gameLevel.BrickTexture);
                    gameLevel.Score += scoreIncrement;
                    if (scoreIncrement == 100) {
                        gameLevel.Nb_bricks--;
                    }
                    destroyedBricks.Add(b);
                }
            }
            return destroyedBricks;
        }


        /* public void bouncingOnTheBricks( GameLevel gameLevel) {
             List<Brick> destroyedBricks = getDestroyedBricks(gameLevel);

            
             foreach (Brick b in destroyedBricks) {

                 Rectangle[] sideRectangles = buildSideAndCornerRectangles(b);
                     if (lastBrick.Position != b.Position) {
                         Rectangle[] sideRectanglesLast = buildSideAndCornerRectangles(lastBrick);
                         if ((Hitbox.IntersectsRec(sideRectangles[5]) && Hitbox.IntersectsRec(sideRectanglesLast[4])) || (Hitbox.IntersectsRec(sideRectangles[4]) && Hitbox.IntersectsRec(sideRectanglesLast[5]))) {
                            Direction = new Vector2(Direction.X, -Direction.Y);
                            Console.WriteLine("coin haut droit A + coin haut gauche B :" + Direction);
                            break;
                         } else if ((Hitbox.IntersectsRec(sideRectangles[7]) && Hitbox.IntersectsRec(sideRectanglesLast[6])) || (Hitbox.IntersectsRec(sideRectangles[6]) && Hitbox.IntersectsRec(sideRectanglesLast[7]))) {
                            Direction = new Vector2(Direction.X, -Direction.Y);
                            Console.WriteLine("coin bas droit A + coin bas gauche B :" + Direction);
                            break;
                         } else if ((Hitbox.IntersectsRec(sideRectangles[5]) && Hitbox.IntersectsRec(sideRectanglesLast[7])) || (Hitbox.IntersectsRec(sideRectangles[7]) && Hitbox.IntersectsRec(sideRectanglesLast[5]))) {
                            Direction = new Vector2(-Direction.X, -Direction.Y);
                            Console.WriteLine("coin haut droit A + coin bas droit B :" + Direction);
                            break;
                         } else if ((Hitbox.IntersectsRec(sideRectangles[4]) && Hitbox.IntersectsRec(sideRectanglesLast[6])) || (Hitbox.IntersectsRec(sideRectangles[6]) && Hitbox.IntersectsRec(sideRectanglesLast[4]))) {
                            Direction = new Vector2(-Direction.X, Direction.Y);
                            Console.WriteLine("coin haut gauche A + coin bas gauche B :" + Direction);
                            break;
                        }
                    }

                    if (singleCornerHit(sideRectangles)) {
                        Direction = new Vector2(-Direction.X, -Direction.Y);
                        Console.WriteLine("un coin " + Direction);
                        break;
                     } else if (Hitbox.IntersectsRec(sideRectangles[0]) || Hitbox.IntersectsRec(sideRectangles[1])) {

                        Direction = new Vector2(Direction.X, -Direction.Y);
                        Console.WriteLine("top || bottom : " + Direction);
                        break;
                     } else if (Hitbox.IntersectsRec(sideRectangles[2]) || Hitbox.IntersectsRec(sideRectangles[3])) {
                        Direction = new Vector2(-Direction.X, Direction.Y);
                        Console.WriteLine("right || left : " + Direction);
                        break;



                    }
                

                lastBrick = b;


            }

        }

         public Rectangle[] buildSideAndCornerRectangles(Brick b) {
             Rectangle[] sideAndCornerRectangles = new Rectangle[8];
             sideAndCornerRectangles[0] = new Rectangle(b.Hitbox.X, b.Hitbox.Top, b.Hitbox.Width, 0); //TOP
             sideAndCornerRectangles[1] = new Rectangle(b.Hitbox.X, b.Hitbox.Bottom, b.Hitbox.Width, 0); //Bottom
             sideAndCornerRectangles[2] = new Rectangle(b.Hitbox.Left, b.Hitbox.Y, 0, b.Hitbox.Height); //Left
             sideAndCornerRectangles[3] = new Rectangle(b.Hitbox.Right, b.Hitbox.Y, 0, b.Hitbox.Height); //Right
             sideAndCornerRectangles[4] = new Rectangle(b.Hitbox.X, b.Hitbox.Y, 15, 15); //TOP-LEFT
             sideAndCornerRectangles[5] = new Rectangle(b.Hitbox.X + b.Hitbox.Width, b.Hitbox.Y, 15, 15); //TOP-RIGHT
             sideAndCornerRectangles[6] = new Rectangle(b.Hitbox.X, b.Hitbox.Y + b.Hitbox.Height, 15, 15); //BOTTOM-LEFT
             sideAndCornerRectangles[7] = new Rectangle(b.Hitbox.X + b.Hitbox.Width, b.Hitbox.Y + b.Hitbox.Height, 15, 15); //BOTTOM-RIGHT


             return sideAndCornerRectangles;
        }

        public bool singleCornerHit(Rectangle[] sideRectangles) {
             return ((Hitbox.IntersectsRec(sideRectangles[0]) && (Hitbox.IntersectsRec(sideRectangles[2]))) || (Hitbox.IntersectsRec(sideRectangles[0]) && (Hitbox.IntersectsRec(sideRectangles[3]))) || (Hitbox.IntersectsRec(sideRectangles[1]) && (Hitbox.IntersectsRec(sideRectangles[2]))) || (Hitbox.IntersectsRec(sideRectangles[1]) && (Hitbox.IntersectsRec(sideRectangles[3]))));
             //return (Hitbox.IntersectsRec(sideRectangles[4]) || Hitbox.IntersectsRec(sideRectangles[5]) || Hitbox.IntersectsRec(sideRectangles[6]) || Hitbox.IntersectsRec(sideRectangles[7]));
        }
         */
    }
}
