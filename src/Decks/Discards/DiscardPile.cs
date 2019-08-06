using Decks.Internal;

namespace Decks
{
    /// <summary>
    /// This is the stack that contains elements that have been discarded from hands, the table, etc.  The discarded elements
    /// can be readded to the <see cref="IDrawPile{TElement}"/>.
    /// </summary>
    /// <typeparam name="TElement">The elements in the discard pile.</typeparam>
    internal class DiscardPile<TElement> : DeckStack<TElement>, IDiscardPile<TElement>, Internal.IDiscardPileInternal<TElement>
        where TElement : class
    {
        /// <summary>
        /// Creates a discard pile.
        /// </summary>
        /// <param name="deck">The deck it's a discard for.</param>
        public DiscardPile(Deck<TElement> deck) : base(deck)
        {
        }
        /// <summary>
        /// Accepts a visitor to the discard pile.
        /// </summary>
        /// <param name="visitor">The visitor to accept.</param>
        void IDeckVisitable<TElement>.Accept(IDeckVisitor<TElement> visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        /// Adds an element to the discard pile.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <param name="side">
        ///     The side to add it to, allows for situations where you bury things in the discard pile.
        /// </param>
        void IDiscardPileInternal<TElement>.Add(TElement element, DeckSide side)
        {
            switch (side)
            {
                case DeckSide.Bottom:
                    Contents.Add(element);
                    break;
                case DeckSide.Default:
                case DeckSide.Top:
                    Contents.Insert(0, element);
                    break;
            }
        }

        /// <summary>
        /// Adds an element to the discard pile.
        /// </summary>
        /// <param name="element">The element to add.</param>
        void IDeckStackInternal<TElement>.Add(TElement element)
        {
            ((IDiscardPileInternal<TElement>)this).Add(element, DeckSide.Top);
        }

        /// <summary>
        /// Confirms this pile is enabled.
        /// </summary>
        void IDeckStackInternal<TElement>.CheckEnabled()
        {
            // No-Op
        }

        /// <summary>
        /// Adds the discards back to the draw pile (usually on the bottom).
        /// </summary>
        /// <returns>Discards (for fluent purposes)</returns>
        public IDiscardPile<TElement> Readd(DeckSide side)
        {
            Deck.Events.PuttingDiscardsBackIntoDrawPile();

            Contents.Apply(c => Deck.DrawPileStack.Add(c, side));
            Contents.Clear();

            Deck.Events.PutDiscardsBackIntoDrawPile();

            return this;
        }
    }
}
