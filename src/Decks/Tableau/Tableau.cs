using Decks.Configuration;
using Decks.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Decks
{
    internal class Tableau<TElement> : DeckStack<TElement>, ITableau<TElement>, Internal.ITableauInternal<TElement>
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
        /// The options for this tableau.
        /// </summary>
        public ITableauOptions Options { get { return Deck.Options.Tableau; } }
        /// <summary>
        /// Whether or not this setup uses a tableau.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return Options.Enabled;
            }
        }

        ITableauOptions ITableau<TElement>.Options
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ITableau<TElement>.Enabled
        {
            get
            {
                return Options.Enabled;
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
        public void DrawUp(DeckSide from = DeckSide.Top)
        {
            Contract.Requires(Enum.IsDefined(typeof(TableauOverflowRule),
                Options.OverflowRule));
            ((IDeckStackInternal<TElement>)this).CheckEnabled();

            var size = Options.InitialSize;

            while(Count < size && 
                (!Options.DrawsUpSafely || 
                (Deck.Count + Deck.DiscardPile.Count) > 0))
            {
                var card = Deck.DrawPileStack.Draw(from);
                Contents.Add(card);
            }
            if(Options.MaximumSize.HasValue)
            {
                var max = Options.MaximumSize.Value;
                while (Count > max)
                {
                    var index = 0;
                    switch (Options.OverflowRule)
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
                            throw new NotImplementedException($"Haven't coded for {Options.MaintainSize} yet.");
                    }
                    var element = Contents[index];
                    Contents.RemoveAt(index);
                    Deck.DiscardPileStack.Add(element);
                }
            }
        }
        /// <summary>
        /// Plays an element from this tableau to the table.
        /// </summary>
        /// <param name="element">The element to play to the table.</param>
        public void Play(TElement element)
        {
            ((IDeckStackInternal<TElement>)this).CheckEnabled();

            CheckOperation(Options.CanPlayToTable, "Playing to the table from the tableau is not allowed.");
            CheckIsMyElement(element, "Cannot play an element not in the tableau.");
            Contents.Remove(element);
            Deck.TableStack.Add(element);
            CheckProperSize();
        }

        /// <summary>
        /// Has a specific hand draw up a given element.
        /// </summary>
        /// <param name="element">The element to play to the table.</param>
        /// <param name="hand">The hand to draw it into.</param>
        public void DrawInto(TElement element, IHand<TElement> hand)
        {
            Contract.Requires(hand is Internal.IHandInternal<TElement>);
            ((IDeckStackInternal<TElement>)this).CheckEnabled();
            CheckOperation(Options.CanDrawIntoHand, "Drawing into a hand from the tableau is not allowed.");
            CheckIsMyElement(element, "Cannot play an element not in the tableau.");
            CheckOwnHand(hand);

            var h = hand as Internal.IHandInternal<TElement>;

            Contents.Remove(element);
            h.Add(element); 
            CheckProperSize();
        }



        private void CheckProperSize()
        {
            if (Options.MaintainSize)
            {
                DrawUp();
            }
        }

        void IDeckStackInternal<TElement>.Add(TElement element)
        {
            Contents.Add(element);
        }

        /// <summary>
        /// Checks to see if the tableau should be usable.
        /// </summary>
        /// <param name="overrideSize">If greater than 0, this is the size to use to determin
        /// enabled-ness.</param>
        void IDeckStackInternal<TElement>.CheckEnabled()
        {
            if (!Enabled)
            {
                throw new InvalidOperationException("The tableau has been disabled.");
            }
        }
    }
}
