using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Decks
{
    internal static class Extensions
    {
        public static Random Rand = new Random(); 

        public static void Shuffle<T>(this List<T> list)
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
