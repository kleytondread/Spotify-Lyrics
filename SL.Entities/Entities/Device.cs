using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Entities.Entities
{
	public class Device
	{
		public string Id { get; set; }
		public bool Is_active { get; set; }
		public bool Is_private_session { get; set; }
		public bool Is_restricted { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Volume_percent { get; set; }
	}
}
