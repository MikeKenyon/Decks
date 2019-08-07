using Caliburn.Micro;
using Decks.Configuration;
using Decks.Events;
using Decks.Internal;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Decks
{
    /// <summary>
    /// The root type for the deck.  The deck unifies access to the different stacks that comprise the whole ecosystem.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public partial class Deck<TElement> : PropertyChangedBase, Internal.IDeckInternal<TElement> where TElement : class
    {
        #region Data
        private bool Initialized { get; set; }
        private ObservableCollection<TElement> Known { get; } = new ObservableCollection<TElement>();
        private readonly DrawPile<TElement> _drawPile;
        private readonly DiscardPile<TElement> _discards;
        private readonly Table<TElement> _table;
        private readonly Tableau<TElement> _tableau;
        #endregion

        #region Construction
        /// <summary>
        /// The general construction of a deck.  
        /// </summary>
        /// <param name="options">The options that denotw how this deck is supposed to operate.</param>
        /// <param name="doInitialize">
        /// Whether or not to run the initialization routine.  This is generally <see langword="true"/> for creating a deck and
        /// <see langword="false"/> when deserializing the deck.
        /// </param>
        public Deck(IDeckOptions options, bool doInitialize = true)
        {
            Contract.Requires(options != null);

            Known.CollectionChanged += OnKnownChanged;
            Options = options;
            ListenToOptions();
            Hands = new ReadOnlyObservableCollection<IHand<TElement>>(HandSet);
            _drawPile = new DrawPile<TElement>(this);
            _discards = new DiscardPile<TElement>(this);
            _table = new Table<TElement>(this);
            _tableau = new Tableau<TElement>(this);

            if (doInitialize)
            {
                Initialize();
            }
            Initialized = true;

        }
        /// <summary>
        /// When the overall contents of the ecosystem change, this is called.
        /// </summary>
        /// <param name="sender">The sending collection,</param>
        /// <param name="e">The collection changed events.</param>
        protected virtual void OnKnownChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyOfPropertyChange(() => Count);
        }
        #endregion

        #region Options
        /// <summary>
        /// Starts listening to the options.
        /// </summary>
        private void ListenToOptions()
        {
            Options.PropertyChanged += OnOptionPropertyChanged;
            Options.DrawPile.PropertyChanged += OnOptionPropertyChanged;
            Options.Discards.PropertyChanged += OnOptionPropertyChanged;
            Options.Table.PropertyChanged += OnOptionPropertyChanged;
            Options.Tableau.PropertyChanged += OnOptionPropertyChanged;
            Options.Hands.PropertyChanged += OnOptionPropertyChanged;
        }
        /// <summary>
        /// Stops listening to the options.
        /// </summary>
        private void DeafenToOptions()
        {
            Options.PropertyChanged -= OnOptionPropertyChanged;
            Options.DrawPile.PropertyChanged -= OnOptionPropertyChanged;
            Options.Discards.PropertyChanged -= OnOptionPropertyChanged;
            Options.Table.PropertyChanged -= OnOptionPropertyChanged;
            Options.Tableau.PropertyChanged -= OnOptionPropertyChanged;
            Options.Hands.PropertyChanged -= OnOptionPropertyChanged;
        }

        /// <summary>
        /// Flagged when any property in the options tree is changed.
        /// </summary>
        /// <param name="sender">The changed options object.</param>
        /// <param name="e">The event argument that indicates the options.</param>
        private void OnOptionPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is DeckOptions)
            {
                DeafenToOptions();
                ListenToOptions();
            }
            Refresh();
        }
        /// <summary>
        /// Called when the optiosn for this deck have been updated.
        /// </summary>
        protected virtual void OnOptionsUpdated() { }
        #endregion

        #region Properties
        /// <summary>
        /// The internal representation of the <see cref="IDrawPile{TElement}"/>.
        /// </summary>
        Internal.IDrawPileInternal<TElement> Internal.IDeckInternal<TElement>.DrawPileStack { get { return _drawPile; } }
        /// <summary>
        /// The internal representation of the <see cref="IDiscardPile{TElement}"/>.
        /// </summary>
        Internal.IDiscardPileInternal<TElement> Internal.IDeckInternal<TElement>.DiscardPileStack { get { return _discards; } }
        /// <summary>
        /// The internal representation of the <see cref="ITableau{TElement}"/>.
        /// </summary>
        Internal.ITableauInternal<TElement> Internal.IDeckInternal<TElement>.TableauStack { get { return _tableau; } }
        /// <summary>
        /// The internal representation of the <see cref="ITable{TElement}"/>.
        /// </summary>
        Internal.ITableInternal<TElement> Internal.IDeckInternal<TElement>.TableStack { get { return _table; } }

        /// <summary>
        /// The elements from this stacck that are sitting on the play table.
        /// </summary>
        public ITable<TElement> Table { get { return _table; } }
        /// <summary>
        /// The elements that have been discarded.
        /// </summary>
        public IDiscardPile<TElement> DiscardPile { get { return _discards; } }
        /// <summary>
        /// The elements that can be drawn from.
        /// </summary>
        public IDrawPile<TElement> DrawPile { get { return _drawPile; } }
        /// <summary>
        /// A palette of elements that are visible from this deck and can be drawn from.
        /// </summary>
        public ITableau<TElement> Tableau { get { return _tableau; } }

        /// <summary>
        /// The options for this deck.
        /// </summary>
        public IDeckOptions Options { get; }


        #region Counts
        /// <summary>
        /// The count of all elements from all different stacks.
        /// </summary>
        public int Count { get { return Known.Count; } }
        #endregion
        #endregion

        #region Public Interface

        /// <summary>
        /// The collection of events that are in use.
        /// </summary>
        IDeckEvents<TElement> IDeckInternal<TElement>.Events
        {
            get
            {
                return Events;
            }
        }
        /// <summary>
        /// The collection of events that are in use.
        /// </summary>
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
                        throw new InvalidCastException($"The deck events need to implement {nameof(IDeckEvents<TElement>)}, not the non-generic base.");
                    }
                    else
                    {
                        throw new InvalidCastException($"The deck events need to implement {nameof(IDeckEvents<TElement>)}, you provided {Options.Events.GetType().Name}.");
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
        public bool Contains(TElement element, Location location = Location.DrawPile)
        {
            Contract.Requires(Enum.IsDefined(typeof(Location), location));

            switch (location)
            {
                case Location.DiscardPile:
                    return DiscardPile.Contains(element);
                case Location.Hand:
                    return HandSet.Any(hand => hand.Contains(element));
                case Location.Table:
                    return Table.Contains(element);
                case Location.DrawPile:
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
        public IDeck<TElement> Add(TElement element, Location location = Location.DrawPile)
        {
            Contract.Requires(Enum.IsDefined(typeof(Location), location));
            CheckAllowAdd();

            Add(element, DeckSide.Default, location);
            return this;
        }
        /// <summary>
        /// Plays the top card from the deck to the table.
        /// </summary>
        /// <returns>The element played.</returns>
        public TElement Play()
        {
            ((Internal.ITableInternal<TElement>)_table).CheckEnabled();
            var card = ((Internal.IDrawPileInternal<TElement>)_drawPile).Draw();
            ((Internal.ITableInternal<TElement>)_table).Add(card);

            Events.Played(card);

            return card;
        }

        /// <summary>
        /// Adds a card to a specific location in the deck.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <param name="side">What side of the deck the item goes to.</param>
        /// <param name="location">The location to add it to.</param>
        /// <returns>This same deck (for FLUID interface reasons).</returns>
        public IDeck<TElement> Add(TElement element, DeckSide side, Location location = Location.DrawPile)
        {
            Contract.Requires(Enum.IsDefined(typeof(DeckSide), side));
            Contract.Requires(Enum.IsDefined(typeof(Location), location));
            CheckAllowAdd();

            switch (location)
            {
                case Location.DrawPile:
                    ((Internal.IDrawPileInternal<TElement>)_drawPile).Add(element, side);
                    break;
                case Location.DiscardPile:
                    ((Internal.IDiscardPileInternal<TElement>)_discards).Add(element, side);
                    break;
                case Location.Table:
                    if (side != DeckSide.Default)
                    {
                        throw new InvalidOperationException("Cannot add to the table on a specific side.");
                    }
                    else
                    {
                        ((Internal.ITableInternal<TElement>)_table).Add(element);
                    }
                    break;
                case Location.Tableau:
                    if (side != DeckSide.Default)
                    {
                        throw new InvalidOperationException("Cannot add to the tableau on a specific side.");
                    }
                    else
                    {
                        ((Internal.ITableauInternal<TElement>)_tableau).Add(element);
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

        /// <summary>
        /// Called before the deck is serialized.
        /// </summary>
        public virtual void Dehydrating() { }
        /// <summary>
        /// Called after the deck has ben deserialized.
        /// </summary>
        public virtual void Rehydrated()
        {
            Known.AddRange(DrawPile);
            Known.AddRange(DiscardPile);
            Known.AddRange(Table);
            Known.AddRange(Tableau);
            Hands.Apply(h => Known.AddRange(h));
        }

        /// <summary>
        /// The type of elements in this <see cref="IDeck{TElement}"/>.
        /// </summary>
        /// <remarks>This is very handy when dealing witih a derived (and no longer generic deck class.</remarks>
        Type IDeckInternal<TElement>.ElementType
        {
            get
            {
                return typeof(TElement);
            }
        }
        /// <summary>
        /// Accepts a visitor at the deck.
        /// </summary>
        /// <param name="visitor">The visitor to this deck.</param>
        void IDeckVisitable<TElement>.Accept(IDeckVisitor<TElement> visitor)
        {
            visitor.Visit(this);

            var me = (IDeckInternal<TElement>)this;

            visitor.Visit(me.DrawPileStack);
            visitor.Visit(me.DiscardPileStack);
            visitor.Visit(me.TableStack);
            visitor.Visit(me.TableauStack);
            visitor.Visit(Hands);
            foreach (IHandInternal<TElement> hand in Hands)
            {
                visitor.Visit(hand);
            }
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
        /// <summary>
        /// Checks to see if we can be allowed to add contents to this deck.
        /// </summary>
        private void CheckAllowAdd()
        {
            if (Initialized && !Options.Modifiable)
            {
                throw new InvalidOperationException("Cannot modify this deck directly, the deck has been initialized.");
            }
        }

        #endregion

    }
}
