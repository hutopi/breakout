// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Pierre Defache
// Created          : 06-07-2015
//
// Last Modified By : Pierre Defache
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="BrickTexture.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//      Breakout is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The breakout namespace.
/// </summary>
namespace breakout
{
    /// <summary>
    /// Class BrickTexture.
    /// </summary>
    public class BrickTexture
    {
        /// <summary>
        /// The zero
        /// </summary>
        private Texture2D zero;
        /// <summary>
        /// Gets or sets the zero.
        /// </summary>
        /// <value>The zero.</value>
        public Texture2D Zero
        {
            get { return zero; }
            set { zero = value; }
        }

        /// <summary>
        /// The one
        /// </summary>
        private Texture2D one;
        /// <summary>
        /// Gets or sets the one.
        /// </summary>
        /// <value>The one.</value>
        public Texture2D One
        {
            get { return one; }
            set { one = value; }
        }

        /// <summary>
        /// The two
        /// </summary>
        private Texture2D two;
        /// <summary>
        /// Gets or sets the two.
        /// </summary>
        /// <value>The two.</value>
        public Texture2D Two
        {
            get { return two; }
            set { two = value; }
        }

        /// <summary>
        /// The three
        /// </summary>
        private Texture2D three;
        /// <summary>
        /// Gets or sets the three.
        /// </summary>
        /// <value>The three.</value>
        public Texture2D Three
        {
            get { return three; }
            set { three = value; }
        }

        /// <summary>
        /// The four
        /// </summary>
        private Texture2D four;
        /// <summary>
        /// Gets or sets the four.
        /// </summary>
        /// <value>The four.</value>
        public Texture2D Four
        {
            get { return four; }
            set { four = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrickTexture"/> class.
        /// </summary>
        /// <param name="textures">The textures.</param>
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
