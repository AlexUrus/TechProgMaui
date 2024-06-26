﻿using ParallelSorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgMaui.ViewModels
{
    public class ParallelSortViewModel : BaseViewModel, IObservable<List<SortResults>>
    {
        public List<SortResults> SortResultsList;
        private List<IObserver<List<SortResults>>> observers;
        public ParallelSortViewModel()
        {
            SortResultsList = new List<SortResults>();
            observers = new List<IObserver<List<SortResults>>>();
            GenerateTestCases();
        }

        public async void GenerateTestCases()
        {
            await GenerateAndAddSortResults("MergeSort", 5000);
            await GenerateAndAddSortResults("ParallelMergeSort", 5000);
            await GenerateAndAddSortResults("WrongParallelMergeSort", 5000);

            await GenerateAndAddSortResults("MergeSort", 50000);
            await GenerateAndAddSortResults("ParallelMergeSort", 50000);

            await GenerateAndAddSortResults("MergeSort", 500000);
            await GenerateAndAddSortResults("ParallelMergeSort", 500000);

            await GenerateAndAddSortResults("MergeSort", 5000000);
            await GenerateAndAddSortResults("ParallelMergeSort", 5000000);

            NotifyObservers(SortResultsList);
        }

        private async Task GenerateAndAddSortResults(string sortName, int lengthArray)
        {
            var testStand = new TestStand();
            int[] array = await testStand.GetRandomArrayAsync(lengthArray, 100, 0);

            var arrayTicks = await GetSortingTicksAsync(testStand, sortName, array);

            int[] sortedArray = arrayTicks.SortedArray;
            long tickSorting = arrayTicks.TicksSort;
            SortResultsList.Add(new SortResults()
            {
                NameSort = sortName,
                TicksSorting = tickSorting,
                LengthArray = lengthArray,
                IsCorrectSorted = await IsCorrectSort(sortedArray)
            });
        }

        private async Task<bool> IsCorrectSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1])
                {
                    return false;
                }
            }
            return true;
        } 

        private async Task<ArrayTicks> GetSortingTicksAsync(TestStand testStand, string sortName, int[] array)
        {
            return sortName switch
            {
                "MergeSort" => await testStand.GetTicksMergeSortAsync(array),
                "ParallelMergeSort" => await testStand.GetTicksParallelMergeSortAsync(array),
                "WrongParallelMergeSort" => await testStand.GetTicksWrongParallelMergeSortAsync(array),
                _ => throw new ArgumentException("Unsupported sort name"),
            };
        }

        public IDisposable Subscribe(IObserver<List<SortResults>> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        protected void NotifyObservers(List<SortResults> listSortResults)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(listSortResults);
            }
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<List<SortResults>>> _observers;
            private IObserver<List<SortResults>> _observer;

            public Unsubscriber(List<IObserver<List<SortResults>>> observers, 
                IObserver<List<SortResults>> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }

    public struct SortResults
    {
        public string NameSort { get; set; }
        public long TicksSorting { get; set; }
        public int LengthArray { get; set; }
        public bool IsCorrectSorted { get; set; }

        public SortResults(string nameSort, long ticksSorting, int lenghtArray, bool isCorrectSorted)
        {
            NameSort = nameSort;
            TicksSorting = ticksSorting;
            LengthArray = lenghtArray;
            IsCorrectSorted = isCorrectSorted;
        }
    }

}
