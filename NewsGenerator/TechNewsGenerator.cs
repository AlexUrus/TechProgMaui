using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsGenerator
{
    public class TechNewsGenerator : NewsGenerator
    {
        protected override string Category { get => "technology"; }
        public override async void GenerateNews()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TechNews.json");

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
    }
}
