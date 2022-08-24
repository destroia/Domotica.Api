using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Intarfaces
{
    public interface ILugarData
    {
        Task<Lugar> Post(Lugar cuenta);
        Task<Lugar> Update(Lugar cuenta);
        Task<Lugar> Get(Lugar cuenta);
        Task<bool> Delete(Lugar cuenta);
    }
}
