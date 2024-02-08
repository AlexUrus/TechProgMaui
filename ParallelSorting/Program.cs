using System.Diagnostics;

namespace ParallelSorting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] massive = ArrayGenerator.Generate(500000, 0, 200);
            var seqMasUnsorted = (int[])massive.Clone();
            var parMasUnsorted = (int[])massive.Clone();
            Stopwatch sw = Stopwatch.StartNew();
            int[] sortMas = MergeSort.Sort(seqMasUnsorted);
            sw.Stop();
            Console.WriteLine(sw.ElapsedTicks);

            Stopwatch sw1 = Stopwatch.StartNew();

            sortMas = ParallelMergeSort.Sort(parMasUnsorted);
            sw1.Stop();
            Console.WriteLine(sw1.ElapsedTicks);

        }
    }
}
