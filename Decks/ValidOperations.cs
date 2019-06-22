using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    [Flags]
    public enum ValidOperations
    {
        None        = 0,
        ShuffleOnce = 1 << 0,
        Reshuffle   = 1 << 1 | ShuffleOnce,
        Add         = 1 << 2,
        DrawMuck    = 1 << 3,
        PlayToTable = 1 << 4,
        All = Reshuffle | Add | DrawMuck | PlayToTable
    }
}
