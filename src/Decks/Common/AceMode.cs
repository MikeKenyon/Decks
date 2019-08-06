namespace Decks.Common
{
    /// <summary>
    /// How aces are handled.
    /// </summary>
    public enum AceMode
    {
        /// <summary>
        /// Aces are high cards.
        /// </summary>
        High,
        /// <summary>
        /// Aces are high or low.
        /// </summary>
        HighLow,
        /// <summary>
        /// Aces are always low.
        /// </summary>
        Low,
    }
}
