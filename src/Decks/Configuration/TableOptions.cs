using Caliburn.Micro;

namespace Decks.Configuration
{
    /// <summary>
    /// Options for the play table.  This is the common shared space for all players.
    /// </summary>
    public class TableOptions : PropertyChangedBase, ITableOptions
    {
        private bool _enabled = false;

        /// <summary>
        /// Whether or not the table is enabled.  By default is is <see cref="false"/>.
        /// </summary>
        public bool Enabled { get { return _enabled; } set { _enabled = value; NotifyOfPropertyChange(); } }
    }
}
