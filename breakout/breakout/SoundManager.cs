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

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

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
        /// The Bump
        /// </summary>
        public SoundEffect Bump;
        /// <summary>
        /// The Bump brick
        /// </summary>
        public SoundEffect BumpBrick;
        /// <summary>
        /// The power up
        /// </summary>
        public SoundEffect PowerUp;
        /// <summary>
        /// The power down
        /// </summary>
        public SoundEffect PowerDown;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundManager"/> class.
        /// </summary>
        public SoundManager()
        {
            Bump = null;
            BumpBrick = null;
            PowerUp = null;
            PowerDown = null;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public void LoadContent(ContentManager content)
        {
            Bump = content.Load<SoundEffect>("Bump");
            BumpBrick = content.Load<SoundEffect>("BumpBrick");
            PowerUp = content.Load<SoundEffect>("powerup");
            PowerDown = content.Load<SoundEffect>("powerdown");
        }
    }
}
