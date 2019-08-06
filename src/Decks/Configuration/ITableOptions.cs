using System.ComponentModel;

namespace Decks.Configuration
{
    /// <summary>
    /// Options for the play table.  This is the common shared space for all players.
    /// </summary>
    public interface ITableOptions : INotifyPropertyChanged
    {
        /// <summary>
        /// Whether or not the table is enabled.  By default is is <see cref="false"/>.
        /// </summary>
        bool Enabled { get; set; }
    }
}
