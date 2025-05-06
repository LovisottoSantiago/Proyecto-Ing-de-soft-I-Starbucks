using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ing_Soft.Models;

public partial class OrdenCompra
{
    [Key] public int ID_OrdenCompra { get; set; }

    public DateTime? Fecha { get; set; }

    [ForeignKey("Proveedor")] public int? ID_Proveedor { get; set; }

    public decimal? Total { get; set; }

    public virtual ICollection<DetalleOrdenCompra> DetalleOrdenCompras { get; set; } = new List<DetalleOrdenCompra>();

    [ForeignKey(nameof(ID_Proveedor))]
    public virtual Proveedor? ID_ProveedorNavigation { get; set; }
}
