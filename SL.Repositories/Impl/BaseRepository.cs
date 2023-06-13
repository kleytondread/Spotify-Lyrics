using SL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Repositories.Impl
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public IList<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public IList<T> FindByAlbumName(string albumName)
        {
            throw new NotImplementedException();
        }

        public T FindByKey(int key)
        {
            throw new NotImplementedException();
        }

        public IList<T> FindByKeys(int[] keys)
        {
            throw new NotImplementedException();
        }

        public IList<T> FindSongByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<T> FindSongsBySingerOrBandName(string SingerOrBandName)
        {
            throw new NotImplementedException();
        }
    }
}
