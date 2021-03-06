﻿// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Hugo Caille
// Created          : 06-07-2015
//
// Last Modified By : Hugo Caille
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="DefaultLevels.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//      Breakout is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using breakout.Util;

/// <summary>
/// The breakout namespace.
/// </summary>
namespace breakout
{
    /// <summary>
    /// Class DefaultLevels.
    /// </summary>
    public class DefaultLevels
    {
        /// <summary>
        /// Gets or sets the maximum level.
        /// </summary>
        /// <value>The maximum level.</value>
        private int maxLevel { get; set; }
        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        /// <value>The current.</value>
        private int current { get; set; }
        /// <summary>
        /// Gets or sets the level files.
        /// </summary>
        /// <value>The level files.</value>
        private List<GameFile> levelFiles { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultLevels"/> class.
        /// </summary>
        public DefaultLevels()
        {
            this.levelFiles = new List<GameFile>();
            this.current = 1;
        }

        /// <summary>
        /// Loads the files.
        /// </summary>
        public void LoadFiles()
        {
            List<string> appPath = Assembly.GetExecutingAssembly().Location.Split('\\').ToList();
            appPath.RemoveAt(appPath.Count - 1);
            var levelsDir = String.Join("\\", appPath.ToArray()) + @"\levels";
            List<String> filenames = Directory.EnumerateFiles(levelsDir, "level*.json").ToList();
            for (int level = 1; level <= filenames.Count; level++)
            {
                var path = String.Format(levelsDir + @"\level{0}.json", level);
                if (File.Exists(path))
                {
                    this.levelFiles.Add(new GameFile(path));
                    this.maxLevel++;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <returns>GameFile.</returns>
        public GameFile GetLevel()
        {
            return this.levelFiles[this.current - 1];
        }

        /// <summary>
        /// Nexts the level.
        /// </summary>
        public void NextLevel()
        {
            this.current++;
        }

        public bool IsLastLevel()
        {
            return this.current < this.maxLevel;
        }
    }
}
