using System;
using System.Linq;
using Newtonsoft.Json;


namespace ImdbNet.Core
{
	[JsonObject(MemberSerialization.OptIn)]
	public class TitlesSuggestionsResults : BaseSuggestionResults<TitleSuggestion> { }


	public class TitleSuggestion : BaseSuggestionResult
	{
		private static readonly char[] StarsSeparators = { ',' };
		private string[] _stars;

		[JsonProperty(PropertyName = "s")]
		public string Stars
		{
			get { return string.Join(", ", _stars); }
			set
			{
				_stars = (value ?? string.Empty)
					.Split(StarsSeparators, StringSplitOptions.RemoveEmptyEntries)
					.Select(str => str.Trim()).ToArray();
			}
		}

		[JsonProperty(PropertyName = "y")]
		public int Year { get; set; }

		[JsonProperty(PropertyName = "q")]
		public string Type { get; set; }
	}
}
