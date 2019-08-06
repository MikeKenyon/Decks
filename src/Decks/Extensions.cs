using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Text;

namespace Decks
{
    /// <summary>
    /// Extension methods, used in implementations.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// The random number seed for this project.
        /// </summary>
        public static readonly Random Rand = new Random(); 

        /// <summary>
        /// Adds all elements to an observable collection.
        /// </summary>
        /// <typeparam name="T">The element type for the collection.</typeparam>
        /// <param name="list">(this) The collection we add to.</param>
        /// <param name="toAdd">The items to add.</param>

        public static void AddRange<T>(this ObservableCollection<T> list, IEnumerable<T> toAdd)
        {
            Contract.Requires(list != null);
            Contract.Requires(toAdd != null);

            foreach(var element in toAdd)
            {
                list.Add(element);
            }
        }

        /// <summary>
        /// Shuffles the contents of a list.
        /// </summary>
        /// <typeparam name="T">The element type for the collection.</typeparam>
        /// <param name="list">(this) The collection we shuffle.</param>
        public static void Shuffle<T>(this ObservableCollection<T> list)
        {
            Contract.Requires(list != null);

            var count = list.Count;
            var last = count - 1;
            for(int i = 0; i < last; ++i)
            {
                var r = Rand.Next(i, count);
                var tmp = list[i];
                list[i] = list[r];
                list[r] = tmp;
            }
        }

        /// <summary>
        /// Applies some action to every element of a collection.
        /// </summary>
        /// <typeparam name="T">The element type for the collection.</typeparam>
        /// <param name="list">(this) The collection we act on.</param>
        /// <param name="todo">The action to perform on each element.</param>
        public static void Apply<T>(this IEnumerable<T> list, Action<T> todo)
        {
            Contract.Requires(list != null);
            Contract.Requires(todo != null);

            foreach(var item in list)
            {
                todo(item);
            }
        }
    }
}
