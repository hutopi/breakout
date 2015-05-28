using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace breakout
{
    public class BrickTexture
    {
        public Texture2D zero { get; set; }
        public Texture2D one { get; set; }
        public Texture2D two { get; set; }
        public Texture2D three { get; set; }
        public Texture2D four { get; set; }

        public BrickTexture(Texture2D[] textures)
        {
            this.zero = textures[0];
            this.one = textures[1];
            this.two = textures[2];
            this.three = textures[3];
            this.four = textures[4];
        }
    }
}
