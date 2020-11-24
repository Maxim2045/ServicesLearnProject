using Newtonsoft.Json;
using System.Collections.Generic;

namespace ImageServiceYandexApi.Models
{
    public class Image
    {
        public int Size { get; set; }

        public string Name { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        public string File { get; set; }

        public string Preview { get; set; }

        public string Path { get; set; }
        [JsonProperty("items")]
        public IEnumerable<Image> Images { get; set; }
    }
}