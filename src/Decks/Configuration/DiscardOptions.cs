using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Configuration
{
    /// <summary>
    /// Options for the discard pile.
    /// </summary>
    public class DiscardOptions : PropertyChangedBase, IDiscardOptions
    {
        private bool _autoShuffle;
        /// <summary>
        /// Automatically shuffles the deck when you need another card 
        /// and one isn't available.
        /// </summary>
        public bool AutoShuffle { get { return _autoShuffle; } set { _autoShuffle = value; NotifyOfPropertyChange(); } }
    }
}
