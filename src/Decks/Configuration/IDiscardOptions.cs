﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Decks.Configuration
{
    /// <summary>
    /// Options for the discard pile.
    /// </summary>
    public interface IDiscardOptions : INotifyPropertyChanged
    {
        /// <summary>
        /// Automatically shuffles the deck when you need another card 
        /// and one isn't available.
        /// </summary>
        bool AutoShuffle { get; }

    }
}
