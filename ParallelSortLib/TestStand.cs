using ParallelSortLib;
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

        public int[] GetRandomArray(int length, int maxValue, int minValue)
        {
            return ArrayGenerator.Generate(length, maxValue, minValue);
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

        public async Task<int[]> GetSortedMasWithConcurencyAsync(int[] array)
        {
            var cts = new CancellationTokenSource();

            Task<int[]> task1 = Task.Run(() => BubbleSort.Sort(array.Clone() as int[]));
            Task<int[]> task2 = Task.Run(() => MergeSort.Sort(array.Clone() as int[]));
            Task<int[]> task3 = Task.Run(() => QuickSort.Sort(array.Clone() as int[]));

            Task<int[]> completedTask = await Task.WhenAny(task1, task2, task3);

            if (completedTask != task1) cts.Cancel();
            if (completedTask != task2) cts.Cancel();
            if (completedTask != task3) cts.Cancel();

            return completedTask.Result;
        }

        public int[] GetSortedMasWithConcurencyAndBlock(int[] array)
        {
            var cts = new CancellationTokenSource();

            Task<int[]> task1 = Task.Run(() => BubbleSort.Sort(array.Clone() as int[]));
            Task<int[]> task2 = Task.Run(() => MergeSort.Sort(array.Clone() as int[]));
            Task<int[]> task3 = Task.Run(() => QuickSort.Sort(array.Clone() as int[]));

            Task<int[]> completedTask = Task.WhenAny(task1, task2, task3).Result;

            if (completedTask != task1) cts.Cancel();
            if (completedTask != task2) cts.Cancel();
            if (completedTask != task3) cts.Cancel();

            return completedTask.Result;
        }


    }
}
