using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Common
{
    public class PinochleOptions : PlayingCardOptions
    {
        public PinochleOptions()
        {
            AceMode = AceMode.High;
            HasJokers = false;
        }
    }
}
