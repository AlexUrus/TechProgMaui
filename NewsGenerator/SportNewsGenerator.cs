using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsGenerator
{
    public class SportNewsGenerator : NewsGenerator
    {
        protected override string Category { get => "sport" ;}

        public override async void GenerateNews()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string apiKey = "e4fc91bf12f84a68bdfd6635855b5b13";
                string apiUrl = $"https://newsapi.org/v2/top-headlines?country=ru&category={Category}&apiKey={apiKey}";

                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string newsJson = await response.Content.ReadAsStringAsync();
                    JToken jToken = JToken.Parse(newsJson);

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
                else
                {
                    throw new Exception($"Failed to fetch news. Status code: {response.StatusCode}");
                }
            }
        }

        
    }
}
