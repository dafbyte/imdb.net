using System.Collections.Generic;


namespace ImdbNet.Core.Models
{
	public class Movie
	{
		public Movie(string id, string name)
		{
			Id = id;
			Name = name;
		}

		public string Id { get; }
		public string Name { get; }
		public ICollection<MovieCast> Casts { get; set; }
		public ICollection<string> Images { get; set; }
	}


	public class MovieCast
	{
		public string Role { get; set; }
		public Person Person { get; set; }
	}
}
