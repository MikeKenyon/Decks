using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Configuration
{
    public class HandOptions : PropertyChangedBase, IHandOptions
    {
        private bool _enabled = true;
        private uint _initial;

        /// <summary>
        /// Are you allowed to draw hands at all.  This is set to <see langword="true"/> by default.
        /// </summary>
        public bool Enabled { get { return _enabled; } set { _enabled = value; NotifyOfPropertyChange(); } }

        /// <summary>
        /// The number of cards that go into a hand when initially drawn.
        /// </summary>
        public uint InitialHandSize { get { return _initial; } set { _initial = value; NotifyOfPropertyChange(); } }

    }
}
