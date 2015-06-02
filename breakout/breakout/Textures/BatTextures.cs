using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace breakout.Textures
{
    public class BatTextures
    {
        private Texture2D reduced;
        public Texture2D Reduced
        {
            get { return reduced; }
            set { reduced = value; }
        }

        private Texture2D regular;
        public Texture2D Regular
        {
            get { return regular; }
            set { regular = value; }
        }

        private Texture2D extended;
        public Texture2D Extended
        {
            get { return extended; }
            set { extended = value; }
        }


        public BatTextures(Texture2D reduced, Texture2D regular, Texture2D extended)
        {
            this.reduced = reduced;
            this.regular = regular;
            this.extended = extended;
        }
    }
}
