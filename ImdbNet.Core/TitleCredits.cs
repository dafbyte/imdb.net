using System.Collections.Generic;


namespace ImdbNet.Core
{
	public class TitleCredits
	{
		public readonly Dictionary<string, string> Cast = new Dictionary<string, string>();
		public readonly Dictionary<string, string> Directors = new Dictionary<string, string>();
		public readonly Dictionary<string, string> Writers = new Dictionary<string, string>();
		public readonly Dictionary<string, string> Producers = new Dictionary<string, string>();
		public readonly Dictionary<string, string> Musicians = new Dictionary<string, string>();
		public readonly Dictionary<string, string> Cinematographers = new Dictionary<string, string>();
		public readonly Dictionary<string, string> Editors = new Dictionary<string, string>();
		public readonly Dictionary<string, string> CastingBy = new Dictionary<string, string>();
		public readonly Dictionary<string, string> ProductionDesigners = new Dictionary<string, string>();
		public readonly Dictionary<string, string> ArtDirectors = new Dictionary<string, string>();
		public readonly Dictionary<string, string> SetDecorators = new Dictionary<string, string>();
		public readonly Dictionary<string, string> CustomDesigners = new Dictionary<string, string>();
		public readonly Dictionary<string, string> MakeupDepartment = new Dictionary<string, string>();
		public readonly Dictionary<string, string> ProductionManagers = new Dictionary<string, string>();
		public readonly Dictionary<string, string> SecondUnitDirectorsAndAssistantDirectors = new Dictionary<string, string>();
		public readonly Dictionary<string, string> ArtDepartment = new Dictionary<string, string>();
		public readonly Dictionary<string, string> SoundDepartment = new Dictionary<string, string>();
		public readonly Dictionary<string, string> SpecialEffectsCrew = new Dictionary<string, string>();
		public readonly Dictionary<string, string> VisualEffectsCrew = new Dictionary<string, string>();
		public readonly Dictionary<string, string> StuntsCrew = new Dictionary<string, string>();
		public readonly Dictionary<string, string> CameraAndElectricalCrew = new Dictionary<string, string>();
		public readonly Dictionary<string, string> CastingDepartment = new Dictionary<string, string>();
		public readonly Dictionary<string, string> CostumeAndWardrobeDepartment = new Dictionary<string, string>();
		public readonly Dictionary<string, string> EditorialDepartment = new Dictionary<string, string>();
		public readonly Dictionary<string, string> LocationManagement = new Dictionary<string, string>();
		public readonly Dictionary<string, string> MusicDepartment = new Dictionary<string, string>();
		public readonly Dictionary<string, string> TransportationDepartment = new Dictionary<string, string>();
		public readonly Dictionary<string, string> OtherCrew = new Dictionary<string, string>();
		public readonly Dictionary<string, string> Thanks = new Dictionary<string, string>();

		public TitleCredits(string id, string title)
		{
			Id = id;
			Title = title;
		}

		public string Title { get; }

		public string Id { get; }

		public string Type { get; set; }

		public string PosterImageUrl { get; set; }
	}
}
