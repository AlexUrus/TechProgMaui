using ParallelSorting;
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
            var testStand = new TestStand();

            int lengthArray = 5000;

            int[] array = await testStand.GetRandomArrayAsync(lengthArray, 100, 0);

            SortResultsList.Add(new SortResults()
            {
                NameSort = "MergeSort",
                TicksSorting = await testStand.GetTicksMergeSortAsync(array),
                LengthArray = lengthArray
            });

            SortResultsList.Add(new SortResults()
            {
                NameSort = "ParallelMergeSort",
                TicksSorting = await testStand.GetTicksParallelMergeSortAsync(array),
                LengthArray = lengthArray
            });

            lengthArray = 50000;

            array = await testStand.GetRandomArrayAsync(lengthArray, 100, 0);

            SortResultsList.Add(new SortResults()
            {
                NameSort = "MergeSort",
                TicksSorting = await testStand.GetTicksMergeSortAsync(array),
                LengthArray = lengthArray
            });

            SortResultsList.Add(new SortResults()
            {
                NameSort = "ParallelMergeSort",
                TicksSorting = await testStand.GetTicksParallelMergeSortAsync(array),
                LengthArray = lengthArray
            });

            lengthArray = 500000;

            array = await testStand.GetRandomArrayAsync(lengthArray, 100, 0);

            SortResultsList.Add(new SortResults()
            {
                NameSort = "MergeSort",
                TicksSorting = await testStand.GetTicksMergeSortAsync(array),
                LengthArray = lengthArray
            });

            SortResultsList.Add(new SortResults()
            {
                NameSort = "ParallelMergeSort",
                TicksSorting = await testStand.GetTicksParallelMergeSortAsync(array),
                LengthArray = lengthArray
            });

            lengthArray = 5000000;

            array = await testStand.GetRandomArrayAsync(lengthArray, 100, 0);

            SortResultsList.Add(new SortResults()
            {
                NameSort = "MergeSort",
                TicksSorting = await testStand.GetTicksMergeSortAsync(array),
                LengthArray = lengthArray
            });

            SortResultsList.Add(new SortResults()
            {
                NameSort = "ParallelMergeSort",
                TicksSorting = await testStand.GetTicksParallelMergeSortAsync(array),
                LengthArray = lengthArray
            });

            NotifyObservers(SortResultsList);
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

        public SortResults(string nameSort, long ticksSorting, int lenghtArray)
        {
            NameSort = nameSort;
            TicksSorting = ticksSorting;
            LengthArray = lenghtArray;
        }
    }

}
