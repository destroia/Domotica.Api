using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Intarfaces
{
    public interface ILugarRegionData
    {
        Task<LugarRegion> Post(LugarRegion cuenta);
        Task<LugarRegion> Update(LugarRegion cuenta);
        Task<LugarRegion> Get(LugarRegion cuenta);
        Task<bool> Delete(LugarRegion cuenta);
    }
}
