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
        public void Discard(TElement element)
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
        }
        public void Muck()
        {
            Deck.Events.Mucking(this);

            ((IDeckStackInternal<TElement>)this).CheckEnabled();
            Contents.Apply(c => Deck.DiscardPileStack.Add(c));
            Contents.Clear();

            Deck.Events.Mucked(this);
        }
        public bool Enabled {
            get {
                return Options.Enabled;
            }
        }

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
