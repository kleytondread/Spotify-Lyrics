using SL.Entities.Entities;
using SL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using SL.Services.Utils;

namespace SL.Services.Impl;

public class SpotifyOAuthService : ISpotifyOAuthService
{
	private readonly IHttpClientFactory _httpClientFactory;

	public SpotifyOAuthService(IHttpClientFactory httpClientFactory)
        {
		_httpClientFactory = httpClientFactory;
	}
    public string StartSpotifyAuthorization()
	{
		return SpotifyHandlerService.StartAuthorization();
	}

	public void FinalizeAuthentication(string code, string state)
	{
		//TODO
	}

	public IList<KeyValuePair<string, string>> CreateTokenAccessBody(string code, string state)
	{
		//var codeVerifier = "FU40kEXEhiIZ8ZGDYvYRVfixMXzeEB7dhuYHo0XPBltftDkQbUXp5yXlwHoNP3Bglq3PBAaiYKQvwDEKykEvpTitYyViNLBIlO4EKWQc5zRwimztEfaq0Q2TIK1N";

		var body = new List<KeyValuePair<string, string>>
		{
			new KeyValuePair<string, string>("grant_type", AuthorizationCode),
			new KeyValuePair<string, string>("code", code),
			new KeyValuePair<string, string>("redirect_uri", RedirectHome),
			new KeyValuePair<string, string>("client_id", ClientID),
			new KeyValuePair<string, string>("code_verifier", SpotifyHandlerService.GetCodeVerifier())
			//new KeyValuePair<string, string>("code_verifier", codeVerifier)
		};

		return body;
	}

	public void SaveAccessToken(SpotifyToken accessToken)
	{
		ArgumentNullException.ThrowIfNull(accessToken, "Access token is null or invalid");

		var tokenAsJson = JsonConvert.SerializeObject(accessToken);

		SpotifyHandlerService.SaveFile(tokenAsJson, AccessTokenFilePath); 
	}

	public async Task<string> GetAccessToken()
	{
		var accessToken = SpotifyHandlerService.GetAccessToken();

		var token = JsonConvert.DeserializeObject<SpotifyToken>(accessToken);

		if (token is not null && token.IsExpired)
		{
			token = await RefreshToken(token.Refresh_token);
		}

		ArgumentNullException.ThrowIfNull(token, "token is null");
		return token.Access_token;
	}

	private async Task<SpotifyToken> RefreshToken(string refresh_token)
	{
		var body = new FormUrlEncodedContent(CreateRefreshTokenBody(refresh_token));
		body.Headers.ContentType = new MediaTypeHeaderValue(ContentTypeEncoded);

		var httpClient = _httpClientFactory.CreateClient();


		HttpResponseMessage response = new HttpResponseMessage();
		var refresh = () => TryRefreshToken(httpClient, body, out response);
		Retry.Do(refresh, TimeSpan.FromSeconds(1));

		Task.WaitAll();
		//var response = await TryRefreshToken(httpClient, body);
		//var response = await httpClient.PostAsync(SpotifyTokenAccess, body);

		//response.EnsureSuccessStatusCode();

		var token = await ExtractNewTokenFromResponse(response);

		SaveAccessToken(token);

		return token;
	}

	private void TryRefreshToken(HttpClient httpClient, FormUrlEncodedContent body, out HttpResponseMessage response)
	{

		response = httpClient.PostAsync(SpotifyTokenAccess, body).Result;

		response.EnsureSuccessStatusCode();

		//int retryCount = 0;
		//bool retry = false;
		//HttpResponseMessage response = null;

		//do
		//{
		//	retry = false;
		//	try
		//	{
		//		response = await httpClient.PostAsync(SpotifyTokenAccess, body);
		//		if (response.IsSuccessStatusCode)
		//		{
		//			return response;
		//		}
		//		else if ((int)response.StatusCode >= 400 && retryCount < 3)
		//		{
		//			retry = true;
		//			retryCount++;

		//			// Delay before retrying (optional)
		//			await Task.Delay(TimeSpan.FromMilliseconds(500));
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//	}
		//} while (retry);

		//throw new Exception("Maximum retries exceeded.");
	}

	private static async Task<SpotifyToken> ExtractNewTokenFromResponse(HttpResponseMessage response)
	{
		var responseBody = await response.Content.ReadAsStringAsync();

		ArgumentNullException.ThrowIfNull(responseBody, string.Format(
			"Variable {0} in method {1} is null.",
			nameof(responseBody), nameof(ExtractNewTokenFromResponse)));

		var token = JsonConvert.DeserializeObject<SpotifyToken>(responseBody);
		ValidateAndConfigureToken(token);

		return token!;
	}

	private static void ValidateAndConfigureToken(SpotifyToken? token)
	{
		ArgumentNullException.ThrowIfNull(token, "token is null");
		token.Configure();
	}

	public IList<KeyValuePair<string, string>> CreateRefreshTokenBody(string refreshToken)
	{
		var body = new List<KeyValuePair<string, string>>
		{
			new KeyValuePair<string, string>("grant_type", RefreshTokenType),
			new KeyValuePair<string, string>(RefreshTokenType, refreshToken),
			new KeyValuePair<string, string>("client_id", ClientID)
		};

		return body;
	}

	public string GetCodeVerifier()
	{
		return SpotifyHandlerService.GetCodeVerifier();
	}
}
