using ParallelSorting;
using System.Windows.Input;

namespace TechProgMaui.ViewModels
{
    public class CancelSortViewModel: BaseViewModel
    {
        public ICommand StartSortWithoutBlockCancelWhenAnyConmmand { get; private set; }
        public ICommand StartSortWithBlockCancelWhenAnyConmmand { get; private set; }
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

        public NotifyTaskCompletion<int[]> NotifyTaskCompletionSortedMas { get; private set; }

        private TestStand _testStand;

        public CancelSortViewModel()
        {
            _testStand = new TestStand();
            StartSortWithoutBlockCancelWhenAnyConmmand = new Command(async () =>
            {
                int[] array = await _testStand.GetRandomArrayAsync(100000, 100, 0);
                //int[] sortedArray = await _testStand.GetSortedMasWithConcurencyAsync(array);
                NotifyTaskCompletionSortedMas = new NotifyTaskCompletion<int[]>(_testStand.GetSortedMasWithConcurencyAsync(array));
            });
            StartSortWithBlockCancelWhenAnyConmmand = new Command(async () =>
            {
                int[] array = await _testStand.GetRandomArrayAsync(1000000, 100, 0);
                int[] sortedArray = _testStand.GetSortedMasWithConcurencyAndBlock(array);
                SortedMas = string.Join(", ", sortedArray);
            });
        }
    }
}
