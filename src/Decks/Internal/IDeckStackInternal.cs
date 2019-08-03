using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Internal
{
    /// <summary>
    /// The internal interface for dealing with any deck stack (draw pile, discards, table, etc.)
    /// </summary>
    /// <typeparam name="TElement">Type of the elements involved.</typeparam>
    internal interface IDeckStackInternal<TElement> : IDeckStack<TElement>, IDeckVisitable<TElement> where TElement : class
    {
        /// <summary>
        /// Adds an eleemnt from one area to another.
        /// </summary>
        /// <param name="element">The element to add.</param>
        void Add(TElement element);

        /// <summary>
        /// Confirms that this stack is enabled, errors if it isn't.
        /// </summary>
        /// <exception cref="InvalidOperationException">The stack is not enabled.</exception>
        void CheckEnabled();
    }
}
