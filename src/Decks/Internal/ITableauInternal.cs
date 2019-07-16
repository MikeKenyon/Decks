using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Internal
{
    /// <summary>
    /// The internal interface to the tableau.
    /// </summary>
    /// <typeparam name="TElement">The elements that comprise the tableau.</typeparam>
    internal interface ITableauInternal<TElement> : IDeckStackInternal<TElement>, ITableau<TElement> where TElement : class
    {
    }
}
