namespace ImdbNet.Core
{
	internal static class RegexPatterns
	{
		public static class Names
		{
			public const string Name = @"<meta property=[\'\""]og:title[\'\""] content=[\'\""](.*)[\'\""]\s?\/>";
			public const string ActorRoles = @"<div class=""filmo\-row.*id\=""actor\-(?<titleId>tt\d+)"">[\s.<>\w\=""\/\&\;]*<a href=""[\w.\/\?\=]*""\s*>(?<title>[\w\s\-\(\)\d]*)";
		}


		public static class Titles
		{
			public const string Name = @"<meta property=[\'\""]og:title[\'\""] content=[\'\""](.*)[\'\""]\s?\/>";
		}
	}
}
