using Newtonsoft.Json;

namespace NewsGenerator
{
    abstract public class News
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("publishedAt")]
        public DateTime PublishedAt { get; set; }
    }
}
