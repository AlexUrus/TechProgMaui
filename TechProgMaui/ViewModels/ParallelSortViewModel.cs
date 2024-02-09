using ParallelSorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgMaui.ViewModels
{
    public class ParallelSortViewModel : BaseViewModel
    {
        public List<SortResults> SortResultsList;

        public ParallelSortViewModel()
        {
            SortResultsList = new List<SortResults>();
        }

        public void GenerateTestCases()
        {
            int lengthArray = 5000;

            int[] array = TestStand.GetRandomArray(lengthArray, 100, 0);

            SortResultsList.Add(new SortResults()
            {
                NameSort = "MergeSort",
                TicksSorting = TestStand.GetTicksMergeSort(array),
                LengthArray = lengthArray
            });

            SortResultsList.Add(new SortResults()
            {
                NameSort = "ParallelMergeSort",
                TicksSorting = TestStand.GetTicksParallelMergeSort(array),
                LengthArray = lengthArray
            });

            lengthArray = 50000;

            array = TestStand.GetRandomArray(lengthArray, 100, 0);

            SortResultsList.Add(new SortResults()
            {
                NameSort = "MergeSort",
                TicksSorting = TestStand.GetTicksMergeSort(array),
                LengthArray = lengthArray
            });

            SortResultsList.Add(new SortResults()
            {
                NameSort = "ParallelMergeSort",
                TicksSorting = TestStand.GetTicksParallelMergeSort(array),
                LengthArray = lengthArray
            });

            lengthArray = 500000;

            array = TestStand.GetRandomArray(lengthArray, 100, 0);

            SortResultsList.Add(new SortResults()
            {
                NameSort = "MergeSort",
                TicksSorting = TestStand.GetTicksMergeSort(array),
                LengthArray = lengthArray
            });

            SortResultsList.Add(new SortResults()
            {
                NameSort = "ParallelMergeSort",
                TicksSorting = TestStand.GetTicksParallelMergeSort(array),
                LengthArray = lengthArray
            });
        }
    }

    public struct SortResults
    {
        public string NameSort { get; set; }
        public long TicksSorting { get; set; }
        public int LengthArray { get; set; }

        public SortResults(string nameSort, long ticksSorting, int lenghtArray)
        {
            NameSort = nameSort;
            TicksSorting = ticksSorting;
            LengthArray = lenghtArray;
        }
    }

}
