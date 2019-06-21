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
        public bool HasBeenMucked { get; internal set; }

        public void Draw(DeckSide side = DeckSide.Top)
        {
            var card = Deck.Draw(side);
            Contents.Add(card);
        }
        public void Muck()
        {
            Deck.Muck(this);
        }
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
            if(HasBeenMucked)
            {
                throw new ObjectDisposedException("Hand");
            }
        }
    }
}
