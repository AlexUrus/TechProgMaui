using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSorting
{
    public static class TestStand
    {
        public static int[] GetRandomArray(int lenght, int maxValue, int minValue)
        {
            return ArrayGenerator.Generate(lenght, maxValue, minValue);
        }

        public static long GetTicksMergeSort(int[] array)
        {
            Stopwatch sw = Stopwatch.StartNew();
            MergeSort.Sort(array);
            sw.Stop();
            return sw.ElapsedTicks;
        }

        public static long GetTicksParallelMergeSort(int[] array)
        {
            Stopwatch sw = Stopwatch.StartNew();
            ParallelMergeSort.ParallelSort(array);
            sw.Stop();
            return sw.ElapsedTicks;
        }

        public static long GetTicksWrongParallelMergeSort(int[] array)
        {
            Stopwatch sw = Stopwatch.StartNew();
            WrongParallelMergeSort.Sort(array);
            sw.Stop();
            return sw.ElapsedTicks;
        }
    }
}
