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

namespace breakout
{
    public class SoundManager
    {
        public SoundEffect bump;
        public SoundEffect bumpBrick;
        public SoundEffect powerUp;
        public SoundEffect powerDown;

        public SoundManager()
        {
            bump = null;
            bumpBrick = null;
            powerUp = null;
            powerDown = null;
        }

        public void LoadContent(ContentManager Content)
        {
            bump = Content.Load<SoundEffect>("bump");
            bumpBrick = Content.Load<SoundEffect>("bumpBrick");
            powerUp = Content.Load<SoundEffect>("powerup");
            powerDown = Content.Load<SoundEffect>("powerdown");
        }
    }
}
