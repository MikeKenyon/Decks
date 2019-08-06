using System.ComponentModel;

namespace Decks.Configuration
{
    /// <summary>
    /// Options for dealing with the tableau
    /// </summary>
    public interface ITableauOptions : INotifyPropertyChanged
    {
        /// <summary>
        /// Whether or not the tableau is enabled.
        /// </summary>
        bool Enabled { get; }
        /// <summary>
        /// The number of elements in the tableau.
        /// </summary>
        uint InitialSize { get; }
        /// <summary>
        /// The maximum size the table can grow to.  This may be set to <see langword="null"/> to remove any maximum capacity.
        /// </summary>
        uint? MaximumSize { get; }
        /// <summary>
        /// Whether the system automatically adjusts the tableau after something modifies 
        /// it's contents.  It draw up to its initial size, or discard down to it.
        /// </summary>
        bool MaintainSize { get; }
        /// <summary>
        /// What to do what a tableau gets to be oversized.  Ignored if <see cref="MaximumSize"/> is <see langword="null"/>.
        /// </summary>
        TableauOverflowRule OverflowRule { get; }
        /// <summary>
        /// If set (<see langword="true"/> by default), you are allowed to play from the tableau straight into the <see cref="ITable{TElement}"/>.
        /// </summary>
        bool CanPlayToTable { get; }
        /// <summary>
        /// If set (<see langword="true"/> by default), you are allowed to draw from the tableau straight into a <see cref="IHand{TElement}"/>.
        /// </summary>
        bool CanDrawIntoHand { get; }
        /// <summary>
        /// In many games, if the tableau cannot safely draw up, it just shrinks in size.
        /// If this option is set to <see langword="true"/> then the tableau cannot bottom
        /// deck you.
        /// </summary>
        bool DrawsUpSafely { get; }
    }
}
