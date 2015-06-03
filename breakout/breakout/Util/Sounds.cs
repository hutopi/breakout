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
    public class Sounds
    {
        public SoundEffect Bump { get; set; }
        public SoundEffect Pause { get; set; }
        public SoundEffect Win { get; set; }
        public SoundEffect Loose { get; set; }

        public Sounds()
        {
            Bump = null;
            Pause = null;
            Win = null;
            Loose = null;
        }

        public void LoadContent(SoundEffect b, SoundEffect p, SoundEffect w, SoundEffect l)
        {
            Bump = b;
            Pause = p;
            Win = w;
            Loose = l;
        }

    }
}
