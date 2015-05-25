using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace breakout
{
    public class Brick : Sprite
    {
        public int height { get; set; }
        public int width { get; set; }

        public Bonus bonus { get; set; }

        public int resistance { get; set; }

        public Rectangle hitbox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        public Brick(int h, int w, int r = 1, Bonus b = Bonus.NONE)
        {
            this.height = h;
            this.width = w;
            this.resistance = r;
            this.bonus = b;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            base.LoadContent(content, assetName);
        }

        public override void Update()
        {
            base.Update();

        }
    }
}
