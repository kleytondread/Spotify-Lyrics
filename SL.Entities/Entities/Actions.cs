namespace SL.Entities.Entities
{
	public class Actions
	{
		public bool interrupting_playback { get; set; }
		public bool pausing { get; set; }
		public bool resuming { get; set; }
		public bool seeking { get; set; }
		public bool skipping_next { get; set; }
		public bool skipping_prev { get; set; }
		public bool toggling_repeat_context { get; set; }
		public bool toggling_shuffle { get; set; }
		public bool toggling_repeat_track { get; set; }
		public bool transferring_playback { get; set; }
	}

}
