using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMaze
{
    enum GameState
    {
        None,
        Intro,
        MainMenu,
        InGame,
        Reset,
        Steuerung,
        Credits,
        LoadLevelState,
        ChooseLevelState,
        StartGameAtLevel,
    }
}
