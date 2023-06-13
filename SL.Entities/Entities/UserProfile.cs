using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Entities.Entities;

internal class UserProfile
{
	public string Country { get; set; }
	public string Display_name { get; set; }
	public string Email { get; set; }
	public External_Urls External_urls { get; set; }
	public Followers Followers { get; set; }
	public string Href { get; set; }
	public string Id { get; set; }
	public Image[] Images { get; set; }
	public string Product { get; set; }
	public string Type { get; set; }
	public string Uri { get; set; }
}
