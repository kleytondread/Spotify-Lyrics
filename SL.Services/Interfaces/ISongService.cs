using SL.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Services.Interfaces;

public interface ISongService
{
	Task<Player> GetCurrentPlayer();
	string GetSong();
	void StartOrResumePlayback();
	void PausePlayback();
	Task NextSong();
	Task PreviousSong();
	Task ChangeVolume(int volume);
	Task GoToPosition(int timeMiliseconds);
}
