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
            if (Contains(element))
            {
                Deck.DiscardPileStack.Add(element);
                Contents.Remove(element);
            }
            else
            {
                throw new InvalidElementException("Element is not in play.");
            }
        }
        public void Muck()
        {
            ((IDeckStackInternal<TElement>)this).CheckEnabled();
            Contents.Apply(c => Deck.DiscardPileStack.Add(c));
            Contents.Clear();
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
