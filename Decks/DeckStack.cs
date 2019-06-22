using System.Collections;
using System.Collections.Generic;

namespace Decks
{
    /// <summary>
    /// A base class for different types of deck stacks -- hands, table, tableau.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    internal class DeckStack<TElement> : IDeckStack<TElement> where TElement : class
    {
        protected internal List<TElement> Contents { get; } = new List<TElement>();

        public int Count { get { return Contents.Count; } }

        /// <summary>
        /// Checks if this hand contains that element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public virtual bool Contains(TElement element)
        {
            return Contents.Contains(element);
        }
        public virtual IEnumerator<TElement> GetEnumerator()
        {
            return Contents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
