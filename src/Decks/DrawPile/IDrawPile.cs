namespace Decks
{
    /// <summary>
    /// The stack in the deck that represents the unknown and undrawn elements.  It's from here that you draw cards into a 
    /// hand, onto the table or into the tableau.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public interface IDrawPile<TElement> : IDeckStack<TElement> where TElement : class
    {
        /// <summary>
        /// The options related to the draw pile.
        /// </summary>
        Configuration.IDrawPileOptions Options { get; }
        /// <summary>
        /// Optionally gets the discards back and then randomly orders the cards.
        /// </summary>
        /// <param name="retreiveDiscards">Whether or not to clear out the discards.</param>
        /// <returns>Draw pile (for fluent purposes)</returns>
        IDrawPile<TElement> Shuffle(bool retreiveDiscards = true);
    }
}
