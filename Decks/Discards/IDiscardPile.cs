using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    public interface IDiscardPile<TElement> : IDeckStack<TElement>
        where TElement : class
    {
    }
}
