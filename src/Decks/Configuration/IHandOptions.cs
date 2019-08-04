using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Decks.Configuration
{
    /// <summary>
    /// Options dealing with hands
    /// </summary>
    public interface IHandOptions : INotifyPropertyChanged
    {
        /// <summary>
        /// Are you allowed to draw hands at all.  This is set to <see langword="true"/> by default.
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// The number of cards that go into a hand when initially drawn.
        /// </summary>
        uint InitialHandSize { get; }

    }
}
