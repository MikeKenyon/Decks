﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Configuration
{
    public class HandOptions : IHandOptions
    {
        /// <summary>
        /// Are you allowed to draw hands at all.  This is set to <see langword="true"/> by default.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// The number of cards that go into a hand when initially drawn.
        /// </summary>
        public uint InitialHandSize { get; set; }

    }
}
