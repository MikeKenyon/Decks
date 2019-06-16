using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    public interface IHand<TElement> : IEnumerable<TElement> where TElement : class
    {
        int Count { get; }
    }
}
