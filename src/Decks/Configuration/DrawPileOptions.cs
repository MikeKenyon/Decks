using Caliburn.Micro;

namespace Decks.Configuration
{
    /// <summary>
    /// Options for the draw pile, which is always enabled.
    /// </summary>
    public class DrawPileOptions : PropertyChangedBase, IDrawPileOptions
    {
        private uint? _maxShuffles = null;

        /// <summary>
        /// The number of times the deck can be reshuffled.  0, 1, 3 and a negative number are the most commonly 
        /// set numbers.  <see langword="null" /> is unlimited, which is the default.
        /// </summary>
        public uint? MaximumShuffleCount { get { return _maxShuffles; } set { _maxShuffles = value; NotifyOfPropertyChange(); } }
    }
}
