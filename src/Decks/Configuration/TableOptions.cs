﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Configuration
{
    /// <summary>
    /// Options for the play table.  This is the common shared space for all players.
    /// </summary>
    public class TableOptions : ITableOptions
    {
        /// <summary>
        /// Whether or not the table is enabled.  By default is is <see cref="false"/>.
        /// </summary>
        public bool Enabled { get; set; } = false;
    }
}