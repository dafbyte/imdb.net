using System.Collections.Generic;


namespace ImdbNet.Core.Models
{
	public class Person
	{
		public readonly Dictionary<string, string> Filmography = new Dictionary<string, string>();

		public Person(string id, string name)
		{
			Id = id;
			Name = name;
		}

		public string Id { get; }
		public string Name { get; }
	}
}
