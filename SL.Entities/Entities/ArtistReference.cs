using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Entities.Entities
{
	public class ArtistReference
	{
		public External_Urls External_urls { get; set; }
		public string Href { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Uri { get; set; }
	}
}
