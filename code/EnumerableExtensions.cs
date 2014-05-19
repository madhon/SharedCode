using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System
{
    [DebuggerStepThrough]
    public static class EnumerableExtensions
    {
        //public static int Count<T>(this IEnumerable<T> source)
        //{
        //    if (source == null)
        //        throw new ArgumentNullException("source");

        //    ICollection<T> col = source as ICollection<T>;
        //    if (col != null)
        //        return col.Count;

        //    int count = 0;
        //    using (IEnumerator<T> enumerator = source.GetEnumerator())
        //        while (enumerator.MoveNext())
        //            count++;

        //    return count;
        //}
        
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
            {
                action(element);
            }
        }

    }
}
