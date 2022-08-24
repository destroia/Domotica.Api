using Data.Intarfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepoData
{
    public class LugarData : ILugarData
    {
        public Task<bool> Delete(Lugar cuenta)
        {
            throw new NotImplementedException();
        }

        public Task<Lugar> Get(Lugar cuenta)
        {
            throw new NotImplementedException();
        }

        public Task<Lugar> Post(Lugar cuenta)
        {
            throw new NotImplementedException();
        }

        public Task<Lugar> Update(Lugar cuenta)
        {
            throw new NotImplementedException();
        }
    }
}
