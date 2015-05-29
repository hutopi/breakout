using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace breakout {
    public enum GameState {
        STARTMENU = 0,
        PLAYING = 1,
        PAUSED = 2,
        WIN = 3,
        LOOSE = 4,
        RESTART = 5,
        NEXT_LEVEL = 6,
        EXIT = 7,
        READYTOSTART = 8
    }
}
