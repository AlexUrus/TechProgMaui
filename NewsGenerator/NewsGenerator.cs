using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsGenerator
{
    abstract public class NewsGenerator: IObservable
    {
        public List<News> NewsList { get; set; }

        protected List<IObserver> observers;
        protected abstract string Category { get; }
        protected abstract string FileName { get; }

        public NewsGenerator()
        {
            NewsList = new List<News>();
            observers = new List<IObserver>();
        }

        public async void GenerateNews()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(BuildApiUrl());

                    if (response.IsSuccessStatusCode)
                    {
                        string newsJson = await response.Content.ReadAsStringAsync();
                        SaveJsonToFile(newsJson, filePath);
                        ParseAndNotify(newsJson);
                    }
                    else
                    {
                        TryLoadFromSavedFile(filePath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
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
                if(this is TechNewsGenerator)
                {
                    var news = new TechNews();
                    news.Title = Category + " | " + item["title"]?.ToString();
                    news.Description = item["description"]?.ToString();
                    news.PublishedAt = DateTime.Parse(item["publishedAt"]?.ToString());
                    NewsList.Add(news);
                }
                else if(this is SportNewsGenerator)
                {
                    var news = new SportNews();
                    news.Title = Category + " | " + item["title"]?.ToString();
                    news.Description = item["description"]?.ToString();
                    news.PublishedAt = DateTime.Parse(item["publishedAt"]?.ToString());
                    NewsList.Add(news);
                }
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

        public void AddObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Remove(o);
        }

        public void NotifyObservers(object obj)
        {
            foreach (IObserver observer in observers)
                observer.Update(obj);
        }
    }
}
