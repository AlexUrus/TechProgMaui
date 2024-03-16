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
            SportNewsGenerator.AddObserver(this);
            TechNewsGenerator.AddObserver(this);
            SportNewsGenerator.GenerateNews();
            TechNewsGenerator.GenerateNews();
        }

        public void Update(object obj)
        {
            if (obj is List<News> list) 
            {
                foreach (var item in list)
                {
                    NewsList.Add(item);
                }
            }
        }
    }
}
