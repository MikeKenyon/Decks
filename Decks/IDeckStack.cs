using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    public interface IDeckStack<TElement> : IEnumerable<TElement> where TElement : class
    {
        /// <summary>
        /// The number of elements in this location.
        /// </summary>
        int Count { get; }
        /// <summary>
        /// Checks if this hand contains that element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        bool Contains(TElement element);
    }
}
