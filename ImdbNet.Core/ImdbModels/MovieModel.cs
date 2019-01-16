using System;
using Newtonsoft.Json;


namespace ImdbNet.Core.ImdbModels
{
	internal class BaseTypeEntity
	{
		[JsonProperty("@type")]
		public string Type { get; set; }
	}


	internal class BaseNameEntity : BaseTypeEntity
	{
		[JsonProperty("name")]
		public string Name { get; set; }
	}


	internal class BaseResourceEntity : BaseNameEntity
	{
		[JsonProperty("url")]
		public string Url { get; set; }
	}


	internal class MovieModel : BaseResourceEntity
	{
		[JsonProperty("@context")]
		public string Context { get; set; }

		[JsonProperty("image")]
		public string Image { get; set; }

		[JsonProperty("genre")]
		public string[] Genre { get; set; }

		[JsonProperty("contentRating")]
		public string ContentRating { get; set; }

		[JsonProperty("actor")]
		public BaseResourceEntity[] Actors { get; set; }

		[JsonProperty("director")]
		public BaseResourceEntity Director { get; set; }

		[JsonProperty("creator")]
		public BaseResourceEntity[] Creators { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("datePublished")]
		public string DatePublished { get; set; }

		[JsonProperty("keywords")]
		public string Keywords { get; set; }

		[JsonProperty("aggregateRating")]
		public AggregateRatingModel AggregateRating { get; set; }

		[JsonProperty("review")]
		public ReviewModel Review { get; set; }

		[JsonProperty("duration")]
		public string Duration { get; set; }

		[JsonProperty("trailer")]
		public TrailerModel Trailer { get; set; }


		public class AggregateRatingModel : BaseTypeEntity
		{
			[JsonProperty("ratingCount")]
			public uint RatingCount { get; set; }

			[JsonProperty("bestRating")]
			public string BestRating { get; set; }

			[JsonProperty("worstRating")]
			public string WorstRating { get; set; }

			[JsonProperty("ratingValue")]
			public string RatingValue { get; set; }
		}


		public class ReviewModel
		{
			[JsonProperty("@type")]
			public string Type { get; set; }

			[JsonProperty("itemReviewed")]
			public ItemReviewedModel ItemReviewed { get; set; }

			[JsonProperty("author")]
			public AuthorModel Author { get; set; }

			[JsonProperty("dateCreated")]
			public string DateCreated { get; set; }

			[JsonProperty("inLanguage")]
			public string InLanguage { get; set; }

			[JsonProperty("name")]
			public string Mame { get; set; }

			[JsonProperty("reviewBody")]
			public string ReviewBody { get; set; }

			public class ItemReviewedModel
			{
				[JsonProperty("@type")]
				public string Type { get; set; }

				[JsonProperty("url")]
				public string Url { get; set; }
			}

			public class AuthorModel
			{
				[JsonProperty("@type")]
				public string Type { get; set; }

				[JsonProperty("name")]
				public string Name { get; set; }
			}
		}


		public class TrailerModel : BaseNameEntity
		{
			[JsonProperty("embedUrl")]
			public string EmbedUrl { get; set; }

			[JsonProperty("thumbnail")]
			public ThumbnailModel Thumbnail { get; set; }

			[JsonProperty("thumbnailUrl")]
			public string ThumbnailUrl { get; set; }

			[JsonProperty("description")]
			public string Description { get; set; }

			[JsonProperty("uploadDate")]
			public DateTime UploadDate { get; set; }

			public class ThumbnailModel
			{
				[JsonProperty("@type")]
				public string Type { get; set; }

				[JsonProperty("contentUrl")]
				public string Description { get; set; }
			}
		}
	}
}
