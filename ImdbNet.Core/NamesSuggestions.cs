using System.Linq;
using Newtonsoft.Json;


namespace ImdbNet.Core
{
    /*
{
    "v": 1,
    "q": "jack",
    "d": [ ... ]
    }
     */
    [JsonObject(MemberSerialization.OptIn)]
	public class NamesSuggestionsResults
    {
        [JsonProperty(PropertyName = "v")]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "q")]
        public string Query { get; set; }

        [JsonProperty(PropertyName = "d")]
        public NameSuggestion[] Data { get; set; }
    }

	/*
        {
            "l": "Jack O'Connell (IV)",
            "id": "nm1925239",
            "s": "Actor, Unbroken (2014)",
            "i": [ "https://images-na.ssl-images-amazon.com/images/M/MV5BMTU4MjU0NjI4NF5BMl5BanBnXkFtZTgwNjU3NTUzMDI@._V1_.jpg", 973, 1338 ]
        },
     */
	public class NameSuggestion
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "l")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "s")]
        public string Summary { get; set; }

        public string ImagePath => ImageDetails?.ElementAt(0) as string;

        [JsonProperty(PropertyName = "i")]
        private object[] ImageDetails { get; set; }
    }
}
