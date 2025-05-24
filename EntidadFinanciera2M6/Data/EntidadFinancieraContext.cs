using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadFinanciera2M6.Models;

namespace EntidadFinanciera2M6.Data
{
    public class EntidadFinancieraContext : DbContext
    {
        public DbSet <Cliente> Clientes { get; set; }
        public DbSet <Cuenta> Cuentas { get; set;}
        public DbSet   <Transaccion>  Transacciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer (@"Server = AIDANKELLY\SQLEXPRESS; database = EntidadFinanciera2M6; trusted_connection = true; trustserverCertificate = true;");
        }
        // Filtros globales
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity <Cuenta>().HasQueryFilter (c => c.Activa);
       }
    }
}
