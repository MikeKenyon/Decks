using Decks.Configuration;
using Decks.Events;
using Decks.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Decks
{
    public partial class Deck<TElement> : IEnumerable<TElement>, Internal.IDeckInternal<TElement> where TElement : class
    {
        #region Data
        private bool Initialized { get; set; }
        private List<TElement> Known { get; } = new List<TElement>();
        private DrawPile<TElement> _drawPile;
        private DiscardPile<TElement> _discards;
        private Table<TElement> _table;
        private Tableau<TElement> _tableau;
        #endregion

        #region Construction
        public Deck(IDeckOptions options)
        {
            Contract.Requires(options != null);

            Options = options;
            Hands = new ReadOnlyCollection<IHand<TElement>>(HandSet);
            _drawPile = new DrawPile<TElement>(this);
            _discards = new DiscardPile<TElement>(this);
            _table = new Table<TElement>(this);
            _tableau = new Tableau<TElement>(this);

            Initialize();
            Initialized = true;
        }

        #endregion

        #region Properties
        Internal.IDrawPileInternal<TElement> Internal.IDeckInternal<TElement>.DrawPileStack { get { return _drawPile; } }
        Internal.IDiscardPileInternal<TElement> Internal.IDeckInternal<TElement>.DiscardPileStack { get { return _discards; } }
        Internal.ITableauInternal<TElement> Internal.IDeckInternal<TElement>.TableauStack { get { return _tableau; } }
        public IDeckOptions Options { get; }
        public ITableau<TElement> Tableau { get { return _tableau; } }


        #region Counts
        public int Count { get { return _drawPile.Count; } }
        public int TotalCount { get { return Known.Count; } }
        #endregion
        #endregion

        #region Public Interface
        public IDiscardPile<TElement> DiscardPile { get { return _discards; } }
        public IDrawPile<TElement> DrawPile { get { return _drawPile; } }

        IDeckEvents<TElement> IDeckInternal<TElement>.Events
        {
            get
            {
                return this.Events;
            }
        }
        private IDeckEvents<TElement> Events
        {
            get
            {
                if (Options.Events != null && Options.Events is IDeckEvents<TElement> tEvents)
                {
                    return tEvents;
                }
                else if (Options.Events != null)
                {
                    if (Options.Events.GetType() == typeof(IDeckEvents))
                    {
                        throw new InvalidCastException($"The deck events need to implement {typeof(IDeckEvents<TElement>).Name}, not the non-generic base.");
                    }
                    else
                    {
                        throw new InvalidCastException($"The deck events need to implement {typeof(IDeckEvents<TElement>).Name}, you provided {Options.Events.GetType().Name}.");
                    }
                }
                else
                {
                    return Internal.Events.EmptyDeckEvents<TElement>.Singleton;
                }
            }
        }

        /// <summary>
        /// Determines if an area contains an element.
        /// </summary>
        /// <param name="element">The element to look for.</param>
        /// <param name="location">The location to check.</param>
        /// <returns><see langword="true"/> if the element is in that location.</returns>
        public bool Contains(TElement element, Location location = Location.TopDeck)
        {
            Contract.Requires(Enum.IsDefined(typeof(Location), location));

            switch(location)
            {
                case Location.DiscardPile:
                    return DiscardPile.Contains(element);
                case Location.Hand:
                    return HandSet.Any(hand => hand.Contains(element));
                case Location.Table:
                    return Table.Contains(element);   
                case Location.TopDeck:
                    return DrawPile.Contains(element);
            }
            return false;
        }
        /// <summary>
        /// Adds a card to the deck.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <param name="location">The location to add it to.</param>
        /// <returns>This same deck (for FLUID interface reasons).</returns>
        public IDeck<TElement> Add(TElement element, Location location = Location.TopDeck)
        {
            Contract.Requires(Enum.IsDefined(typeof(Location), location));
            CheckAllowAdd();

            Add(element, DeckSide.Default, location);
            return this;
        }

        /// <summary>
        /// Adds a card to a specific location in the deck.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <param name="side">What side of the deck the item goes to.</param>
        /// <param name="location">The location to add it to.</param>
        /// <returns>This same deck (for FLUID interface reasons).</returns>
        public IDeck<TElement> Add(TElement element, DeckSide side, Location location = Location.TopDeck)
        {
            Contract.Requires(Enum.IsDefined(typeof(DeckSide), side));
            Contract.Requires(Enum.IsDefined(typeof(Location), location));
            CheckAllowAdd();

            switch (location)
            {
                case Location.TopDeck:
                    ((Internal.IDrawPileInternal<TElement>)this._drawPile).Add(element, side);
                    break;
                case Location.DiscardPile:
                    ((Internal.IDiscardPileInternal<TElement>)this._discards).Add(element, side);
                    break;
                case Location.Table:
                    if (side != DeckSide.Default)
                    {
                        throw new InvalidOperationException("Cannot add to the table on a specific side.");
                    }
                    else
                    {
                        ((Internal.ITableInternal<TElement>)this._table).Add(element);
                    }
                    break;
                case Location.Tableau:
                    if (side != DeckSide.Default)
                    {
                        throw new InvalidOperationException("Cannot add to the tableau on a specific side.");
                    }
                    else
                    {
                        ((Internal.ITableauInternal<TElement>)this._tableau).Add(element);
                    }
                    break;
                case Location.Hand:
                    throw new InvalidOperationException("Cannot add directly to a hand.");
                default:
                    throw new NotImplementedException($"Don't know about the {location} location.");
            }
            Known.Add(element);

            Events.Added(element, side, location);
            return this;
        }

        #endregion

        #region Protected Interface
        /// <summary>
        /// Allows you to setup before rules are enforced.
        /// </summary>
        protected virtual void Initialize()
        {

        }
        #endregion

        #region Protected Helpers
        /// <summary>
        /// Checks to see if an operation is allowed or not.
        /// </summary>
        /// <param name="condition">Condition to check.</param>
        /// <param name="message">The error message if it's not allowed.</param>
        protected virtual void CheckOperation(bool condition, string message)
        {
            if (!condition)
            {
                throw new InvalidOperationException(message);
            }
        }
        #endregion

        #region Private Helpers
        private void CheckAllowAdd()
        {
            if(Initialized && !Options.Modifiable)
            {
                throw new InvalidOperationException("Cannot modify this deck directly, the deck has been initialized.");
            }
        }

        #endregion

        #region IEnumerable
        public IEnumerator<TElement> GetEnumerator()
        {
            return ((Internal.IDrawPileInternal<TElement>)this._drawPile).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((Internal.IDrawPileInternal<TElement>)this._drawPile).GetEnumerator();
        }

        #endregion
    }
}
