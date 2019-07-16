using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Internal
{
    internal interface IDeckInternal<TElement> : IDeck<TElement> where TElement : class
    {
        Internal.IDrawPileInternal<TElement> DrawPileStack { get; }
        Internal.IDiscardPileInternal<TElement> DiscardPileStack { get; }
        Internal.ITableauInternal<TElement> TableauStack { get; }
        Internal.ITableInternal<TElement> TableStack { get; }

        void RemoveHand(IHand<TElement> hand);
    }
}
