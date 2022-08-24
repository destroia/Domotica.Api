using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Lugar
    {
        public Guid Id { get; set; }
        public Guid CuentaId { get; set; }
        public string Nombre  { get; set; }
        [ForeignKey("LugarId")]
        public ICollection<LugarRegion> LugarRegiones { get; set; }
    }
}
