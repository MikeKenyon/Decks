using Decks.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    internal class Table<TElement> : DeckStack<TElement>, ITable<TElement>
        where TElement : class
    {
        public Table(Deck<TElement> deck) : base(deck)
        {
        }
        public void Discard(TElement element)
        {
            EnabledCheck();
            if (Contains(element))
            {
                Deck.DiscardPileStack.Contents.Add(element);
                Contents.Remove(element);
            }
            else
            {
                throw new InvalidElementException("Element is not in play.");
            }
        }
        public void Muck()
        {
            EnabledCheck();
            Deck.DiscardPileStack.Contents.AddRange(Contents);
            Contents.Clear();
        }
        public bool Enabled {
            get {
                return Deck.Options.Allow.HasFlag(ValidOperations.PlayToTable);
            }
        }
        internal void EnabledCheck()
        {
            if(!Enabled)
            {
                throw new InvalidOperationException("Play table isn't enabled.");
            }
        }
    }
}
