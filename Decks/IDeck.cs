using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    /// <summary>
    /// A "deck" of game components.  They can be cards, tiles, chits, whatever.  Uniqueness is not
    /// guaranteed.  
    /// </summary>
    /// <remarks>
    /// <para>
    /// Items are drawn from the "top deck" and returned to the "discard pile".  Each 
    /// player gets a "hand" and when you draw, you draw into your hand and when you play from your
    /// hand it goes automatically to the discard pile.  Elements are always in either someone's hand,
    /// the top deck, the discard pile or possibly in play on the "table". 
    /// </para>
    /// <para>
    /// When creating a deck, you provide rules that explain how the system should react to specific 
    /// circumstances through the <see cref="DeckOptions"/> class.
    /// </para>
    /// </remarks>
    /// <typeparam name="TElement"></typeparam>
    public interface IDeck<TElement> : IEnumerable<TElement> where TElement : class
    {
        /// <summary>
        /// The options that this deck is operating under.
        /// </summary>
        IDeckOptions Options { get; }

        /// <summary>
        /// The cards in the top-deck.
        /// </summary>
        int Count { get; }
        /// <summary>
        /// The cards in the discard pile.
        /// </summary>
        int DiscardCount { get; }
        /// <summary>
        /// The total cards in the system.
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Optionally gets the discards back and then randomly orders the cards.
        /// </summary>
        /// <param name="retreiveDiscards">Whether or not to clear out the discards.</param>
        void Shuffle(bool retreiveDiscards = true);
        /// <summary>
        /// Determines if an area contains an element.
        /// </summary>
        /// <param name="element">The element to look for.</param>
        /// <param name="location">The location to check.</param>
        /// <returns><see langword="true"/> if the element is in that location.</returns>
        bool Contains(TElement element, Location location = Location.TopDeck);

        #region Adding
        /// <summary>
        /// Adds a card 
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <param name="location">The location to add it to.</param>
        void Add(TElement element, Location location = Location.TopDeck);
        /// <summary>
        /// Adds a card to a specific location in the deck.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <param name="side">What side of the deck the item goes to.</param>
        /// <param name="location">The location to add it to.</param>
        void Add(TElement element, DeckSide side, Location location = Location.TopDeck);
        #endregion

        #region Hands
        /// <summary>
        /// Deals out a number of hands to their default handsize.
        /// </summary>
        /// <param name="numberOfHands">Number of hands to deal.</param>
        /// <returns></returns>
        IEnumerable<IHand<TElement>> Deal(int numberOfHands);
        /// <summary>
        /// Deals out a number of hands to their default 
        /// </summary>
        /// <param name="numberOfHands">Number of hands to deal.</param>
        /// <param name="handSize">Number of cards in the hand.</param>
        /// <returns></returns>
        IEnumerable<IHand<TElement>> Deal(int numberOfHands, uint handSize);
        /// <summary>
        /// Adds a hand, at its' default hand size.
        /// </summary>
        /// <returns>A newly drawn hand.</returns>
        IHand<TElement> Deal();
        /// <summary>
        /// Mucks all hands, putting them back into the discard pile.
        /// </summary>
        void Muck();
        /// <summary>
        /// Mucks a specific hand, putting it into the discard pile.
        /// </summary>
        /// <param name="hand">The hand to muck.</param>
        void Muck(IHand<TElement> hand);
        /// <summary>
        /// All of the hands that are currently dealt.
        /// </summary>
        IReadOnlyCollection<IHand<TElement>> Hands { get;}
        #endregion

        #region Table
        /// <summary>
        /// Gets the tabled portion of the deck, those cards currently "in play".
        /// </summary>
        ITable<TElement> Table { get; }
        /// <summary>
        /// Plays the top card from the deck to the table.
        /// </summary>
        /// <returns>The element played.</returns>
        TElement Play();
        #endregion
    }
}
