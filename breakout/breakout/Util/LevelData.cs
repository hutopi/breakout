using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace breakout.Util
{
    public class LevelData
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        public List<object> Bricks { get; set; }
        public Dictionary<string, object> Background { get; set; }
        public Dictionary<string, object> Music { get; set; }
    }
}
