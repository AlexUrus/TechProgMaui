using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSorting
{
    public static class WrongParallelMergeSort
    {
        public static void Merge(int[] array, int left, int mid, int right)
        {
            int[] temp = new int[right - left + 1];
            int i = left, j = mid + 1, k = 0;

            Parallel.For(left, right + 1, index =>
            {
                if (i <= mid && (j > right || array[i] <= array[j]))
                {
                    temp[index - left] = array[i];
                    i++;
                }
                else
                {
                    temp[index - left] = array[j];
                    j++;
                }
            });

            for (i = left; i <= right; i++)
            {
                array[i] = temp[i - left];
            }
        }

        public static int[] Sort(int[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                var middleIndex = (lowIndex + highIndex) / 2;
                Sort(array, lowIndex, middleIndex);
                Sort(array, middleIndex + 1, highIndex);
                Merge(array, lowIndex, middleIndex, highIndex);
            }
            return array;
        }

        public static int[] Sort(int[] array)
        {
            return Sort(array, 0, array.Length - 1);
        }
    }
}
