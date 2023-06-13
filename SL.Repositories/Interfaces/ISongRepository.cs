using SL.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Repositories.Interfaces
{
    public interface ISongRepository : IBaseRepository<Player>
    {
        string teste();
    }
}
