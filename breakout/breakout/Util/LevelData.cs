// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Hugo Caille
// Created          : 06-07-2015
//
// Last Modified By : Hugo Caille
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="LevelData.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//      Breakout is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

/// <summary>
/// The Util namespace.
/// </summary>
namespace breakout.Util
{
    /// <summary>
    /// Class LevelData.
    /// </summary>
    public class LevelData
    {
        /// <summary>
        /// Gets or sets the lines.
        /// </summary>
        /// <value>The lines.</value>
        public int Lines { get; set; }
        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>The columns.</value>
        public int Columns { get; set; }
        /// <summary>
        /// Gets or sets the bricks.
        /// </summary>
        /// <value>The bricks.</value>
        public List<object> Bricks { get; set; }
        /// <summary>
        /// Gets or sets the background.
        /// </summary>
        /// <value>The background.</value>
        public Dictionary<string, object> Background { get; set; }
        /// <summary>
        /// Gets or sets the music.
        /// </summary>
        /// <value>The music.</value>
        public Dictionary<string, object> Music { get; set; }
    }
}
