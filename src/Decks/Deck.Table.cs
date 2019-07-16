using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    // Portion of the Deck that deals with the table.
    public partial class Deck<TElement> : IDeck<TElement> where TElement : class
    {

        /// <summary>
        /// Plays the top card from the deck to the table.
        /// </summary>
        /// <returns>The element played.</returns>
        public TElement Play()
        {
            ((Internal.ITableInternal<TElement>)_table).CheckEnabled();
            var card = ((Internal.IDrawPileInternal<TElement>)this._drawPile).Draw();
            ((Internal.ITableInternal<TElement>)_table).Add(card);
            return card;
        }

        Internal.ITableInternal<TElement> Internal.IDeckInternal<TElement>.TableStack { get { return _table; } }
        public ITable<TElement> Table { get { return _table; } }
    }
}
