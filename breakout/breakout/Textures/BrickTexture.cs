using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace breakout
{
    public class BrickTexture
    {
        private Texture2D zero;
        public Texture2D Zero
        {
            get { return zero; }
            set { zero = value; }
        }

        private Texture2D one;
        public Texture2D One
        {
            get { return one; }
            set { one = value; }
        }

        private Texture2D two;
        public Texture2D Two
        {
            get { return two; }
            set { two = value; }
        }

        private Texture2D three;
        public Texture2D Three
        {
            get { return three; }
            set { three = value; }
        }

        private Texture2D four;
        public Texture2D Four
        {
            get { return four; }
            set { four = value; }
        }

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
