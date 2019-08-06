using Decks.Configuration;
using Decks.Internal;
using System;
using System.Diagnostics.Contracts;

namespace Decks
{
    /// <summary>
    /// A tableau is an area with face-up elements.  It is backfilled from the top-deck.
    /// </summary>
    /// <remarks>
    /// Generally, the tableau exists to give players an set of options that they can opt
    /// between.  Usually, you have a choice of either something in the tableau or the top
    /// of the draw pile.  Sometimes, you have to pull from the tableau.
    /// </remarks>
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

        /// <summary>
        /// The options for this tableau.
        /// </summary>
        ITableauOptions ITableau<TElement>.Options
        {
            get
            {
                return Deck.Options.Tableau;
            }
        }

        /// <summary>
        /// Whether or not this setup uses a tableau.
        /// </summary>
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
        /// <returns>This tableau (for fluent purposes).</returns>
        public ITableau<TElement> DrawUp(DeckSide from = DeckSide.Top)
        {
            Contract.Requires(Enum.IsDefined(typeof(TableauOverflowRule), Options.OverflowRule));
            ((IDeckStackInternal<TElement>)this).CheckEnabled();

            Deck.Events.DrawingInto(this);

            var size = Options.InitialSize;

            TElement element = null;

            while (Count < size &&
                (!Options.DrawsUpSafely ||
                (Deck.DrawPile.Count + Deck.DiscardPile.Count) > 0))
            {
                element = Deck.DrawPileStack.Draw(from);
                Contents.Add(element);

                Deck.Events.DrewInto(this, element);
            }
            if (Options.MaximumSize.HasValue)
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
                            return this; // Intentionally return from method, we do nothing here.
                        case TableauOverflowRule.Ask:
                            index = Deck.Events.PickElementToDiscard(this);
                            Contract.Assert(index >= 0 && index < Contents.Count);
                            break;
                        default:
                            throw new NotImplementedException($"Haven't coded for {Options.MaintainSize} yet.");
                    }
                    element = Contents[index];
                    Contents.RemoveAt(index);
                    Deck.DiscardPileStack.Add(element);
                }
            }

            return this;
        }
        /// <summary>
        /// Plays an element from this tableau to the table.
        /// </summary>
        /// <param name="element">The element to play to the table.</param>
        /// <returns>This tableau (for fluent purposes).</returns>
        public ITableau<TElement> Play(TElement element)
        {
            ((IDeckStackInternal<TElement>)this).CheckEnabled();

            Deck.Events.Playing(this, element);

            CheckOperation(Options.CanPlayToTable, "Playing to the table from the tableau is not allowed.");
            CheckIsMyElement(element, "Cannot play an element not in the tableau.");
            Contents.Remove(element);
            Deck.TableStack.Add(element);
            CheckProperSize();

            Deck.Events.Played(this, element);

            return this;
        }

        /// <summary>
        /// Has a specific hand draw up a given element.
        /// </summary>
        /// <param name="element">The element to play to the table.</param>
        /// <param name="hand">The hand to draw it into.</param>
        /// <returns>This tableau (for fluent purposes).</returns>
        public ITableau<TElement> DrawInto(TElement element, IHand<TElement> hand)
        {
            Contract.Requires(hand is Internal.IHandInternal<TElement>);
            ((IDeckStackInternal<TElement>)this).CheckEnabled();
            CheckOperation(Options.CanDrawIntoHand, "Drawing into a hand from the tableau is not allowed.");
            CheckIsMyElement(element, "Cannot play an element not in the tableau.");
            CheckOwnHand(hand);

            Deck.Events.DrawingInto(this, hand, element);

            var h = hand as Internal.IHandInternal<TElement>;

            Contents.Remove(element);
            h.Add(element);
            CheckProperSize();

            Deck.Events.DrewInto(this, hand, element);

            return this;
        }


        /// <summary>
        /// Checks that this tableau is of the proper size.
        /// </summary>
        private void CheckProperSize()
        {
            if (Options.MaintainSize)
            {
                DrawUp();
            }
        }

        /// <summary>
        /// Adds an element to this tableau.
        /// </summary>
        /// <param name="element">The element to add.</param>
        void IDeckStackInternal<TElement>.Add(TElement element)
        {
            Contents.Add(element);
        }
        /// <summary>
        /// Accepts a vistor at the tableau.
        /// </summary>
        /// <param name="visitor">The visitor to accept.</param>
        void IDeckVisitable<TElement>.Accept(IDeckVisitor<TElement> visitor)
        {
            visitor.Visit(this);
        }


        /// <summary>
        /// Checks to see if the tableau should be usable.
        /// </summary>
        void IDeckStackInternal<TElement>.CheckEnabled()
        {
            if (!Enabled)
            {
                throw new InvalidOperationException("The tableau has been disabled.");
            }
        }
    }
}
