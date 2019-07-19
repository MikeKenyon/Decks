using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Configuration
{
    /// <summary>
    /// By default, a <see cref="ITable{TElement}"/> has a maximum size the same as its
    /// initial size.   This is the rule used to handle when the tableaut gets to be 
    /// oversized.
    /// </summary>
    public enum TableauOverflowRule
    {
        /// <summary>
        /// Let the tableau grow larger, effectively grants no maximum size.
        /// </summary>
        Ignore = 0,
        /// <summary>
        /// Drop the oldest element in the tableau.
        /// </summary>
        DiscardOldest,
        /// <summary>
        /// Drop the most recently added element to the tableau.
        /// </summary>
        DiscardNewest,
        /// <summary>
        /// Drop a randomly selected card from the tableau.
        /// </summary>
        DiscardRandom,
        /// <summary>
        /// Ask which one we should discard.
        /// </summary>
        Ask
    }
}
