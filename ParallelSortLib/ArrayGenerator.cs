using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSorting
{
    public static class ArrayGenerator
    {
        public static int[] Generate(int lenght, int maxValue, int minValue)
        {
            int[] massive = new int[lenght];
            Random rnd = new Random();

            for (int i = 0; i < lenght; i++)
            {
                massive[i] = rnd.Next(minValue, maxValue);
            }
            return massive;
        }
    }
}
