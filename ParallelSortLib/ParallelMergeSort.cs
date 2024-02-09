using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSorting
{
    public static class ParallelMergeSort
    {
        public static void Merge(int[] array, int left, int mid, int right)
        {
            int[] temp = new int[right - left + 1];
            int i = left, j = mid + 1, k = 0;

            while (i <= mid && j <= right)
            {
                if (array[i] <= array[j])
                {
                    temp[k] = array[i];
                    k++;
                    i++;
                }
                else
                {
                    temp[k] = array[j];
                    k++;
                    j++;
                }
            }

            while (i <= mid)
            {
                temp[k] = array[i];
                k++;
                i++;
            }

            while (j <= right)
            {
                temp[k] = array[j];
                k++;
                j++;
            }

            for (i = left; i <= right; i++)
            {
                array[i] = temp[i - left];
            }
        }

        public static void ParallelMergeSortInternal(int[] array, int left, int right, int depth)
        {
            const int SequentialThreshold = 1000;

            if (left < right)
            {
                if (right - left < SequentialThreshold || depth <= 0)
                {
                    Array.Sort(array, left, right - left + 1);
                }
                else
                {
                    int mid = (left + right) / 2;

                    Parallel.Invoke(
                        () => ParallelMergeSortInternal(array, left, mid, depth - 1),
                        () => ParallelMergeSortInternal(array, mid + 1, right, depth - 1)
                    );

                    Merge(array, left, mid, right);
                }
            }
        }

        public static int[] ParallelSort(int[] array)
        {
            ParallelMergeSortInternal(array, 0, array.Length - 1, Environment.ProcessorCount);
            return array;
        }
    }
}
