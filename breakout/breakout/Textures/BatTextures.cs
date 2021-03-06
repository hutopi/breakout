﻿// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Pierre Defache
// Created          : 06-07-2015
//
// Last Modified By : Pierre Defache
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="BatTextures.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//      Breakout is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The Textures namespace.
/// </summary>
namespace breakout.Textures
{
    /// <summary>
    /// Class BatTextures.
    /// </summary>
    public class BatTextures
    {
        /// <summary>
        /// The reduced
        /// </summary>
        private Texture2D reduced;
        /// <summary>
        /// Gets or sets the reduced.
        /// </summary>
        /// <value>The reduced.</value>
        public Texture2D Reduced
        {
            get { return reduced; }
        }

        /// <summary>
        /// The regular
        /// </summary>
        private Texture2D regular;
        /// <summary>
        /// Gets or sets the regular.
        /// </summary>
        /// <value>The regular.</value>
        public Texture2D Regular
        {
            get { return regular; }
        }

        /// <summary>
        /// The extended
        /// </summary>
        private Texture2D extended;
        /// <summary>
        /// Gets or sets the extended.
        /// </summary>
        /// <value>The extended.</value>
        public Texture2D Extended
        {
            get { return extended; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BatTextures"/> class.
        /// </summary>
        /// <param name="reduced">The reduced.</param>
        /// <param name="regular">The regular.</param>
        /// <param name="extended">The extended.</param>
        public BatTextures(Texture2D reduced, Texture2D regular, Texture2D extended)
        {
            this.reduced = reduced;
            this.regular = regular;
            this.extended = extended;
        }
    }
}
