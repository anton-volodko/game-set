using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AV.Set.Model
{
    /// <summary>
    /// Represent the current game state
    /// </summary>
    public enum GameState: byte
    {
        WaitingUsers,
        BoardScanning,
        SetSelection,
        Finished
    }
}
