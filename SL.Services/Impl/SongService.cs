using Newtonsoft.Json;
using SL.Entities.Entities;
using SL.Repositories.Interfaces;
using SL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SL.Services.Impl;

public class SongService : ISongService
{
    private readonly ISongRepository _songRepository;
    private readonly ISpotifyOAuthService _spotifyOAuth;
	private readonly IHttpClientFactory _httpClientFactory;

	public SongService(ISongRepository songRepository, ISpotifyOAuthService spotifyOAuth, IHttpClientFactory httpClientFactory)
	{
		_songRepository = songRepository;
		_spotifyOAuth = spotifyOAuth;
		_httpClientFactory = httpClientFactory;
	}

	public async Task<Player> GetCurrentPlayer()
	{
		var player = await GetPlayerStatus(EndpointCurrentlyPlaying);
		if(player is null)
		{
			player = CreateNewPlayerInstance();
		}
		return player!;
	}

	public string GetSong()
        {
            return _songRepository.teste();
        }

	public async void PausePlayback()
	{
		var endpoint = EndpointPausePlayback;
		await SendPutRequest(endpoint);
	}

	public async void StartOrResumePlayback()
	{
		var endpoint =  EndpointResumePlayback;
		await SendPutRequest(endpoint);
	}

	public async Task NextSong()
	{
		var endpoint =  EndpointSkipToNext;
		await SendPostRequest(endpoint);
	}

	public async Task PreviousSong()
	{
		var endpoint = EndpointSkipToPrevious;
		await SendPostRequest(endpoint);
	}

	public async Task ChangeVolume(int volume)
	{
		var endpoint = string.Format(EndpointChangeVolume, volume);
		await SendPostRequest(endpoint);
	}

	public async Task GoToPosition(int timeMiliseconds)
	{
		var endpoint = string.Format(EndpointSeekPosition, timeMiliseconds);
		await SendPutRequest(endpoint);
	}

	private async Task SendPutRequest(string endpoint)
	{
		var httpClient = await GetConfiguredHttpClient();
		var response = await httpClient.PutAsync(endpoint, null);

		response.EnsureSuccessStatusCode();
	}

	private async Task SendPostRequest(string endpoint)
	{
		var httpClient = await GetConfiguredHttpClient();
		var response = await httpClient.PostAsync(endpoint, null);

		response.EnsureSuccessStatusCode();
	}

	private async Task<Player?> GetPlayerStatus(string endPoint)
	{
		var httpClient = await GetConfiguredHttpClient();

		var response = await httpClient.GetAsync(endPoint);
		response.EnsureSuccessStatusCode();
		var responseBody = await response.Content.ReadAsStringAsync();

		var player = JsonConvert.DeserializeObject<Player>(responseBody);

		return player;
	}

	private async Task<HttpClient> GetConfiguredHttpClient()
	{
		var accessToken = await GetAccessToken();
		var authentication = new AuthenticationHeaderValue(Bearer, accessToken);

		var httpClient = _httpClientFactory.CreateClient();
		httpClient.DefaultRequestHeaders.Authorization = authentication;

		return httpClient;
	}

	private Task<string> GetAccessToken()
	{
		return _spotifyOAuth.GetAccessToken();
	}

	private Player CreateNewPlayerInstance()
	{
		var album = new Album()
		{
			Images = new Image[] { new Image { Url = string.Empty } }
		};
		var song = new Song()
		{
			name = "No song is current playing",
			Album = album
		};
		var player = new Player()
		{
			Is_playing = false,
			Item = song
		};

		return player;
	}
}
