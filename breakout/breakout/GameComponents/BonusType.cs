// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Thomas Fossati
// Created          : 06-07-2015
//
// Last Modified By : Thomas Fossati
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="BonusType.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//      Breakout is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************

/// <summary>
/// The breakout namespace.
/// </summary>
namespace breakout.GameComponents
{
    /// <summary>
    /// Enum BonusType
    /// Describes the different bonus and malus of the game.
    /// </summary>
    public enum BonusType
    {
        /// <summary>
        /// The none bonus
        /// </summary>
        NONE = 0,
        /// <summary>
        /// The high speed bonus
        /// </summary>
        HIGH_SPEED = 1,
        /// <summary>
        /// The high resistance malus
        /// </summary>
        HIGH_RESISTANCE = 2,
        /// <summary>
        /// The bat reduced malus
        /// </summary>
        BAT_REDUCED = 3,
        /// <summary>
        /// The down life malus
        /// </summary>
        DOWN_LIFE = 4,
        /// <summary>
        /// The low resistance malus
        /// </summary>
        LOW_RESISTANCE = 5,
        /// <summary>
        /// The bat extended bonus
        /// </summary>
        BAT_EXTENDED = 6,
        /// <summary>
        /// The low speed bonus
        /// </summary>
        LOW_SPEED = 7,
        /// <summary>
        /// The one up life bonus
        /// </summary>
        UP_LIFE = 8,
        /// <summary>
        /// The multiplicate ball bonus
        /// </summary>
        MULTIPLICATE_BALL = 9,
    }
}
