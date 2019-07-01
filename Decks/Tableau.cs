using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Decks
{
    internal class Tableau<TElement> : DeckStack<TElement>, ITableau<TElement>
        where TElement : class
    {
        /// <summary>
        /// Creates a tableau.
        /// </summary>
        /// <param name="deck">The deck we're part of.</param>
        internal Tableau(Deck<TElement> deck) : base(deck)
        {
        }
        /// <summary>
        /// Whether or not this setup uses a tableau.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return Deck.Options.TableauSize > 0;
            }
        }
        /// <summary>
        /// Draws the tableau up to its configured size.
        /// </summary>
        /// <remarks>
        /// This uses the <see cref="TableauOverflowRule"/> set in the options to 
        /// explain how to handle overage.  Underage is handled by drawing from 
        /// the top deck.
        /// </remarks>
        /// <param name="from">Which side of the draw pile we're drawing from.</param>
        /// <param name="overrideSize">
        /// If set to something greater than zero, it ignores the configured size of the
        /// Tableau and adjusts it to the size given here.
        /// </param>
        public void DrawUp(DeckSide from = DeckSide.Top, uint overrideSize = 0)
        {
            Contract.Requires(Enum.IsDefined(typeof(TableauOverflowRule),
                Deck.Options.TableauOverflow));
            EnabledCheck(overrideSize);

            var size = overrideSize == 0 ? Deck.Options.TableauSize : overrideSize;

            while(Count < size)
            {
                var card = Deck.Draw(from);
                Contents.Add(card);
            }
            while (Count > size)
            {
                var index = 0;
                switch(Deck.Options.TableauOverflow)
                {
                    case TableauOverflowRule.DiscardNewest:
                        index = Count - 1;
                        break;
                    case TableauOverflowRule.DiscardOldest:
                        index = 0;
                        break;
                    case TableauOverflowRule.DiscardRandom:
                        index = Extensions.Rand.Next(0, Count);
                        break;
                    case TableauOverflowRule.Ignore:
                        return; // Intentionally return from method, we do nothing here.
                    default:
                        throw new NotImplementedException($"Haven't coded for {Deck.Options.TableauMaintainSize} yet.");
                }
                var element = Contents[index];
                Contents.RemoveAt(index);
                Deck.Discards.Contents.Add(element);
            }
        }
        /// <summary>
        /// Plays an element from this tableau to the table.
        /// </summary>
        /// <param name="element">The element to play to the table.</param>
        public void Play(TElement element)
        {
            MyElementCheck(element);
            Contents.Remove(element);
            Deck.InPlay.Contents.Add(element);
            MaintainCheck();
        }

        /// <summary>
        /// Has a specific hand draw up a given element.
        /// </summary>
        /// <param name="element">The element to play to the table.</param>
        /// <param name="hand">The hand to draw it into.</param>
        public void DrawInto(TElement element, IHand<TElement> hand)
        {
            MyElementCheck(element);
            HandCheck(hand);
            Contents.Remove(element);
            ((Hand<TElement>)hand).Contents.Add(element); //TODO: Elegance?
            MaintainCheck();
        }


        /// <summary>
        /// Checks to see if the tableau should be usable.
        /// </summary>
        /// <param name="overrideSize">If greater than 0, this is the size to use to determin
        /// enabled-ness.</param>
        internal void EnabledCheck(uint overrideSize = 0)
        {
            if (!Enabled && overrideSize == 0)
            {
                throw new InvalidOperationException("The tableau has been disabled.");
            }
        }

        private void MaintainCheck()
        {
            if (Deck.Options.TableauMaintainSize)
            {
                DrawUp();
            }
        }

    }
}
