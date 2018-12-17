using System;
using ImdbNet.Core;
using ImdbNet.Core.Models;
using NUnit.Framework;


namespace ImdbNet.Tests
{
	[TestFixture]
	public class UtilsTests
	{
		[Test]
		public static void TestTitlesSuggestions()
		{
			var titleSuggestions = Utils.GetTitlesSuggestionsResults(TestQuery);
			ShowSuggestions(titleSuggestions,
				s => $"    \"{s.Title}\" ({s.Year}) with {string.Join(", ", s.Stars)} << {s.Id} >>");
		}

		[Test]
		public static void TestNamesSuggestions()
		{
			var nameSuggestions = Utils.GetNamesSuggestions(TestQuery);
			ShowSuggestions(nameSuggestions, s => $"    \"{s.Title}\" as {s.Summary} << {s.Id} >>");
		}

		private static void ShowSuggestions<T>(BaseSuggestionResults<T> suggestions, Func<T, string> itemToString)
			where T : BaseSuggestionResult
		{
			Console.WriteLine(
				$"Got {suggestions.Data?.Length ?? 0} results for query \"{suggestions.Query}\".");
			if (suggestions.Data == null)
				return;

			foreach (var s in suggestions.Data)
				Console.WriteLine(itemToString(s));
		}

		private const string TestQuery = "jack";
	}
}
