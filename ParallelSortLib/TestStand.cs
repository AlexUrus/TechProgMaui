using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSorting
{
    public class TestStand
    {
        public async Task<int[]> GetRandomArrayAsync(int length, int maxValue, int minValue)
        {
            return await Task.Run(() => ArrayGenerator.Generate(length, maxValue, minValue));
        }

        public async Task<long> GetTicksMergeSortAsync(int[] array)
        {
            return await Task.Run(() =>
            {
                Stopwatch sw = Stopwatch.StartNew();
                MergeSort.Sort(array);
                sw.Stop();
                return sw.ElapsedTicks;
            });
        }

        public async Task<long> GetTicksParallelMergeSortAsync(int[] array)
        {
            return await Task.Run(() =>
            {
                Stopwatch sw = Stopwatch.StartNew();
                ParallelMergeSort.ParallelSort(array);
                sw.Stop();
                return sw.ElapsedTicks;
            });
        }

        public async Task<long> GetTicksWrongParallelMergeSortAsync(int[] array)
        {
            return await Task.Run(() =>
            {
                Stopwatch sw = Stopwatch.StartNew();
                WrongParallelMergeSort.Sort(array);
                sw.Stop();
                return sw.ElapsedTicks;
            });
        }
    }
}
