﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    internal class Table<TElement> : DeckStack<TElement>, ITable<TElement>
        where TElement : class
    {
        public Table(Deck<TElement> deck)
        {
            Deck = deck;
        }
        public void Discard(TElement element)
        {
            EnabledCheck();
            if (Contains(element))
            {
                Deck.Discards.Contents.Add(element);
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
            Deck.Discards.Contents.AddRange(Contents);
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
        private Deck<TElement> Deck { get; }
    }
}