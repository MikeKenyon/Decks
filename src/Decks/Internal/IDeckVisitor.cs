using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Internal
{
    internal interface IDeckVisitor<TElement> where TElement : class
    {
        void Visit(IDeckInternal<TElement> deck);

        void Visit(IDrawPileInternal<TElement> drawPile);

        void Visit(IDiscardPileInternal<TElement> discards);

        void Visit(ITableInternal<TElement> table);

        void Visit(ITableauInternal<TElement> tableau);

        void Visit(IReadOnlyCollection<IHand<TElement>> hands);

        void Visit(IHandInternal<TElement> element);
    }
}
