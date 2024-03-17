using Newtonsoft.Json;

namespace NewsGenerator
{
    public abstract class News
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty(propertyName: "description")]
        public string Description { get; set; }

        [JsonProperty("publishedAt")]
        public DateTime PublishedAt { get; set; }

        [JsonIgnore]
        public virtual string ViewTitle { get;}
    }
}
