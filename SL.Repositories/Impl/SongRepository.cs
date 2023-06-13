using SL.Entities.Entities;
using SL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Repositories.Impl
{
    public class SongRepository : BaseRepository<Player>, ISongRepository
    {
        public string teste()
        {
            return "teste bateu no repositorio";
        }
    }
}
