using Decks.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Decks
{
    public partial class Deck<TElement> : IEnumerable<TElement> where TElement : class
    {
        #region Data
        private bool Initialized { get; set; }
        private List<TElement> Known { get; } = new List<TElement>();
        private DrawPile<TElement> DrawPileStack { get; }
        internal DiscardPile<TElement> DiscardPileStack { get; }
        internal Tableau<TElement> TableauStack { get; }
        #endregion

        #region Construction
        public Deck(IDeckOptions options)
        {
            Options = options;
            Hands = new ReadOnlyCollection<IHand<TElement>>(HandSet);
            DrawPileStack = new DrawPile<TElement>(this);
            DiscardPileStack = new DiscardPile<TElement>(this);
            TableStack = new Table<TElement>(this);
            TableauStack = new Tableau<TElement>(this);

            Initialize();
            Initialized = true;
        }
        #endregion

        #region Properties
        public IDeckOptions Options { get; }
        public ITableau<TElement> Tableau { get { return TableauStack; } }


        #region Counts
        public int Count { get { return DrawPileStack.Count; } }
        public int TotalCount { get { return Known.Count; } }
        #endregion
        #endregion

        #region Public Interface
        public IDiscardPile<TElement> DiscardPile { get { return DiscardPileStack; } }
        public IDrawPile<TElement> DrawPile { get { return DrawPileStack; } }
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
            switch (location)
            {
                case Location.TopDeck:
                    DrawPileStack.Contents.Insert(0, element);
                    break;
                case Location.DiscardPile:
                    DiscardPileStack.Contents.Insert(0, element);
                    break;
                case Location.Table:
                    TableStack.EnabledCheck();
                    TableStack.Contents.Add(element);
                    break;
                case Location.Tableau:
                    TableauStack.CheckEnabled();
                    TableauStack.Contents.Add(element);
                    break;
                case Location.Hand:
                    throw new InvalidOperationException("Cannot add directly to a hand.");
            }
            Known.Add(element);
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

            switch (location)
            {
                case Location.TopDeck:
                    switch (side)
                    {
                        case DeckSide.Top:
                            DrawPileStack.Contents.Insert(0, element);
                            break;
                        case DeckSide.Bottom:
                            DrawPileStack.Contents.Add(element);
                            break;
                    }
                    break;
                case Location.DiscardPile:
                    switch (side)
                    {
                        case DeckSide.Top:
                            DiscardPileStack.Contents.Insert(0, element);
                            break;
                        case DeckSide.Bottom:
                            DiscardPileStack.Contents.Add(element);
                            break;
                    }
                    break;
                case Location.Table:
                    throw new InvalidOperationException("Cannot add to the table on a specific side.");
                case Location.Tableau:
                    throw new InvalidOperationException("Cannot add to the tableau on a specific side.");
                case Location.Hand:
                    throw new InvalidOperationException("Cannot add directly to a hand.");
                default:
                    throw new NotImplementedException($"Don't know about the {location} location.");
            }
            Known.Add(element);
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
        protected void MissingOperation(ValidOperations validOperations)
        {
            throw new InvalidOperationException($"Can not perform action, deck is not allowed {validOperations}.");
        }
        protected internal void CheckOperation(ValidOperations operation)
        {
            if (Initialized && !Options.Allow.HasFlag(operation))
            {
                MissingOperation(operation);
            }
        }
        /// <summary>
        /// Checks to see if an operation is allowed or not.
        /// </summary>
        /// <param name="condition">Condition to check.</param>
        /// <param name="message">The error message if it's not allowed.</param>
        protected void CheckOperation(bool condition, string message)
        {
            if (!condition)
            {
                throw new InvalidOperationException(message);
            }
        }
        #endregion

        #region Private Helpers
        #endregion

        #region IEnumerable
        public IEnumerator<TElement> GetEnumerator()
        {
            return DrawPileStack.Contents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return DrawPileStack.Contents.GetEnumerator();
        }
        #endregion
    }
}
