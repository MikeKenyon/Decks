using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    internal class Hand<TElement> : IHand<TElement> where TElement : class
    {
        internal Hand(Deck<TElement> deck)
        {
            Deck = deck;
        }
        public int Count { get { return Contents.Count; } }
        private Deck<TElement> Deck { get; }
        internal List<TElement> Contents { get; } = new List<TElement>();
        internal bool Invalidated { get; set; }
        public IEnumerator<TElement> GetEnumerator()
        {
            InvalidCheck();
            return Contents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            InvalidCheck();
            return Contents.GetEnumerator();
        }

        private void InvalidCheck()
        {
            if(Invalidated)
            {
                throw new ObjectDisposedException("Hand");
            }
        }
    }
}
