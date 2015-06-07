using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace breakout
{
    public class DefaultLevels
    {
        public int MaxLevel { get; set; }
        public int Current { get; set; }
        public List<GameFile> LevelFiles { get; set; }

        public DefaultLevels()
        {
            this.LevelFiles = new List<GameFile>();
            this.Current = 1;
        }

        public void loadFiles()
        {
            var directory = Directory.GetCurrentDirectory() + @"\levels";
            List<String> filenames = Directory.EnumerateFiles(directory, "level*.json").ToList();
            for (int level = 1; level <= filenames.Count; level++)
            {
                var path = String.Format(Directory.GetCurrentDirectory() + @"\levels\level{0}.json", level);
                if (File.Exists(path))
                {
                    this.LevelFiles.Add(new GameFile(path));
                    this.MaxLevel++;
                }
                else
                {
                    break;
                }
            }
        }

        public GameFile getLevel()
        {
            return this.LevelFiles[this.Current - 1];
        }

        public void nextLevel()
        {
            this.Current++;
        }
    }
}
