using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Entities.Entities
{
	public class Album
	{
		public string Album_type { get; set; }
		public string Total_tracks { get; set; }
		public string[] Available_markets { get; set; }
		public External_Urls External_urls { get; set; }
		public string Href { get; set; }
		public string Id { get; set; }
		public Image[] Images { get; set; }
		public string Name { get; set; }
		public string Release_date { get; set; }
		public string Release_date_precision { get; set; }
		public Restrictions Restrictions { get; set; }
		public string Type { get; set; }
		public string Uri { get; set; }
		public External_Ids External_ids { get; set; }
		public string[] Genres { get; set; }
		public string Label { get; set; }
		public string Popularity { get; set; }
		public string Album_group { get; set; }
		public ArtistReference[] Artists { get; set; }
	}
}
