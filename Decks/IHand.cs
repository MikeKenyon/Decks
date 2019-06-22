﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    public interface IHand<TElement> : IDeckStack<TElement> where TElement : class
    {

        void Draw(DeckSide from = DeckSide.Top);

        void Muck();
        bool HasBeenMucked { get; }

        /// <summary>
        /// Plays this element onto the table.
        /// </summary>
        /// <param name="element">The element to play.</param>
        /// <exception cref="InvalidElementException">
        /// The element isn't part of this hand.
        /// </exception>
        void Play(TElement element);
    }
}
