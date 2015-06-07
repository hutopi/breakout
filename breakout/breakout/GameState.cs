// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Thomas Fossati
// Created          : 05-27-2015
//
// Last Modified By : Thomas Fossati
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="GameState.cs" company="Hutopi">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// The breakout namespace.
/// </summary>
namespace breakout {
    /// <summary>
    /// Enum GameState
    /// Describes the state of the game
    /// </summary>
    public enum GameState {
        /// <summary>
        /// The startmenu state
        /// </summary>
        STARTMENU = 0,
        /// <summary>
        /// The playing state
        /// </summary>
        PLAYING = 1,
        /// <summary>
        /// The paused state
        /// </summary>
        PAUSED = 2,
        /// <summary>
        /// The win state
        /// </summary>
        WIN = 3,
        /// <summary>
        /// The loose state
        /// </summary>
        LOOSE = 4,
        /// <summary>
        /// The restart state
        /// </summary>
        RESTART = 5,
        /// <summary>
        /// The next level
        /// </summary>
        NEXT_LEVEL = 6,
        /// <summary>
        /// The exit state
        /// </summary>
        EXIT = 7,
        /// <summary>
        /// The readytostart state
        /// </summary>
        READYTOSTART = 8,
        /// <summary>
        /// The custom state
        /// </summary>
        CUSTOM = 9
    }
}
