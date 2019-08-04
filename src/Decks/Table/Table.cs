using Decks.Configuration;
using Decks.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    internal class Table<TElement> : DeckStack<TElement>, ITable<TElement>, Internal.ITableInternal<TElement>
        where TElement : class
    {
        public Table(Deck<TElement> deck) : base(deck)
        {
        }
        /// <summary>
        /// Discards a single element from the table.
        /// </summary>
        /// <param name="element">The element to discard.</param>
        /// <returns>The table (for fluent purposes).</returns>
        public ITable<TElement> Discard(TElement element)
        {
            ((IDeckStackInternal<TElement>)this).CheckEnabled();
            if (!Contains(element))
            {
                throw new InvalidElementException("Element is not in play.");
            }

            Deck.Events.Discarding(this, element);

            Deck.DiscardPileStack.Add(element);
            Contents.Remove(element);

            Deck.Events.Discarded(this, element);

            return this;
        }
        /// <summary>
        /// Discards all elements from the table.  Unlike a <see cref="IHand{TElement}"/>, the table persists 
        /// after this operation.
        /// </summary>
        /// <returns>The table (for fluent purposes).</returns>
        public ITable<TElement> Muck()
        {
            Deck.Events.Mucking(this);

            ((IDeckStackInternal<TElement>)this).CheckEnabled();
            Contents.Apply(c => Deck.DiscardPileStack.Add(c));
            Contents.Clear();

            Deck.Events.Mucked(this);

            return this;
        }
        /// <summary>
        /// Checks to see if the table is enabled (from <see cref="Options"/>).
        /// </summary>
        public bool Enabled {
            get {
                return Options.Enabled;
            }
        }

        /// <summary>
        /// The options pertainent to this table.
        /// </summary>
        public ITableOptions Options
        {
            get
            {
                return Deck.Options.Table;
            }
        }

        void IDeckVisitable<TElement>.Accept(IDeckVisitor<TElement> visitor)
        {
            visitor.Visit(this);
        }

        void IDeckStackInternal<TElement>.Add(TElement element)
        {
            Contents.Add(element);
        }

        void IDeckStackInternal<TElement>.CheckEnabled()
        {
            if (!Enabled)
            {
                throw new InvalidOperationException("Play table isn't enabled.");
            }
        }
    }
}
