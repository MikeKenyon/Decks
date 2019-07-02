using Decks.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    public interface ITable<TElement> : IDeckStack<TElement>
        where TElement : class
    {
        ITableOptions Options { get; }
        void Discard(TElement element);
        void Muck();
        bool Enabled { get; }
    }
}
