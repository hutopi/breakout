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

namespace breakout {
    class Ball : Sprite {
        private int screenHeight;
        private int screenWidth;
        public Circle hitbox {
            //the Postions are taken from the top left of the circle and not the center so you have to to move the postion with half the texture of the sprite to a have a circle hitbox that fits correclty the circle
            get { return new Circle((int)Position.X + Texture.Width / 2, (int)Position.Y + Texture.Height / 2, (double)Texture.Width / 2); }
        }
       

        public Ball(int screenWidth, int screenHeight) {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;

        }
        public override void Initialize() {

            base.Initialize();


            Direction = new Vector2(0, -1);

            Speed = 0.2f;

        }


        public void LoadContent(ContentManager content, String assetName, Bat bat) {

            base.LoadContent(content, assetName);
            Position = new Vector2(bat.Position.X + bat.Texture.Width / 2 - Texture.Width / 2, bat.Position.Y - bat.Texture.Height - Texture.Height / 2);
            //Position = new Vector2(100, 100);
        }

        public void Update(GameTime gameTime, Rectangle batHitBox) {

            if ((Position.Y <= 0 && Direction.Y < 0)) {

                Direction = new Vector2(Direction.X, -Direction.Y);

            }
            if (Position.X <= 0 && Direction.X < 0 || Position.X > screenWidth - Texture.Height / 2 && Direction.X > 0) {
                Direction = new Vector2(-Direction.X, Direction.Y);
            }


            if ((Direction.Y > 0 && this.hitbox.IntersectsRec(batHitBox))) {
                if (this.Position.X > batHitBox.X) {
                    Direction = new Vector2(-1, -Direction.Y);
                    //Console.WriteLine(Direction);
                }
                if (this.Position.X < batHitBox.X) {
                    Direction = (new Vector2((batHitBox.X - this.Position.X) * Direction.X, -Direction.Y));
                    Console.WriteLine(Direction);
                }
                if (this.Position.X == batHitBox.X) {
                    Direction = new Vector2(Direction.X, -Direction.Y);
                    Console.WriteLine(Direction);
                }
                Speed += 0.05f;

            }


            base.Update(gameTime);

        }
    }
}
