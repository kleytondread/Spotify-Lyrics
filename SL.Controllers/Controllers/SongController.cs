using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SL.Entities.Constants;
using SL.Entities.DTOs;
using SL.Entities.Entities;
using SL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SL.Controllers.Controllers;

[ApiController]
[Route("[controller]")]
public class SongController : ControllerBase
{
	private readonly ILogger<SongController> _logger;
	private readonly ISongService _songService;

	public SongController(ISongService songService, ILogger<SongController> logger)
	{
		_songService = songService;
		_logger = logger;
	}

	[HttpGet(Name = "GetCurrentSong")]
	public Task<Player> GetCurrentPlayer()
	{
		return _songService.GetCurrentPlayer(); 
	//var accessToken = await _songService.GetAccessToken();

	//using (var httpClient = new HttpClient())
	//{
	//	var request = new HttpRequestMessage(HttpMethod.Get, Constants.EndpointCurrentlyPlaying);
	//	request.Headers.Authorization = new AuthenticationHeaderValue(Constants.Bearer, accessToken);
	//	var response = await httpClient.SendAsync(request);
	//	response.EnsureSuccessStatusCode();
	//	var responseBody = await response.Content.ReadAsStringAsync();

	//	var song = JsonConvert.DeserializeObject<Song>(responseBody);

	//	if (song?.item?.name is null)
	//	{
	//		return "No song playing now";
	//	}
	//	return song.item.name;
	//}

	}

	public void ChangeSongStatus(bool IsCurrentlyPlaying)
	{
	if (IsCurrentlyPlaying)
	{
		_songService.PausePlayback();
	}
	else
	{
		_songService.StartOrResumePlayback();
	}
	}

	public async Task NextSong()
	{
	await _songService.NextSong();
	}

	public async Task PreviousSong()
	{
	await _songService.PreviousSong();
	}

	public async Task ChangeVolume(int volume)
	{
	await _songService.ChangeVolume(volume);
	}

	public async Task GoToPosition(int timeMiliseconds)
	{
	await _songService.GoToPosition(timeMiliseconds);
	}
}
