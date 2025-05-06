using Ing_Soft.Models;
using Microsoft.EntityFrameworkCore;

namespace Ing_Soft.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Cliente> Cliente { get; set; }

        public virtual DbSet<Cobranza> Cobranza { get; set; }

        public virtual DbSet<DetalleFactura> DetalleFactura { get; set; }

        public virtual DbSet<DetalleOrdenCompra> DetalleOrdenCompra { get; set; }

        public virtual DbSet<Factura> Factura { get; set; }

        public virtual DbSet<FormaPago> FormaPago { get; set; }

        public virtual DbSet<OrdenCompra> OrdenCompra { get; set; }

        public virtual DbSet<Producto> Producto { get; set; }

        public virtual DbSet<Proveedor> Proveedor { get; set; }

    }
}
