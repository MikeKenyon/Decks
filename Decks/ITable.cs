using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    public interface ITable<TElement> : IDeckStack<TElement>
        where TElement : class
    {
        void Discard(TElement element);
        void Muck();
        bool Enabled { get; }
    }
}
