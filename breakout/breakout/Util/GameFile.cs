using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using breakout.Util;
using Json;

namespace breakout
{
    class GameFile
    {
        private string filePath;
        public LevelData Data { get; set; }

        public GameFile(string filePath)
        {
            this.filePath = filePath;
        }

        public void Load()
        {
            FileStream stream = File.Open(this.filePath, FileMode.Open, FileAccess.Read);
            var reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            this.Data = JsonParser.Deserialize<LevelData>(json);
        }

    }
}
