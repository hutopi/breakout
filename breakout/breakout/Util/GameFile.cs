// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Hugo Caille
// Created          : 06-07-2015
//
// Last Modified By : Hugo Caille
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="GameFile.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//      Breakout is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.IO;
using Json;

/// <summary>
/// The breakout namespace.
/// </summary>
namespace breakout.Util
{
    /// <summary>
    /// Class GameFile.
    /// </summary>
    public class GameFile
    {
        /// <summary>
        /// The file path
        /// </summary>
        private string filePath;
        /// <summary>
        /// Stores this level's data.
        /// </summary>
        /// <value>The data.</value>
        public LevelData Data { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameFile"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public GameFile(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Loads and parse JSON file.
        /// </summary>
        public void Load()
        {
            FileStream stream = File.Open(this.filePath, FileMode.Open, FileAccess.Read);
            var reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            this.Data = JsonParser.Deserialize<LevelData>(json);
            stream.Close();
        }

    }
}
