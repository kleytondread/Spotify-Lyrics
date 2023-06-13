namespace SL.Entities.Entities
{
	public class ArtistProfile
	{
		public External_Urls external_urls { get; set; }
		public Followers followers { get; set; }
		public string[] genres { get; set; }
		public string href { get; set; }
		public string id { get; set; }
		public Image[] images { get; set; }
		public string name { get; set; }
		public string popularity { get; set; }
		public string type { get; set; }
		public string uri { get; set; }
	}

}
