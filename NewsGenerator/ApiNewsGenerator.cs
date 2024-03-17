using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewsGenerator
{
    public class ApiNewsGenerator : NewsGenerator
    {
        private const string _apiKey = "e4fc91bf12f84a68bdfd6635855b5b13";
        private const string _bodyURL = "https://newsapi.org/v2/top-headlines?country=ru&category=";

        public override async void GenerateNews()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    foreach (var category in CategoryMas)
                    {
                        HttpResponseMessage response = await httpClient.GetAsync(BuildApiUrl(category));
                        if (response.IsSuccessStatusCode)
                        {
                            string newsJson = await response.Content.ReadAsStringAsync();
                           // ParseNewsByCategory(newsJson, category);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            NotifyObservers(NewsList);
            //SaveToStorageNews();
        }

        private string BuildApiUrl(string category)
        {
            return $"{_bodyURL}{category}&apiKey={_apiKey}";
        }

        private void ParseNewsByCategory(string json, string category)
        {
            JToken jToken = JToken.Parse(json);

            foreach (var item in jToken["articles"])
            {
                var news = new ApiNews();
                news.Title = item["title"]?.ToString();
                news.Description = item["description"]?.ToString();
                news.PublishedAt = DateTime.Parse(item["publishedAt"]?.ToString());
                NewsList.Add(news);
            }
        }

        private void SaveToStorageNews()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);
            string json = JsonSerializer.Serialize(NewsList);
            SaveJsonToFile(json, filePath);
        }

        private async void SaveJsonToFile(string json, string filePath)
        {
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}
