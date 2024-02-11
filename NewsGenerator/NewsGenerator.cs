using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsGenerator
{
    abstract public class NewsGenerator: IObservable<List<News>>
    {
        public List<News> NewsList { get; set; }
        public abstract void GenerateNews();

        protected List<IObserver<List<News>>> observers;
        protected virtual string Category { get; } 

        public NewsGenerator()
        {
            NewsList = new List<News>();
            observers = new List<IObserver<List<News>>>();
        }

        public IDisposable Subscribe(IObserver<List<News>> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        protected void NotifyObservers(List<News> listNews)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(listNews);
            }
        }

        protected string BuildApiUrl()
        {
            string apiKey = "e4fc91bf12f84a68bdfd6635855b5b13";
            return $"https://newsapi.org/v2/top-headlines?country=ru&category={Category}&apiKey={apiKey}";
        }

        protected void SaveJsonToFile(string json, string filePath)
        {
            File.WriteAllText(filePath, json);
        }

        protected void ParseAndNotify(string json)
        {
            JToken jToken = JToken.Parse(json);

            foreach (var item in jToken["articles"])
            {
                var news = new TechNews
                {
                    Title = item["title"]?.ToString(),
                    Description = item["description"]?.ToString(),
                    PublishedAt = DateTime.Parse(item["publishedAt"]?.ToString())
                };

                NewsList.Add(news);
            }

            NotifyObservers(NewsList);
        }

        protected void TryLoadFromSavedFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string savedNewsJson = File.ReadAllText(filePath);
                ParseAndNotify(savedNewsJson);
            }
            else
            {
                throw new Exception("Failed to fetch news.");
            }
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<List<News>>> _observers;
            private IObserver<List<News>> _observer;

            public Unsubscriber(List<IObserver<List<News>>
                > observers, IObserver<List<News>> observer)
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
}
