using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    /// <summary>
    /// A tableau is an area with face-up elements.  It is backfilled from the top-deck.
    /// </summary>
    /// <remarks>
    /// Generally, the tableau exists to give players an set of options that they can opt
    /// between.  Usually, you have a choice of either something in the tableau or the top
    /// of the draw pile.  Sometimes, you have to pull from the tableau.
    /// </remarks>
    public interface ITableau<TElement> : IDeckStack<TElement>
        where TElement : class
    {
        /// <summary>
        /// The options for this tableau.
        /// </summary>
        Configuration.ITableauOptions Options { get; }
        /// <summary>
        /// Whether or not this setup uses a tableau.
        /// </summary>
        bool Enabled { get; }
        /// <summary>
        /// Draws the tableau up to its configured size.
        /// </summary>
        /// <remarks>
        /// This uses the <see cref="TableauOverflowRule"/> set in the options to 
        /// explain how to handle overage.  Underage is handled by drawing from 
        /// the top deck.
        /// </remarks>
        /// <param name="from">Which side of the draw pile we're drawing from.</param>
        void DrawUp(DeckSide from = DeckSide.Top);

        /// <summary>
        /// Plays an element from this tableau to the table.
        /// </summary>
        /// <param name="element">The element to play to the table.</param>
        void Play(TElement element);

        /// <summary>
        /// Has a specific hand draw up a given element.
        /// </summary>
        /// <param name="element">The element to play to the table.</param>
        /// <param name="hand">The hand to draw it into.</param>
        void DrawInto(TElement element, IHand<TElement> hand);
    }
}
