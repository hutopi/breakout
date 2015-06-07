// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Pierre Defache
// Created          : 06-07-2015
//
// Last Modified By : Pierre Defache
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="SoundManager.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//      Breakout is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
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

/// <summary>
/// The breakout namespace.
/// </summary>
namespace breakout
{
    /// <summary>
    /// Class SoundManager.
    /// </summary>
    public class SoundManager
    {
        /// <summary>
        /// The bump
        /// </summary>
        public SoundEffect bump;
        /// <summary>
        /// The bump brick
        /// </summary>
        public SoundEffect bumpBrick;
        /// <summary>
        /// The power up
        /// </summary>
        public SoundEffect powerUp;
        /// <summary>
        /// The power down
        /// </summary>
        public SoundEffect powerDown;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundManager"/> class.
        /// </summary>
        public SoundManager()
        {
            bump = null;
            bumpBrick = null;
            powerUp = null;
            powerDown = null;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="Content">The content.</param>
        public void LoadContent(ContentManager Content)
        {
            bump = Content.Load<SoundEffect>("bump");
            bumpBrick = Content.Load<SoundEffect>("bumpBrick");
            powerUp = Content.Load<SoundEffect>("powerup");
            powerDown = Content.Load<SoundEffect>("powerdown");
        }
    }
}
