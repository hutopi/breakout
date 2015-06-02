using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace breakout
{
    public class Brick : Sprite
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public Bonus Bonus { get; set; }

        public int Resistance { get; set; }

        public Rectangle Hitbox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        public Brick(int screenWidth, int screenHeight, Vector2 position, int h, int w, int r = 1, BonusType b = BonusType.NONE) : base( screenWidth, screenHeight)
        {
            this.Height = h;
            this.Width = w;
            this.Position = position;
            this.Resistance = r;
            this.Bonus = new Bonus(screenWidth, screenHeight, b);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            assetName += this.Resistance;
            base.LoadContent(content, assetName);
        }

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

        public override void Update()
        {
            base.Update();
        }
    }
}
