namespace SL.Entities.Entities;

public class Song
{
	public Album Album { get; set; }
	public ArtistProfile[] Artists { get; set; }
	public string[] Available_markets { get; set; }
	public string Disc_number { get; set; }
	public string Duration_ms { get; set; }
	public bool Explicit { get; set; }
	public External_Ids External_ids { get; set; }
	public External_Urls External_urls { get; set; }
	public string Href { get; set; }
	public string Id { get; set; }
	public bool Is_playable { get; set; }
	public string name { get; set; }
	public string Popularity { get; set; }
	public string Preview_url { get; set; }
	public string Track_number { get; set; }
	public string Type { get; set; }
	public string Uri { get; set; }
	public bool Is_local { get; set; }
}
