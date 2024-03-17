using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewsGenerator
{
    public class StorageNewsGenerator : NewsGenerator
    {
        public override async void GenerateNews()
        {
            string filePath = FileName;
			try
			{
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);
                string jsonText = File.ReadAllText(filePath);
                ParseNews(jsonText);
                if(NewsList.Count > 0 ) NotifyObservers(NewsList);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Файл '{filePath}' не найден.");
            }
            catch (Newtonsoft.Json.JsonException)
            {
                Console.WriteLine($"Ошибка парсинга JSON файла '{filePath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        private void ParseNews(string json)
        { 
            var storageNews = JsonConvert.DeserializeObject<List<StorageNews>>(json);
            NewsList.AddRange(storageNews);
        }
    }
}
