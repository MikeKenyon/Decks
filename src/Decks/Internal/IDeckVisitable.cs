using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Internal
{
    /// <summary>
    /// An element that knows how to accept a <see cref="IDeckVisitor{TElement}"/>.  An implementation of the
    /// Visitor pattern.
    /// </summary>
    /// <typeparam name="TElement">The elements of the deck.</typeparam>
    internal interface IDeckVisitable<TElement> where TElement : class
    {
        void Accept(IDeckVisitor<TElement> visitor);
    }
}
