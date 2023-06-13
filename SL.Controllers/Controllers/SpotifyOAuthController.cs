using Microsoft.AspNetCore.Mvc;
using SL.Entities.Constants;
using SL.Entities.DTOs;
using SL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using System.Web;
using Newtonsoft.Json;
using SL.Entities.Entities;

namespace SL.Controllers.Controllers;

[ApiController]
[Route("[controller]")]
public  class SpotifyOAuthController : ControllerBase
{
	private readonly ISpotifyOAuthService _spotifyOAuth;
	public SpotifyOAuthController(ISpotifyOAuthService spotifyOAuth)
	{
		_spotifyOAuth = spotifyOAuth;
	}

	[HttpGet(Name = "StartAuthorization")]
	public string StartSpotifyAuthorization()
	{
		return _spotifyOAuth.StartSpotifyAuthorization();
	}

	[HttpPost(Name = "FinalizeAuthentication")]
	public async void FinalizeAuthentication(string code, string state)
	{
		//_spotifyOAuth.FinalizeAuthentication(code, state);

		using (var httpClient = new HttpClient())
		{

			var body = new FormUrlEncodedContent(_spotifyOAuth.CreateTokenAccessBody(code, state));
			body.Headers.ContentType = new MediaTypeHeaderValue(Constants.ContentTypeEncoded);


			var response = await httpClient.PostAsync(Constants.SpotifyTokenAccess, body);

			response.EnsureSuccessStatusCode();

			var responseBody = await response.Content.ReadAsStringAsync();
			var token = JsonConvert.DeserializeObject<SpotifyToken>(responseBody);

			token.Configure();
			_spotifyOAuth.SaveAccessToken(token);
		}

		#region just in case
		//funciona
		//var httpClient = new HttpClient();

		//var parameters = new Dictionary<string, string>
		//{
		//	{"client_id", Constants.ClientID},
		//	{"grant_type", "authorization_code"},
		//	{"code", code},
		//	{"redirect_uri", Constants.RedirectHome},
		//	{"code_verifier", _spotifyOAuth.GetCodeVerifier()}
		//};

		//var urlEncodedParameters = new FormUrlEncodedContent(parameters);
		//var request = new HttpRequestMessage(HttpMethod.Post, Constants.SpotifyTokenAccess) { Content = urlEncodedParameters };
		//var response = await httpClient.SendAsync(request);
		//response.EnsureSuccessStatusCode();
		//var responseBody = await response.Content.ReadAsStringAsync();
		//var json = JsonDocument.Parse(responseBody);
		//if (json.RootElement.TryGetProperty("access_token", out var accessToken))
		//{
		//	_spotifyOAuth.SaveAccessToken(accessToken.GetString());
		//}
		//else
		//{
		//	throw new InvalidOperationException("Access token not found in the response");
		//} 
		#endregion
	}

	[HttpGet(Name = "GetProfile")]
	public async Task<UserProfileDTO> GetProfile()
	{
		var accessToken = string.Empty;
		try
		{
			accessToken = await _spotifyOAuth.GetAccessToken();
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		using (var httpClient = new HttpClient())
		{
			var request = new HttpRequestMessage(HttpMethod.Get, Constants.EndpointUserProfile);
			request.Headers.Authorization = new AuthenticationHeaderValue(Constants.Bearer, accessToken);
			var response = await httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();
			var json = JsonDocument.Parse(responseBody);
			UserProfileDTO profile = await response.Content.ReadFromJsonAsync<UserProfileDTO>();

			return profile;
		}			
	}
}
