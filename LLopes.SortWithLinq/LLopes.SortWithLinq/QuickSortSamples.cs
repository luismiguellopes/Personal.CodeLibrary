using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LLopes.SortWithLinq
{
    public static class QuickSortSamples
    {
        public static IEnumerable<int> LinqIntQuickSort(IEnumerable<int> x)
        {
            return x.Count() <= 1
                ? x
                : LinqIntQuickSort(x.Skip(1).Where(z => z <= x.First()))
                    .Concat(new List<int> { x.First() })
                    .Concat(LinqIntQuickSort(x.Skip(1).Where(z => z > x.First())));
        }

        public static IEnumerable<T> LinqGenericQuickSort<T>(IEnumerable<T> x, Func<T, T, int> fnCompare)
        {
            return x.Count() <= 1
                ? x
                : LinqGenericQuickSort(x.Skip(1)
                        .Where(z => fnCompare(z, x.First()) <= 0), fnCompare)
                    .Concat(new List<T> { x.First() })
                    .Concat(LinqGenericQuickSort(x.Skip(1)
                        .Where(z => fnCompare(z, x.First()) > 0), fnCompare));
        }

        public static IEnumerable<int> LambdaGenericQuickSort(IEnumerable<int> list)
        {
            Func<IEnumerable<int>, Func<int, int, int>, IEnumerable<int>> fn = null;
            fn = (l1, fnCompare) => l1.Count() <= 1
                ? l1
                : fn(l1.Skip(1).Where(z => fnCompare(z, l1.First()) <= 0), fnCompare)
                    .Concat(new List<int> { l1.First() })
                    .Concat(fn(l1.Skip(1).Where(z => fnCompare(z, l1.First()) > 0), fnCompare));

            return fn(list, (x, y) => x - y);
        }


    }
}
