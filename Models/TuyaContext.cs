using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestTuya
{
    public class TuyaContext : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<DescripcionFactura> DescripcionFactura { get; set; }

        // The following configures EF to create a Sqlite database file as `C:\blogging.db`.
        // For Mac or Linux, change this to `/tmp/blogging.db` or any other absolute path.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        
            => options.UseSqlServer(@"Server=localhost;Database=dbtuya;User Id=sa;Password=jose.123;");
    }
}