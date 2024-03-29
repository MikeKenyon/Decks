﻿using Decks.Configuration;

namespace Decks
{
    /// <summary>
    /// The table is the public/shared playspace.  Elements may be played from hands or the tableau to the table.
    /// </summary>
    /// <typeparam name="TElement">The elemenets on the table.</typeparam>
    public interface ITable<TElement> : IDeckStack<TElement>
        where TElement : class
    {
        /// <summary>
        /// The options pertainent to this table.
        /// </summary>
        ITableOptions Options { get; }
        /// <summary>
        /// Discards a single element from the table.
        /// </summary>
        /// <param name="element">The element to discard.</param>
        /// <returns>The table (for fluent purposes).</returns>
        /// <exception cref="System.InvalidOperationException">
        ///     The table is not enabled, see <see cref="Configuration.ITableauOptions.Enabled"/>.
        /// </exception>
        /// <exception cref="InvalidElementException">The element isn't on the table.</exception>
        ITable<TElement> Discard(TElement element);
        /// <summary>
        /// Discards all elements from the table.  Unlike a <see cref="IHand{TElement}"/>, the table persists 
        /// after this operation.
        /// </summary>
        /// <returns>The table (for fluent purposes).</returns>
        /// <exception cref="System.InvalidOperationException">
        ///     The table is not enabled, see <see cref="Configuration.ITableauOptions.Enabled"/>.
        /// </exception>
        ITable<TElement> Muck();
        /// <summary>
        /// Checks to see if the table is enabled (from <see cref="Options"/>).
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        ///     The table is not enabled, see <see cref="Configuration.ITableauOptions.Enabled"/>.
        /// </exception>
        bool Enabled { get; }
    }
}
