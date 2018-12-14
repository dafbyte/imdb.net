using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace ImdbNet.Core
{
	public class Utils
	{
		public const int HTTP_CONTENT_CHUNK_SIZE = 65536;
		private static readonly bool TraceHttpByteTransfer;

		static Utils()
		{
			TraceHttpByteTransfer = false;
		}

		#region Suggestions

		public static TitlesSuggestionsResults GetTitlesSuggestionsResults(string query) =>
			GetTitlesSuggestionsResultsAsync(query).Result;

		public static async Task<TitlesSuggestionsResults> GetTitlesSuggestionsResultsAsync(string query)
		{
			return await GetSuggestionsResultsAsync<TitlesSuggestionsResults>(Urls.TitleSuggestions, query);
		}

		public static NamesSuggestionsResults GetNamesSuggestions(string query) =>
			GetNamesSuggestionsAsync(query).Result;

		public static async Task<NamesSuggestionsResults> GetNamesSuggestionsAsync(string query) =>
			await GetSuggestionsResultsAsync<NamesSuggestionsResults>(Urls.NameSuggestions, query);

		private static async Task<T> GetSuggestionsResultsAsync<T>(string uri, string query)
			where T : class
		{
			if (string.IsNullOrEmpty(query))
				throw new ArgumentNullException(nameof(query));

			query = query.ToLower();
			uri = $"{uri}/{query[0]}/{query}.json";

			try
			{
				Console.WriteLine($"Getting data for \"{query}\" at {uri}");
				var json = await GetContent(uri);
				json = GetJsonFromCallback(json);
				var results = JsonConvert.DeserializeObject<T>(json);
				return results;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
				return null;
			}
		}

		#endregion


		#region Common Methods

		private static async Task<string> GetContent(string uri)
		{
			var req = WebRequest.Create(uri);
			var sr = new StringBuilder();
			try
			{
				using (var res = await req.GetResponseAsync())
				{
					if (TraceHttpByteTransfer)
						Console.WriteLine($"Reading {res.ContentLength} bytes...");
					var buffer = new byte[HTTP_CONTENT_CHUNK_SIZE];
					using (var s = res.GetResponseStream())
					{
						if (null == s)
							return null;

						int total = 0,
							count;
						while ((count = s.Read(buffer, 0, HTTP_CONTENT_CHUNK_SIZE)) > 0)
						{
							total += count;
							if (TraceHttpByteTransfer)
								Console.WriteLine(
									$"    Read {total} / {buffer.Length} bytes ( {(res.ContentLength > 0 ? $"{(double)total / res.ContentLength:P2}" : "---")} ).");
							sr.Append(Encoding.UTF8.GetString(buffer, 0, count));
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
				sr = null;
			}
			return sr?.ToString();
		}

		private static string GetJsonFromCallback(string json)
		{
			var tstIndex = json.IndexOf("{", StringComparison.Ordinal);
			if (tstIndex < 0)
				throw new InvalidOperationException("Given value is not a valid json.");

			return json.Substring(tstIndex, json.Length - 1 - tstIndex);
		}

		#endregion
	}
}
