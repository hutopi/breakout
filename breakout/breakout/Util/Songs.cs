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

namespace breakout.Util
{
    public class Songs
    {
        public Song One;
        public Song Two;
        public Song Three;
        public Song Four;

        public Songs()
        {
            One = null;
            Two = null;
            Three = null;
            Four = null;
        }

        public void LoadContent(Song one, Song two, Song three, Song four)
        {
            One = one;
            Two = two;
            Three = three;
            Four = four;
        }
    }
}
