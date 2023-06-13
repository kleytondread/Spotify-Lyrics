using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Entities.Entities;

public class Player
{
	public Device Device { get; set; }
	public string Repeat_state { get; set; }
	public bool Shuffle_state { get; set; }
	public Context Context { get; set; }
	public string Timestamp { get; set; }
	public string Progress_ms { get; set; }
	public bool Is_playing { get; set; }
	public Song Item { get; set; }
	public string Currently_playing_type { get; set; }
	public Actions Actions { get; set; }
}
