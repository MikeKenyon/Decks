﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    public interface IDeckStack<TElement> : IReadOnlyCollection<TElement> where TElement : class
    {
        /// <summary>
        /// Checks if this hand contains that element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        bool Contains(TElement element);
    }
}