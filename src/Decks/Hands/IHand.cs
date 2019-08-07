using System;

namespace Decks
{
    /// <summary>
    /// A hand is a collection of elements from a specific deck that are owned usually by a specific player.  
    /// </summary>
    /// <typeparam name="TElement">The type of the elements in this deck and it's hands.</typeparam>
    public interface IHand<TElement> : IDeckStack<TElement>, IDisposable where TElement : class
    {
        /// <summary>
        /// Draws a card from the draw pile into the hand.
        /// </summary>
        /// <param name="from">Where to draw the card from, top or bottom of the deck.</param>
        /// <returns>This hand (for fluent purposes).</returns>
        /// <exception cref="BottomDeckException">If there isn't a card to be dealt.</exception>
        IHand<TElement> Draw(DeckSide from = DeckSide.Top);

        /// <summary>
        /// Mucks this hand. You should remove this reference after mucking the hand.
        /// </summary>
        void Muck();

        /// <summary>
        /// Determines if this hand has previously been mucked.
        /// </summary>
        bool HasBeenMucked { get; }

        /// <summary>
        /// Plays this element onto the table.
        /// </summary>
        /// <param name="element">The element to play.</param>
        /// <returns>This hand (for fluent purposes).</returns>
        /// <exception cref="InvalidElementException">
        /// The element isn't part of this hand.
        /// </exception>
        /// <exception cref="ObjectDisposedException">If the hand has previously been mucked.</exception>
        IHand<TElement> Play(TElement element);

    }
}
