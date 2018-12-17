using Newtonsoft.Json;


namespace ImdbNet.Core.Models
{
	[JsonObject(MemberSerialization.OptIn)]
	public class BaseSuggestionResults<T>
		where T : BaseSuggestionResult
	{
		[JsonProperty(PropertyName = "v")]
		public int Version { get; set; }

		[JsonProperty(PropertyName = "q")]
		public string Query { get; set; }

		[JsonProperty(PropertyName = "d")]
		public T[] Data { get; set; }
	}


	public class BaseSuggestionResult
	{
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "l")]
		public string Title { get; set; }
	}
}
