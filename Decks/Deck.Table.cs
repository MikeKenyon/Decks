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
            InPlay.EnabledCheck();
            var card = Draw();
            InPlay.Contents.Add(card);
            return card;
        }

        internal Table<TElement> InPlay { get; }
        public ITable<TElement> Table { get { return InPlay; } }
    }
}
