using ParallelSorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TechProgMaui.ViewModels
{
    public class HandleExceptionTaskViewModel: BaseViewModel
    {
        public ICommand StartOneSortWithEx { get; private set; }
        public ICommand StartTwoSortWithEx { get; private set; }
        public ICommand StartThreeSortWithEx { get; private set; }

        private string _sortedMas;
        public string SortedMas
        {
            get => _sortedMas;
            set
            {
                if (_sortedMas != value)
                {
                    _sortedMas = value;
                    OnPropertyChanged();
                }
            }
        }

        private TestStand _testStand;

        public HandleExceptionTaskViewModel()
        {
            _testStand = new TestStand();
            int[] array = _testStand.GetRandomArray(1000000, 100, 0);
            StartOneSortWithEx = new Command(async () =>
            {
                try
                {
                    int[] sortedMas = await _testStand.BubbleSortMasAsyncWithException(array);
                    SortedMas = string.Join(", ", sortedMas);
                }
                catch (Exception ex)
                {
                    SortedMas = ex.Message;
                }
            });

            StartTwoSortWithEx = new Command(async () =>
            {
                Task result = null;
                try
                {
                    Task<int[]> mergeSortTask = _testStand.MergeSortMasAsyncWithException(array);
                    Task<int[]> bubbleSortTask = _testStand.BubbleSortMasAsyncWithException(array);

                    await (result = Task.WhenAll(mergeSortTask, bubbleSortTask));
                }
                catch (Exception)
                {
                    foreach (var ex in result.Exception.InnerExceptions)
                    {
                        SortedMas += $"{ex.Message} " + $"\n"; 
                    }
                }
            });

            StartThreeSortWithEx = new Command(async () =>
            {
                CancellationTokenSource tokenSource = new CancellationTokenSource();

                Task<int[]> bubbleSort = _testStand.BubbleSortMasAsyncWithException(array);
                Task<int[]> mergeSort = _testStand.MergeSortMasAsync(array);
                Task<int[]> quickSort = _testStand.QuickSortMasAsyncCanceled(array,tokenSource.Token);

                Task<int[]> completedTask = await Task.WhenAny(mergeSort, bubbleSort, quickSort);
                tokenSource.Cancel();

                Task ContinueBubbleException = bubbleSort.ContinueWith(x =>
                {
                    SortedMas += "Продолжение BubbleSort " + x.Exception.InnerException.Message + $"\n";
                }, TaskContinuationOptions.OnlyOnFaulted);

                Task ContinueBubbleComplete = bubbleSort.ContinueWith( x =>
                {
                    SortedMas += "Продолжение BubbleSort - все успешно выполнено" + $"\n";
                    SortedMas += string.Join(", ", bubbleSort.Result);
                }, TaskContinuationOptions.OnlyOnRanToCompletion);

                Task ContinueBubbleCanceled = bubbleSort.ContinueWith(x =>
                {
                    SortedMas += "Продолжение BubbleSort отменен" + $"\n";
                }, TaskContinuationOptions.OnlyOnCanceled);


                Task ContinueMergeException = mergeSort.ContinueWith(x =>
                {
                    SortedMas += "Продолжение MergeSort " + x.Exception.InnerException.Message + $"\n";
                }, TaskContinuationOptions.OnlyOnFaulted);

                Task ContinueMergeComplete = mergeSort.ContinueWith(x =>
                {
                    SortedMas += "Продолжение MergeSort успешная сортировка" + $"\n";
                    SortedMas += string.Join(", ", mergeSort.Result);
                }, TaskContinuationOptions.OnlyOnRanToCompletion);

                Task ContinueMergeCanceled = mergeSort.ContinueWith(x =>
                {
                    SortedMas += "Продолжение MergeSort отменен" + $"\n";
                }, TaskContinuationOptions.OnlyOnCanceled);


                Task ContinueInsertException = quickSort.ContinueWith(x =>
                {
                    SortedMas += "Продолжение QuickSort " + x.Exception.InnerException.Message + $"\n";
                }, TaskContinuationOptions.OnlyOnFaulted);

                Task ContinueInsertComplete = quickSort.ContinueWith(x =>
                {
                    SortedMas += "Продолжение QuickSort успешная сортировка" + $"\n";
                    SortedMas += string.Join(", ", quickSort.Result);
                }, TaskContinuationOptions.OnlyOnRanToCompletion);

                Task ContinueInsertCanceled = quickSort.ContinueWith(x =>
                {
                    SortedMas += "Продолжение QuickSort отменен" + $"\n";
                }, TaskContinuationOptions.OnlyOnCanceled);
            });
        }
    }
}
