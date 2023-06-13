using SL.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Services.Interfaces;

public interface ISpotifyOAuthService
{
	string StartSpotifyAuthorization();
	void FinalizeAuthentication(string code, string state);
	IList<KeyValuePair<string, string>> CreateTokenAccessBody(string code, string state);
	void SaveAccessToken(SpotifyToken accessToken);
	Task<string> GetAccessToken();
	string GetCodeVerifier();
}
