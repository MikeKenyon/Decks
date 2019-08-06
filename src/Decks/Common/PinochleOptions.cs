namespace Decks.Common
{
    /// <summary>
    /// The options used for a pinochle deck.
    /// </summary>
    public class PinochleOptions : PlayingCardOptions
    {
        /// <summary>
        /// The options for a pinochle deck.
        /// </summary>
        public PinochleOptions()
        {
            AceMode = AceMode.High;
            HasJokers = false;
        }
    }
}
