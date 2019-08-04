using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Decks
{
    public interface IDeckStack<TElement> : IReadOnlyCollection<TElement>, INotifyPropertyChanged, INotifyCollectionChanged 
        where TElement : class
    {
        /// <summary>
        /// Checks if this hand contains that element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        bool Contains(TElement element);
    }
}
