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
        public ICommand StartOneSortWithException { get; private set; }
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
            StartOneSortWithException = new Command(async () =>
            {
                int[] array = await _testStand.GetRandomArrayAsync(1000000, 100, 0);
                try
                {
                    int[] sortedMas = await _testStand.SortMasAsyncWithException(array);
                    SortedMas = string.Join(", ", sortedMas);
                }
                catch (Exception)
                {
                    SortedMas = "Ошибка сортировки";

                }
            });
        }
    }
}
