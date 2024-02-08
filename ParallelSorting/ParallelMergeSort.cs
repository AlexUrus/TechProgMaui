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

        public static int[] Sort(int[] array, int lowIndex, int highIndex)
        {

            if (lowIndex < highIndex)
            {
                var middleIndex = (lowIndex + highIndex) / 2;
                if (array.Length > 10000)
                {
                    Parallel.Invoke(
                                        () => Sort(array, lowIndex, middleIndex),
                                        () => Sort(array, middleIndex + 1, highIndex)
                                    );
                }
                else
                {
                    Sort(array, lowIndex, middleIndex);
                    Sort(array, middleIndex + 1, highIndex);
                }

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
