using Decks.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
    public interface IDeck : INotifyPropertyChanged
    {
        /// <summary>
        /// The options that this deck is operating under.
        /// </summary>
        IDeckOptions Options { get; }

        /// <summary>
        /// The type of elements in this deck.
        /// </summary>
        /// <remarks>This is very handy when dealing witih a derived (and no longer generic deck class.</remarks>
        Type ElementType { get; }


        /// <summary>
        /// The total number of elements in the system.
        /// </summary>
        int Count { get; }
    }

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
    /// <typeparam name="TElement">The type of the elements in the deck.</typeparam>
    public interface IDeck<TElement> : IDeck where TElement : class
    {

        /// <summary>
        /// Determines if an area contains an element.
        /// </summary>
        /// <param name="element">The element to look for.</param>
        /// <param name="location">The location to check.</param>
        /// <returns><see langword="true"/> if the element is in that location.</returns>
        bool Contains(TElement element, Location location = Location.DrawPile);

        /// <summary>
        /// The draw pile for this deck.
        /// </summary>
        IDrawPile<TElement> DrawPile { get; }
        /// <summary>
        /// The tableau for this deck.
        /// </summary>
        ITableau<TElement> Tableau { get; }

        #region Adding
        /// <summary>
        /// Adds a card 
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <param name="location">The location to add it to.</param>
        /// <returns>This same deck (for FLUID interface reasons).</returns>
        /// <exception cref="System.InvalidOperationException">
        /// If you try to supply <see cref="Location.Hand"/> or you attempt to add to a deck that has been initialized 
        /// and declared to be unmodified by <see cref="IDeckOptions.Modifiable"/>.
        /// </exception>
        IDeck<TElement> Add(TElement element, Location location = Location.DrawPile);
        /// <summary>
        /// Adds a card to a specific location in the deck.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <param name="side">What side of the deck the item goes to.</param>
        /// <param name="location">The location to add it to.</param>
        /// <returns>This same deck (for FLUID interface reasons).</returns>
        /// <exception cref="System.InvalidOperationException">
        /// If the given <see cref="Location"/> cannot have an element added to the given <paramref name="side"/> or if you 
        /// try to supply <see cref="Location.Hand"/> or you attempt to add to a deck that has been initialized and declared to 
        /// be unmodified by <see cref="IDeckOptions.Modifiable"/>.
        /// </exception>
        IDeck<TElement> Add(TElement element, DeckSide side, Location location = Location.DrawPile);
        #endregion

        #region Hands
        /// <summary>
        /// Deals out a number of hands to their default handsize.
        /// </summary>
        /// <param name="numberOfHands">Number of hands to deal.</param>
        /// <returns>The dealt hands.</returns>
        IEnumerable<IHand<TElement>> Deal(int numberOfHands);
        /// <summary>
        /// Deals out a number of hands to their default 
        /// </summary>
        /// <param name="numberOfHands">Number of hands to deal.</param>
        /// <param name="handSize">Number of cards in the hand.</param>
        /// <returns>The dealt hands.</returns>
        IEnumerable<IHand<TElement>> Deal(int numberOfHands, uint handSize);
        /// <summary>
        /// Adds a hand, at its' default hand size.
        /// </summary>
        /// <returns>A newly drawn hand.</returns>
        IHand<TElement> Deal();
        /// <summary>
        /// Mucks all hands, putting them back into the discard pile.
        /// </summary>
        /// <returns>This deck (for fluent purposes) </returns>
        IDeck<TElement> Muck();
        /// <summary>
        /// All of the hands that are currently dealt.
        /// </summary>
        ReadOnlyObservableCollection<IHand<TElement>> Hands { get; }
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

        #region Discards
        /// <summary>
        /// The discard pile.
        /// </summary>
        IDiscardPile<TElement> DiscardPile { get; }
        #endregion
    }
}
