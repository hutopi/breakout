using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace breakout.Textures
{
    public class SoundTextures
    {
        public Texture2D On { get; set; }
        public Texture2D Off { get; set; }

        public SoundTextures()
        {
            On = null;
            Off = null;
        }

        public void LoadContent(Texture2D on, Texture2D off)
        {
            On = on;
            Off = off;
        }
    }
}
