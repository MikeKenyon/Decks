using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Configuration
{
    public class TableauOptions : ITableauOptions
    {
        /// <summary>
        /// Whether or not the tableau is enabled.  It is not by default.
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// The number of elements in the tableau, set to 0 to disable the tableau.
        /// </summary>
        public uint InitialSize { get; set; } = 0;
        /// <summary>
        /// The maximum size the table can grow to.  This may be set to <see langword="null"/> to remove any maximum capacity.
        /// </summary>
        public uint? MaximumSize { get; set; } = null;

        /// <summary>
        /// Whether the system automatically adjusts the tableau after something modifies 
        /// it's contents.
        /// </summary>
        public bool MaintainSize { get; set; }
        /// <summary>
        /// What to do what a tableau gets to be oversized.  Ignored if <see cref="MaximumSize"/> is <see langword="null"/>.
        /// </summary>
        public TableauOverflowRule OverflowRule { get; set; }

        /// <summary>
        /// If set (<see langword="true"/> by default), you are allowed to play from the tableau straight into the <see cref="ITable{TElement}"/>.
        /// </summary>
        public bool CanPlayToTable { get; set; } = true;
        /// <summary>
        /// If set (<see langword="true"/> by default), you are allowed to draw from the tableau straight into a <see cref="IHand{TElement}"/>.
        /// </summary>
        public bool CanDrawIntoHand { get; set; } = true;


        /// <summary>
        /// In many games, if the tableau cannot safely draw up, it just shrinks in size.
        /// If this option is set to <see langword="true"/> then the tableau cannot bottom
        /// deck you.
        /// </summary>
        public bool DrawsUpSafely { get; set; }

    }
}
