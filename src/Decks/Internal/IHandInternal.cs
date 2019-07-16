using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Internal
{
    /// <summary>
    ///  The internal interface to a specific player hand.
    /// </summary>
    /// <typeparam name="TElement">The elements in that hand.</typeparam>
    internal interface IHandInternal<TElement> : IDeckStackInternal<TElement>, IHand<TElement> where TElement : class
    {
    }
}
