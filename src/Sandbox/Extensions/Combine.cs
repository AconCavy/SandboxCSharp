using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.Extensions
{
    public static partial class EnumerableExtension
    {
        public static IEnumerable<IEnumerable<T>> Combine<T>(this IEnumerable<T> source, int count)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            IEnumerable<IEnumerable<T>> Inner()
            {
                var items = source.ToArray();
                if (count <= 0 || items.Length < count) throw new ArgumentOutOfRangeException(nameof(count));
                var idx = 0;
                var ret = new T[count];
                foreach (var x in Combination(items.Length, count))
                {
                    ret[idx++] = items[x];
                    if (idx == count) yield return ret;
                    idx %= count;
                }
            }

            return Inner();
        }

        private static IEnumerable<int> Combination(int n, int r)
        {
            var items = new int[r];

            IEnumerable<int> Inner(int step = 0, int value = 0)
            {
                if (step >= r)
                {
                    foreach (var x in items) yield return x;
                    yield break;
                }

                for (var i = value; i <= n - r + step; i++)
                {
                    items[step] = i;
                    foreach (var x in Inner(step + 1, i + 1)) yield return x;
                }
            }

            return Inner();
        }
    }
}