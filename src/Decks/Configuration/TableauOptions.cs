using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Configuration
{
    public class TableauOptions : PropertyChangedBase, ITableauOptions
    {
        #region Data
        private bool _enabled = false;
        private uint _initial = 0;
        private uint? _max = null;
        private bool _maintain;
        private TableauOverflowRule _overflow = TableauOverflowRule.Ignore;
        private bool _table = true;
        private bool _hand = true;
        private bool _safe = false;
        #endregion

        /// <summary>
        /// Whether or not the tableau is enabled.  It is not by default.
        /// </summary>
        public bool Enabled { get { return _enabled; } set { _enabled = value; NotifyOfPropertyChange(); } }
        /// <summary>
        /// The number of elements in the tableau, set to 0 to disable the tableau.
        /// </summary>
        public uint InitialSize { get { return _initial; } set { _initial = value; NotifyOfPropertyChange(); } }
        /// <summary>
        /// The maximum size the table can grow to.  This may be set to <see langword="null"/> to remove any maximum capacity.
        /// </summary>
        public uint? MaximumSize { get { return _max; } set { _max = value; NotifyOfPropertyChange(); } }

        /// <summary>
        /// Whether the system automatically adjusts the tableau after something modifies 
        /// it's contents.
        /// </summary>
        public bool MaintainSize { get { return _maintain; } set { _maintain = value; NotifyOfPropertyChange(); } }
        /// <summary>
        /// What to do what a tableau gets to be oversized.  Ignored if <see cref="MaximumSize"/> is <see langword="null"/>.
        /// </summary>
        public TableauOverflowRule OverflowRule { get { return _overflow; } set { _overflow = value; NotifyOfPropertyChange(); } }

        /// <summary>
        /// If set (<see langword="true"/> by default), you are allowed to play from the tableau straight into the <see cref="ITable{TElement}"/>.
        /// </summary>
        public bool CanPlayToTable { get { return _table; } set { _table = value; NotifyOfPropertyChange(); } }
        /// <summary>
        /// If set (<see langword="true"/> by default), you are allowed to draw from the tableau straight into a <see cref="IHand{TElement}"/>.
        /// </summary>
        public bool CanDrawIntoHand { get { return _hand; } set { _hand = value; NotifyOfPropertyChange(); } }


        /// <summary>
        /// In many games, if the tableau cannot safely draw up, it just shrinks in size.
        /// If this option is set to <see langword="true"/> then the tableau cannot bottom
        /// deck you.
        /// </summary>
        public bool DrawsUpSafely { get { return _safe; } set { _safe = value; NotifyOfPropertyChange(); } }

    }
}
