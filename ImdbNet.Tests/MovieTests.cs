using System;
using ImdbNet.Core;
using NUnit.Framework;


namespace ImdbNet.Tests
{
	[TestFixture]
	public class MovieTests
	{
		[Test]
		public void TestGetTitle()
		{
			var item = GetRandomMovieTestItem();
			Console.WriteLine($"Testing item: {item}");
			var movie = Utils.GetMovie(item.id);
			Assert.NotNull(movie, "Result should return a value.");
			Assert.IsTrue(string.Equals(item.title, movie.Name),
				$"Returned title is \"{movie.Name}\", expected \"{item.title}\"");
			Assert.IsNotEmpty(movie.Casts, "Movie cast should contain items.");
			foreach (var cast in movie.Casts)
				Console.WriteLine("\t{0}, {1}", cast.Person.Id, cast.Person.Name);
		}

		private static (string id, string url, string title) GetRandomMovieTestItem()
		{
			var rng = new Random(DateTime.Now.Millisecond);
			var i = rng.Next(Top50.Length);
			return Top50[i];
		}

		private static readonly (string id, string url, string title)[] Top50 =
		{
			("tt0111161", "https://www.imdb.com/title/tt0111161/", "The Shawshank Redemption"),
			("tt0068646", "https://www.imdb.com/title/tt0068646/", "The Godfather"),
			("tt0071562", "https://www.imdb.com/title/tt0071562/", "The Godfather: Part II"),
			("tt0468569", "https://www.imdb.com/title/tt0468569/", "The Dark Knight"),
			("tt0050083", "https://www.imdb.com/title/tt0050083/", "12 Angry Men"),
			("tt0108052", "https://www.imdb.com/title/tt0108052/", "Schindler's List"),
			("tt0167260", "https://www.imdb.com/title/tt0167260/", "The Lord of the Rings: The Return of the King"),
			("tt0110912", "https://www.imdb.com/title/tt0110912/", "Pulp Fiction"),
			("tt0060196", "https://www.imdb.com/title/tt0060196/", "Il buono, il brutto, il cattivo"),
			("tt0137523", "https://www.imdb.com/title/tt0137523/", "Fight Club"),
			("tt0120737", "https://www.imdb.com/title/tt0120737/", "The Lord of the Rings: The Fellowship of the Ring"),
			("tt0109830", "https://www.imdb.com/title/tt0109830/", "Forrest Gump"),
			("tt0080684", "https://www.imdb.com/title/tt0080684/", "Star Wars: Episode V - The Empire Strikes Back"),
			("tt1375666", "https://www.imdb.com/title/tt1375666/", "Inception"),
			("tt0167261", "https://www.imdb.com/title/tt0167261/", "The Lord of the Rings: The Two Towers"),
			("tt0073486", "https://www.imdb.com/title/tt0073486/", "One Flew Over the Cuckoo's Nest"),
			("tt0099685", "https://www.imdb.com/title/tt0099685/", "Goodfellas"),
			("tt0133093", "https://www.imdb.com/title/tt0133093/", "The Matrix"),
			("tt0047478", "https://www.imdb.com/title/tt0047478/", "Shichinin no samurai"),
			("tt0114369", "https://www.imdb.com/title/tt0114369/", "Se7en"),
			("tt0317248", "https://www.imdb.com/title/tt0317248/", "Cidade de Deus"),
			("tt0076759", "https://www.imdb.com/title/tt0076759/", "Star Wars"),
			("tt0102926", "https://www.imdb.com/title/tt0102926/", "The Silence of the Lambs"),
			("tt0038650", "https://www.imdb.com/title/tt0038650/", "It's a Wonderful Life"),
			("tt0118799", "https://www.imdb.com/title/tt0118799/", "La vita è bella"),
			("tt4633694", "https://www.imdb.com/title/tt4633694/", "Spider-Man: Into the Spider-Verse"),
			("tt0114814", "https://www.imdb.com/title/tt0114814/", "The Usual Suspects"),
			("tt0245429", "https://www.imdb.com/title/tt0245429/", "Sen to Chihiro no kamikakushi"),
			("tt0120815", "https://www.imdb.com/title/tt0120815/", "Saving Private Ryan"),
			("tt0110413", "https://www.imdb.com/title/tt0110413/", "Léon"),
			("tt0120689", "https://www.imdb.com/title/tt0120689/", "The Green Mile"),
			("tt0816692", "https://www.imdb.com/title/tt0816692/", "Interstellar"),
			("tt0054215", "https://www.imdb.com/title/tt0054215/", "Psycho"),
			("tt0120586", "https://www.imdb.com/title/tt0120586/", "American History X"),
			("tt0021749", "https://www.imdb.com/title/tt0021749/", "City Lights"),
			("tt0064116", "https://www.imdb.com/title/tt0064116/", "C'era una volta il West"),
			("tt0034583", "https://www.imdb.com/title/tt0034583/", "Casablanca"),
			("tt0027977", "https://www.imdb.com/title/tt0027977/", "Modern Times"),
			("tt0253474", "https://www.imdb.com/title/tt0253474/", "The Pianist"),
			("tt1675434", "https://www.imdb.com/title/tt1675434/", "Intouchables"),
			("tt0407887", "https://www.imdb.com/title/tt0407887/", "The Departed"),
			("tt0088763", "https://www.imdb.com/title/tt0088763/", "Back to the Future"),
			("tt0103064", "https://www.imdb.com/title/tt0103064/", "Terminator 2: Judgment Day"),
			("tt2582802", "https://www.imdb.com/title/tt2582802/", "Whiplash"),
			("tt0047396", "https://www.imdb.com/title/tt0047396/", "Rear Window"),
			("tt0082971", "https://www.imdb.com/title/tt0082971/", "Raiders of the Lost Ark"),
			("tt0110357", "https://www.imdb.com/title/tt0110357/", "The Lion King"),
			("tt0172495", "https://www.imdb.com/title/tt0172495/", "Gladiator"),
			("tt0482571", "https://www.imdb.com/title/tt0482571/", "The Prestige"),
			("tt0078788", "https://www.imdb.com/title/tt0078788/", "Apocalypse Now")
		};
	}
}
