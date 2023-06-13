using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        IList<T> FindAll();
        T FindByKey(int key);
        IList<T> FindByKeys(int[] keys);
        IList<T> FindSongByName(string name);
        IList<T> FindSongsBySingerOrBandName(string SingerOrBandName);
        IList<T> FindByAlbumName(string albumName);
    }
}
