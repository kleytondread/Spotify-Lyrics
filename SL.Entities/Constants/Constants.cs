using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SL.Entities.Constants;

public static class Constants
{
	public const string BaseAdress = "https://localhost:7188/";
	public const string ClientID = "5b4d3c40776e4b39ba67629f44c1c00a";
	public const string RedirectHome = "https://localhost:7188/home";
	public const string ContentTypeEncoded = "application/x-www-form-urlencoded";
	public const string Bearer = "Bearer";
	public const string AuthorizationCode = "authorization_code";
	public const string RefreshTokenType= "refresh_token";
	public const string CodeVerifierFilePath = @"C:\Users\kleyt\OneDrive\Desktop\Kleyton\Documents\CodeVerifier.txt";
	public const string AccessTokenFilePath = @"C:\Users\kleyt\OneDrive\Desktop\Kleyton\Documents\AccessToken.txt";

	#region Scopes (Permissions)

	/// <summary>
	/// Description:		Read access to a user’s player state.
	/// Visible to users:	Read your currently playing content and Spotify Connect devices information.
	/// </summary>
	public const string PermissionReadPlayerState = "user-read-playback-state";

	/// <summary>
	/// Description:		Write access to a user’s playback state.
	/// Visible to users:	Control playback on your Spotify clients and Spotify Connect devices.
	/// </summary>
	public const string PermissionModifyPlayerState = "user-modify-playback-state";

	/// <summary>
	/// Description:		Read access to a user’s currently playing content.
	/// Visible to users:	Read your currently playing content.
	/// </summary>
	public const string PermissionReadCurrentlyPlayingAndQueue = "user-read-currently-playing";

	/// <summary>
	/// Description:		Read access to user's private playlists.
	/// Visible to users:	Access your private playlists.
	/// </summary>
	public const string PermissionReadPlaylist = "playlist-read-private";

	/// <summary>
	/// Description:		Read access to a user’s recently played tracks.
	/// Visible to users:	Access your recently played items.
	/// </summary>
	public const string PermissionReadRecentlyPlayedTracks = "user-read-recently-played";

	/// <summary>
	/// Description:		Read access to a user's library.
	/// Visible to users:	Access your saved content.
	/// </summary>
	public const string PermissionReadUserLibrary = "user-library-read";

	/// <summary>
	/// Description:		Read access to a user's top artists and tracks.
	/// Visible to users:	Read your top artists and content.
	/// </summary>
	public const string PermissionReadTopTracksAndArtists = "user-top-read";

	/// <summary>
	/// Description:		Read access to user’s subscription details (type of user account).
	/// Visible to users:	Access your subscription details.
	/// </summary>
	public const string PermissionReadUserPrivateDetails = "user-read-private user-read-email";

	#endregion

	public const string SpotifyAuthorization = "https://accounts.spotify.com/authorize?";
	public const string SpotifyTokenAccess = "https://accounts.spotify.com/api/token";

	private const string SpotifyApi = "https://api.spotify.com/v1";

	public const string EndpointUserProfile = SpotifyApi + "/me";
	public const string EndpointCurrentlyPlaying = SpotifyApi + "/me/player/currently-playing";
	public const string EndpointSkipToNext = SpotifyApi + "/me/player/next";
	public const string EndpointSkipToPrevious = SpotifyApi + "/me/player/previous";
	public const string EndpointPausePlayback = SpotifyApi + "/me/player/pause";
	public const string EndpointResumePlayback = SpotifyApi + "/me/player/play";
	public const string EndpointChangeVolume = SpotifyApi + "/me/player/volume?volume_percent={0}";
	public const string EndpointSeekPosition = SpotifyApi + "/me/player/seek?position_ms={0}";
}
