using ImdbNet.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


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


		#region People

		public static Person GetPerson(string id) => GetPersonAsync(id).Result;

		public static async Task<Person> GetPersonAsync(string id)
		{
			var html = await GetPersonHtml(id);

			var matches = Regex.Matches(html, RegexPatterns.Names.Name, RegexOptions.IgnoreCase);
			string name = null;
			if (matches.Count > 0 && matches[0].Groups.Count > 1)
			{
				name = matches[0].Groups[1].Value;
				Console.WriteLine($"Identified person name: {name}");
			}
			else
			{
				Console.WriteLine("Failed to parse person name.");
			}

			var result = new Person(id, name);

			var films = new List<(string id, string title)>();
			var titles = Regex.Matches(html, RegexPatterns.Names.ActorRoles);
			foreach (Match match in titles)
			{
				string titleId = match.Groups[1].Value,
					title = match.Groups[2].Value;
				if (string.IsNullOrEmpty(titleId))
				{
					Console.WriteLine("Empty title found, skipping.");
				}
				else
				{
					var msg = $"Found title #{titleId}: {title}";
					Console.WriteLine(msg);
					result.Filmography.Add(titleId, title);
				}
			}

			Console.WriteLine("Total titles found: " + result.Filmography.Count);
			return result;
		}

		private static async Task<string> GetPersonHtml(string id)
		{
			if (string.IsNullOrEmpty(id))
				throw new ArgumentNullException(nameof(id));

			var uri = $"{Urls.Name}/{id}/";
			string content;

			try
			{
				Console.WriteLine($"Getting data for person \"{id}\" at {uri}");
				content = await GetContent(uri);
				//System.IO.File.WriteAllText(id + ".html", content);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
				content = null;
			}
			return content;
		}

		#endregion


		#region Movies


		public static Movie GetMovie(string id) => GetMovieAsync(id).Result;

		public static async Task<Movie> GetMovieAsync(string id)
		{
			var html = await GetMovieHtml(id);

			var matches = Regex.Matches(html, RegexPatterns.Titles.Name, RegexOptions.IgnoreCase);
			string name = null;
			if (matches.Count > 0 && matches[0].Groups.Count > 1)
			{
				name = matches[0].Groups[1].Value;
				Console.WriteLine($"Identified movie name: {name}");
			}
			else
			{
				Console.WriteLine("Failed to parse movie name.");
			}

			var result = new Movie(id, name);
			ParseCast(html, result);

			//Console.WriteLine("Total titles found: " + result.Filmography.Count);
			return result;
		}

		private static async Task<string> GetMovieHtml(string id)
		{
			if (string.IsNullOrEmpty(id))
				throw new ArgumentNullException(nameof(id));

			var uri = $"{Urls.Title}/{id}/reference";
			string content;

			try
			{
				Console.WriteLine($"Getting data for movie \"{id}\" at {uri}");
				content = await GetContent(uri);
				//System.IO.File.WriteAllText(id + ".html", content);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
				content = null;
			}
			return content;
		}

		private static void ParseCast(string html, Movie movie)
		{
			var matches = Regex.Matches(html, RegexPatterns.Titles.CastTable, RegexOptions.IgnoreCase);
			if (matches.Count <= 0) return;

			var indStart = matches[0].Index + matches[0].Length;
			//Regex.Matches(html.Substring(indStart), "</table>", RegexOptions.IgnoreCase);
			var rgxTableEnd = new Regex("</table>", RegexOptions.IgnoreCase);
			matches = rgxTableEnd.Matches(html, indStart);
			if (matches.Count <= 0) return;

			var tableHtml = html.Substring(indStart, matches[0].Index - indStart);
			var cast = Regex.Matches(tableHtml, @"<span class=""itemprop"" itemprop=""name"">(?<cast>.*)</span>",
				RegexOptions.Multiline);
			movie.Casts = new List<MovieCast>(cast.Count);
			foreach (Match match in cast)
			{
				var item = new MovieCast
				{
					Person = new Person("", match.Groups[1].Value),
					Role = "Cast"
				};
				movie.Casts.Add(item);
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
