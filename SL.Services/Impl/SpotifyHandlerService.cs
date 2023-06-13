using SL.Entities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace SL.Services.Impl;

internal static class SpotifyHandlerService
{
	internal static string StartAuthorization()
	{
		//var codeVerifier = GenerateRandomString(124);
		var codeVerifier = "FU40kEXEhiIZ8ZGDYvYRVfixMXzeEB7dhuYHo0XPBltftDkQbUXp5yXlwHoNP3Bglq3PBAaiYKQvwDEKykEvpTitYyViNLBIlO4EKWQc5zRwimztEfaq0Q2TIK1N";
		SaveCodeVerifier(codeVerifier);

		var codeChallenge = GenerateCodeChallenge(codeVerifier);
		var state = GenerateRandomString(16);

		var httpQuery = CreateQueryParams(codeChallenge, state);

		return SpotifyAuthorization + httpQuery;
	}

	internal static string GetCodeVerifier()
	{
		return GetFile(CodeVerifierFilePath);
	}

	internal static string GetAccessToken()
	{
		return GetFile(AccessTokenFilePath);
	}

	internal static void SaveFile(string stringData, string filePath)
	{
		var file = File.CreateText(filePath);
		file.WriteLine(stringData);
		file.Close();
	}

	internal static string GetFile(string filePath)
	{
		var fileData = File.ReadAllLines(filePath);
		var data = ConvertStringsIntoSingleString(fileData);

		if (string.IsNullOrEmpty(data))
		{
			throw new ArgumentNullException("Access token is null or invalid");
		}

		return data;
	}

	private static string ConvertStringsIntoSingleString(string[] fileData)
	{
		var stringBuilder = new StringBuilder();
		foreach (var line in fileData)
		{
			stringBuilder.AppendLine(line);
		}

		return stringBuilder.ToString();
	}

	#region Auxiliar Methods
	private static string GenerateRandomString(int length)
	{
		var text = string.Empty;
		var baseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		var random = new Random(DateTime.Now.Millisecond);

		for (int index = 0; index < length; index++)
		{
			text += baseCharacters.ElementAt(random.Next() % baseCharacters.Length);
		}

		return text;
	}

	private static string GenerateCodeChallenge(string codeVerifier)
	{
		using var sha256 = SHA256.Create();
		var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
		var b64Hash = Convert.ToBase64String(hash);
		var code = Regex.Replace(b64Hash, "\\+", "-");
		code = Regex.Replace(code, "\\/", "_");
		code = Regex.Replace(code, "=+$", "");
		return code;
	}

	private static void SaveCodeVerifier(string codeVerifier)
	{
		SaveFile(codeVerifier, CodeVerifierFilePath);
	}

	private static string CreateQueryParams(string codeChallenge, string state)
	{
		var scope = string.Join(" ",
							PermissionReadPlayerState,
							PermissionReadCurrentlyPlayingAndQueue,
							PermissionReadUserPrivateDetails,
							PermissionModifyPlayerState,
							PermissionReadRecentlyPlayedTracks,
							PermissionReadTopTracksAndArtists
							);


		var queryParams = HttpUtility.ParseQueryString(string.Empty);
		queryParams.Add("response_type", "code");
		queryParams.Add("client_id", ClientID);
		queryParams.Add("scope", scope);
		queryParams.Add("redirect_uri", RedirectHome);
		queryParams.Add("state", state);
		queryParams.Add("code_challenge_method", "S256");
		queryParams.Add("code_challenge", codeChallenge);

		return queryParams.ToString()!;
	}

	#endregion
}
