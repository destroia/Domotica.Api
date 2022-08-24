using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data
{
    public class SparkDBContext : DbContext
    {
        public SparkDBContext(DbContextOptions<SparkDBContext> options) : base(options)
        {

        }

        public SparkDBContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
            "Data Source=.;Initial Catalog=SparkDB;Integrated Security=true"

             );
            base.OnConfiguring(optionsBuilder);
            //Primer Migracion   Add-Migration InitialCreate
            //Despues de la primera migracion se utiliza  Update-Database

            
        }
        [NotMapped]
        public DbQuery<DispositivoQuery> DispositivosQuery { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Dispositivo> Dispositivos { get; set; }
        public DbSet<Lugar> Lugares { get; set; }
        public DbSet<LugarRegion> LugarRegiones { get; set; }
    }
}
