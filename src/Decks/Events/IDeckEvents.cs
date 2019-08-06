using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Events
{
    /// <summary>
    /// Events and interactions about a <see cref="IDeck{TElement}"/>.
    /// </summary>
    public interface IDeckEvents
    {
    }
    /// <summary>
    /// Element specific events.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public interface IDeckEvents<TElement> : IDeckEvents where TElement : class
    {
        /// <summary>
        /// Notes that we're about to add an element to a stack.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <param name="side">The side of the stack it's getting added to.</param>
        /// <param name="location">The stack it's being sent to.</param>
        void Adding(ref TElement element, ref DeckSide side, ref Location location);
        /// <summary>
        /// An element has been added to a stack.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <param name="side">The side of the stack it's getting added to.</param>
        /// <param name="location">The stack it's being sent to.</param>
        void Added(TElement element, DeckSide side, Location location);

        /// <summary>
        /// One or more hands are being dealt, of a given size.
        /// </summary>
        /// <param name="numberOfHands">The number of hands being dealth.</param>
        /// <param name="handSize">The size of each hand.</param>
        void Dealing(ref int numberOfHands, ref uint handSize);
        /// <summary>
        /// Hands have been dealt.
        /// </summary>
        /// <param name="numberOfHands">The number of hands having been dealth.</param>
        /// <param name="handSize">The size of each hand.</param>
        void Dealt(int numberOfHands, uint handSize);

        /// <summary>
        /// An element is being discarded from the table.
        /// </summary>
        /// <param name="table">The table being discarded from.</param>
        /// <param name="element">The element being discareded.</param>
        void Discarding(ITable<TElement> table, TElement element);
        /// <summary>
        /// An element has been discarded.
        /// </summary>
        /// <param name="table">The table discarded from.</param>
        /// <param name="element">The element discareded.</param>
        void Discarded(ITable<TElement> table, TElement element);

        /// <summary>
        /// A card is about to be drawn from the draw pile.
        /// </summary>
        /// <param name="pile">The pile to draw from.</param>
        void Drawing(IDrawPile<TElement> pile);
        /// <summary>
        /// A card was drawn from the draw pile.
        /// </summary>
        /// <param name="pile">The pile drawn from.</param>
        /// <param name="card">The card drawn.</param>
        void Drew(IDrawPile<TElement> pile, TElement card);

        /// <summary>
        /// About to draw into a hand.
        /// </summary>
        /// <param name="hand">The hand we're drawing into.</param>
        void DrawingInto(IHand<TElement> hand);
        /// <summary>
        /// About to draw into the tableau.
        /// </summary>
        /// <param name="tableau">The tableau to add to.</param>
        void DrawingInto(ITableau<TElement> tableau);
        /// <summary>
        /// About to draw from the tableau into a hand.
        /// </summary>
        /// <param name="tableau">The tableau to draw from.</param>
        /// <param name="hand">The hand to draw into.</param>
        /// <param name="element">The element to draw.</param>
        void DrawingInto(ITableau<TElement> tableau, IHand<TElement> hand, TElement element);
        /// <summary>
        /// An element has been drawn into a tableau.
        /// </summary>
        /// <param name="tableau">The tableau drawn into.</param>
        /// <param name="element">The element drawn.</param>
        void DrewInto(ITableau<TElement> tableau, TElement element);
        /// <summary>
        /// An element is being drawn into a hand.
        /// </summary>
        /// <param name="hand">The hand drawn into.</param>
        /// <param name="element">The element drawn into it.</param>
        void DrewInto(IHand<TElement> hand, TElement element);
        /// <summary>
        /// An element is drawn from a tableau into a hand.
        /// </summary>
        /// <param name="tableau">The tableau drawn from.</param>
        /// <param name="hand">The hand drawn into.</param>
        /// <param name="element">The element to draw.</param>
        void DrewInto(ITableau<TElement> tableau, IHand<TElement> hand, TElement element);

        /// <summary>
        /// The contents of the table are about to be mucked.
        /// </summary>
        /// <param name="table">The table to be mucked.</param>
        void Mucking(ITable<TElement> table);
        /// <summary>
        /// A hand is about to be mucked.
        /// </summary>
        /// <param name="hand">The hand to be mucked.</param>
        void Mucking(IHand<TElement> hand);
        /// <summary>
        /// A hand was mucked.
        /// </summary>
        /// <param name="hand">The hand was mucked.</param>
        void Mucked(IHand<TElement> hand);
        /// <summary>
        /// The table was mucked.
        /// </summary>
        /// <param name="table">The table that was mucked.</param>
        void Mucked(ITable<TElement> table);

        /// <summary>
        /// Asks to pick the element to discard from a tableau.
        /// </summary>
        /// <param name="tableau">The tableau to discard from.</param>
        /// <returns>The index in the tableau to discard.</returns>
        int PickElementToDiscard(ITableau<TElement> tableau);

        /// <summary>
        /// About to play an element from a hand onto the table.
        /// </summary>
        /// <param name="hand">The hand to play from.</param>
        /// <param name="element">The element to play.</param>
        void Playing(IHand<TElement> hand, TElement element);
        /// <summary>
        /// About to play an element from the tableau onto the table.
        /// </summary>
        /// <param name="tableau">The tableau to play from.</param>
        /// <param name="element">The element to play.</param>
        void Playing(ITableau<TElement> tableau, TElement element);
        /// <summary>
        /// An element has been played to the table.
        /// </summary>
        /// <param name="element">The element played</param>
        void Played(TElement element);
        /// <summary>
        /// An element has been played from a hand.
        /// </summary>
        /// <param name="hand">The hand it's been played from.</param>
        /// <param name="element">The element played</param>
        void Played(IHand<TElement> hand, TElement element);
        /// <summary>
        /// And element has been played from a tableau.
        /// </summary>
        /// <param name="tableau">The tableau the element has been played from.</param>
        /// <param name="element">The element played</param>
        void Played(ITableau<TElement> tableau, TElement element);

        /// <summary>
        /// The discards are about to restored to the draw pile.
        /// </summary>
        void PuttingDiscardsBackIntoDrawPile();
        /// <summary>
        /// The discards have just been restored to the draw pile.
        /// </summary>
        void PutDiscardsBackIntoDrawPile();

        /// <summary>
        /// About to shuffle.
        /// </summary>
        /// <param name="shuffleCount">How many times this will have been shuffled.</param>
        void Shuffling(uint shuffleCount);
        /// <summary>
        /// Have just shuffled.
        /// </summary>
        /// <param name="shuffleCount">The number of times the deck has been shuffled.</param>
        void Shuffled(uint shuffleCount);
    }
}
