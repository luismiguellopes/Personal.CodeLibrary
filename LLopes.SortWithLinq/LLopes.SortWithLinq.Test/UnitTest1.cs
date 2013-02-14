using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LLopes.SortWithLinq.Test
{
    [TestClass]
    public class UnitTest1
    {
        static List<int> BuildUnsortedList(int size)
        {
            var rnd = new Random();
            var list = new List<int>();
            for (int i = 0; i < size; ++i)
            {
                list.Add(rnd.Next(int.MaxValue));
            }
            Debug.WriteLine("list generation completed");
            return list;
        }

        static bool ListCheck(IEnumerable<int> list)
        {
            var n = list.First();
            foreach (var x in list)
            {
                if (n > x)
                {
                    Debug.WriteLine("ERROR: List is not sorted");
                    return false;
                }
            }
            Debug.WriteLine("List is sorted");
            return true;
        }

        [TestMethod]
        public void LinqIntQuickSortTest()
        {
            IEnumerable<int> sortedList = null;
            var unsortedList = BuildUnsortedList(100);

            sortedList = QuickSortSamples.LinqIntQuickSort(unsortedList);

            ListCheck(sortedList);
        }

        [TestMethod]
        public void LinqGenericQuickSortTest()
        {
            IEnumerable<int> sortedList = null;
            var unsortedList = BuildUnsortedList(100);

            sortedList = QuickSortSamples.LinqGenericQuickSort(unsortedList, (x, y) => x - y);

            ListCheck(sortedList);
        }

        [TestMethod]
        public void TestLambdaGenericQuickSortTest()
        {
            IEnumerable<int> sortedList = null;
            var unsortedList = BuildUnsortedList(100);

            sortedList = QuickSortSamples.LambdaGenericQuickSort(unsortedList);

            ListCheck(sortedList);
        }
    }
}
