// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Pierre Defache
// Created          : 06-07-2015
//
// Last Modified By : Pierre Defache
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="Sounds.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//      Breakout is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.Xna.Framework.Audio;

/// <summary>
/// The Util namespace.
/// </summary>
namespace breakout.Util
{
    /// <summary>
    /// Class Sounds.
    /// </summary>
    public class Sounds
    {
        /// <summary>
        /// Gets or sets the bump.
        /// </summary>
        /// <value>The bump.</value>
        private SoundEffect bump { get; set; }
        /// <summary>
        /// Gets or sets the pause.
        /// </summary>
        /// <value>The pause.</value>
        public SoundEffect Pause { get; private set; }
        /// <summary>
        /// Gets or sets the win.
        /// </summary>
        /// <value>The win.</value>
        public SoundEffect Win { get; private set; }
        /// <summary>
        /// Gets or sets the loose.
        /// </summary>
        /// <value>The loose.</value>
        public SoundEffect Loose { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sounds"/> class.
        /// </summary>
        public Sounds()
        {
            bump = null;
            Pause = null;
            Win = null;
            Loose = null;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="p">The p.</param>
        /// <param name="w">The w.</param>
        /// <param name="l">The l.</param>
        public void LoadContent(SoundEffect b, SoundEffect p, SoundEffect w, SoundEffect l)
        {
            bump = b;
            Pause = p;
            Win = w;
            Loose = l;
        }

    }
}
