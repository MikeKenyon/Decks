using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Internal
{
    /// <summary>
    /// The internal interface to the table.
    /// </summary>
    /// <typeparam name="TElement">The elements on the table.</typeparam>
    internal interface ITableInternal<TElement> : IDeckStackInternal<TElement>, ITable<TElement> where TElement : class
    {
    }
}
