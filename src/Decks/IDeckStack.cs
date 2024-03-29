﻿using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Decks
{
    /// <summary>
    /// A <see cref="IDeck{TElement}"/> is comprised of several different stacks - the one you draw from, the one to discard to,
    /// the table itself, etc.  Each is represented by a stack.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
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
