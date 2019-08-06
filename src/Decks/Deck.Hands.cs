using Decks.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Decks
{
    // Portion of the Deck that deals with hands.
    public partial class Deck<TElement> : IDeck<TElement> where TElement : class
    {
        /// <summary>
        /// The set of <see cref="IHand{TElement}"/> that have been drawn from this deck.
        /// </summary>
        private ObservableCollection<IHand<TElement>> HandSet { get; } = new ObservableCollection<IHand<TElement>>();
        /// <summary>
        /// All of the hands that are currently dealt.
        /// </summary>
        public ReadOnlyObservableCollection<IHand<TElement>> Hands { get; }

        /// <summary>
        /// Deals out a number of hands to their default handsize.
        /// </summary>
        /// <param name="numberOfHands">Number of hands to deal.</param>
        /// <returns></returns>
        public IEnumerable<IHand<TElement>> Deal(int numberOfHands)
        {
            return Deal(numberOfHands, Options.Hands.InitialHandSize);
        }
        /// <summary>
        /// Adds a hand, at its' default hand size.
        /// </summary>
        /// <returns>A newly drawn hand.</returns>
        public IHand<TElement> Deal()
        {
            return Deal(1, Options.Hands.InitialHandSize).First();
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
            CheckOperation(Options.Hands.Enabled, "Hands are not a part of this deck.");

            Events.Dealing(ref numberOfHands, ref handSize);
            var hands = new Internal.IHandInternal<TElement>[numberOfHands];
            for (int i = 0; i < hands.Length; ++i)
            {
                hands[i] = new Hand<TElement>(this);
            }
            for (int card = 0; card < handSize; ++card)
            {
                for (int deck = 0; deck < hands.Length; ++deck)
                {
                    hands[deck].Add(((Internal.IDrawPileInternal<TElement>)this._drawPile).Draw());
                }
            }
            HandSet.AddRange(hands);

            Events.Dealt(numberOfHands, handSize);

            return hands;
        }
        /// <summary>
        /// Mucks all hands, putting them back into the discard pile.
        /// </summary>
        /// <returns>This deck (for fluent purposes) </returns>
        public IDeck<TElement> Muck()
        {
            var hands = HandSet.ToArray();
            hands.Apply(h => h.Muck());

            return this;
        }

        /// <summary>
        /// Removes a hand from those that the deck is aware of.
        /// </summary>
        /// <param name="hand">The hand to remove.</param>
        void Internal.IDeckInternal<TElement>.RemoveHand(IHand<TElement> hand)
        {
            HandSet.Remove(hand);
        }
    }
}
