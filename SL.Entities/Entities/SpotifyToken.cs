using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Entities.Entities;

public class SpotifyToken
{
	public string Access_token { get; set; }
	public string Token_type { get; set; }
	public int Expires_in { get; set; }
	public string Refresh_token { get; set; }
	public string Scope { get; set; }

	public DateTime RecievedTime { get; set; }
	public DateTime	ExpireTime { get; set; }
	public bool IsExpired => IsTokenExpired();
	


	public void Configure()
	{
		RecievedTime = DateTime.Now;
		ExpireTime = ExpirationTime();
	}

	private bool IsTokenExpired()
	{
		return DateTime.Now >= ExpireTime;
	}

	private DateTime ExpirationTime()
	{
		return RecievedTime.AddSeconds(Expires_in);
	}
}
