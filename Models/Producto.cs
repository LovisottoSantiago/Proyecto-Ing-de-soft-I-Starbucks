using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ing_Soft.Models;

public partial class Producto
{
    [Key] public int ID_Producto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public decimal? PrecioCosto { get; set; }  // <-- NUEVO

    public int? Stock { get; set; }

    public bool? Estado { get; set; }

    public string? ImagenUrl { get; set; }

    public string? Categoria { get; set; }

    [ForeignKey("Proveedor")]
    public int? ID_Proveedor { get; set; } // <-- NUEVO

    public virtual Proveedor? Proveedor { get; set; } // <-- NUEVO

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual ICollection<DetalleOrdenCompra> DetalleOrdenCompras { get; set; } = new List<DetalleOrdenCompra>();
}
