using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Globo.Model
{
    public class Feed
    {

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }

    public class Enclosure
    {

        [JsonProperty("link")]
        public string Link { get; set; }
    }

    public class Item
    {

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("pubDate")]
        public string PubDate { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("enclosure")]
        public Enclosure Enclosure { get; set; }

        [JsonProperty("categories")]
        public IList<string> Categories { get; set; }
    }

    public class Notice
    {

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("feed")]
        public Feed Feed { get; set; }

        [JsonProperty("items")]
        public IList<Item> Items { get; set; }
    }
}
