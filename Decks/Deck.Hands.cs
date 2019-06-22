using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Decks
{
    // Portion of the Deck that deals with hands.
    public partial class Deck<TElement> : IDeck<TElement> where TElement : class
    {
        private List<IHand<TElement>> DealtHands { get; } = new List<IHand<TElement>>();
        /// <summary>
        /// All of the hands that are currently dealt.
        /// </summary>
        public IReadOnlyCollection<IHand<TElement>> Hands { get; }

        /// <summary>
        /// Deals out a number of hands to their default handsize.
        /// </summary>
        /// <param name="numberOfHands">Number of hands to deal.</param>
        /// <returns></returns>
        public IEnumerable<IHand<TElement>> Deal(int numberOfHands)
        {
            return Deal(numberOfHands, Options.HandSize);
        }
        /// <summary>
        /// Adds a hand, at its' default hand size.
        /// </summary>
        /// <returns>A newly drawn hand.</returns>
        public IHand<TElement> Deal()
        {
            return Deal(1, Options.HandSize).First();
        }
        /// <summary>
        /// Deals out a number of hands to their default 
        /// </summary>
        /// <param name="numberOfHands">Number of hands to deal.</param>
        /// <param name="handSize">Number of cards in the hand.</param>
        /// <returns></returns>
        public IEnumerable<IHand<TElement>> Deal(int numberOfHands, uint handSize)
        {
            Contract.Requires(handSize > 0);

            Check(ValidOperations.DrawMuck);
            var hands = new Hand<TElement>[numberOfHands];
            for (int i = 0; i < hands.Length; ++i)
            {
                hands[i] = new Hand<TElement>(this);
            }
            for (int card = 0; card < handSize; ++card)
            {
                for (int deck = 0; deck < hands.Length; ++deck)
                {
                    hands[deck].Contents.Add(Draw());
                }
            }
            DealtHands.AddRange(hands);
            return hands;
        }
        /// <summary>
        /// Mucks all hands, putting them back into the discard pile.
        /// </summary>
        public void Muck()
        {
            var hands = DealtHands.ToArray();
            hands.Apply(h => Muck(h));
        }
        /// <summary>
        /// Mucks a specific hand, putting it into the discard pile.
        /// </summary>
        /// <param name="hand">The hand to muck.</param>
        public void Muck(IHand<TElement> hand)
        {
            Contract.Requires(Hands.Contains(hand), "The hand provided is not dealt from this deck.");
            Contract.Requires(hand is Hand<TElement> h);
            Contract.Requires(!h.HasBeenMucked);

            h = (Hand<TElement>)hand;
            while(h.Contents.Count > 0)
            {
                var card = h.Contents[0];
                h.Contents.RemoveAt(0);
                DiscardPile.Add(card);
            }
            h.HasBeenMucked = true;
            DealtHands.Remove(h);
        }

        protected internal TElement Draw(DeckSide side = DeckSide.Top)
        {
            Contract.Requires(Enum.IsDefined(typeof(DeckSide), side));

            if((Count == 0 && DiscardCount == 0) ||
                (Count == 0 && !Options.AutoShuffle))
            {
                throw new BottomDeckException();
            }
            else if(Count == 0) // Must be auto-shuffle with a discard.
            {
                Shuffle();
            }

            TElement card = null;
            switch(side)
            {
                case DeckSide.Bottom:
                    card = TopDeck.Last();
                    TopDeck.RemoveAt(TopDeck.Count - 1);
                    break;
                case DeckSide.Top:
                    card = TopDeck[0];
                    TopDeck.RemoveAt(0);
                    break;
            }
            return card;
        }
    }
}
