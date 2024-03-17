using NewsGenerator;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TechProgMaui.ViewModels
{
    public class NewspaperViewModel : BaseViewModel, IObserver
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
        private ApiNewsGenerator _apiNewsGenerator;
        private StorageNewsGenerator _storageNewsGenerator;

        public NewspaperViewModel()
        {
            NewsList = new ObservableCollection<News>();
            _apiNewsGenerator = new();
            GenerateNews(_apiNewsGenerator);
        }

        public void Update(object obj)
        {
            if (obj is List<News> list && list.Count > 0) 
            {
                foreach (var item in list)
                {
                    NewsList.Add(item);
                }
            }
            else
            {
                _storageNewsGenerator = new();
                GenerateNews(_storageNewsGenerator);
            }
        }

        private void GenerateNews<T>(T newsGenerator) where T : NewsGenerator.NewsGenerator
        { 
            newsGenerator.AddObserver(this);
            newsGenerator.GenerateNews();
        }
    }
}
