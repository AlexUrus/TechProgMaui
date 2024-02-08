using NewsGenerator;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TechProgMaui.ViewModels
{
    public class NewspaperViewModel : BaseViewModel, IObserver<List<News>>
    {
        private ObservableCollection<News> _newsList;
        public ObservableCollection<News> NewsList
        {
            get => _newsList;
            set
            {
                if (_newsList != value)
                {
                    _newsList = value;
                    OnPropertyChanged();
                }
            }
        }
        public SportNewsGenerator SportNewsGenerator { get; set; }
        public TechNewsGenerator TechNewsGenerator { get; set; }

        public NewspaperViewModel()
        {
            NewsList = new ObservableCollection<News>();
            GenerateNews();
        }

        public void GenerateNews()
        {
            SportNewsGenerator = new SportNewsGenerator();
            TechNewsGenerator = new TechNewsGenerator();
            SportNewsGenerator.Subscribe(this);
            TechNewsGenerator.Subscribe(this);
            SportNewsGenerator.GenerateNews();
            TechNewsGenerator.GenerateNews();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(List<News> value)
        {
            foreach (var item in value)
            {
                NewsList.Add(item);
            }
        }
    }
}
