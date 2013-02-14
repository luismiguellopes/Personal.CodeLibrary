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
            fn = (x1, fnc) => x1.Count() <= 1
                ? x1
                : fn(x1.Skip(1).Where(z => fnc(z, x1.First()) <= 0), fnc)
                    .Concat(new List<int> { x1.First() })
                    .Concat(fn(x1.Skip(1).Where(z => fnc(z, x1.First()) > 0), fnc));

            return fn(list, (x, y) => x - y);
        }


    }
}
