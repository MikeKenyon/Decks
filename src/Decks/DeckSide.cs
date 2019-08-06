namespace Decks
{
    /// <summary>
    /// The side of a stack (draw pile, discards, etc.) that you want to perform an operation on.  This allows for bottom-dealing and burying cards - operations that are 
    /// common in some games, like Pandemic.
    /// </summary>
    public enum DeckSide
    {
        /// <summary>
        /// Item goes on the bottom of the deck, discard, etc.
        /// </summary>
        Bottom = -1,
        /// <summary>
        /// <see cref="Top"/> or <see cref="Bottom"/>, as suits that particular stack.
        /// </summary>
        Default = 0,
        /// <summary>
        /// Item goes on the top of the deck, disard, etc.
        /// </summary>
        Top = 1,
    }
}
