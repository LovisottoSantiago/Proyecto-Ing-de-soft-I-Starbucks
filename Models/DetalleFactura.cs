using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ing_Soft.Models;

public partial class DetalleFactura
{
    [Key] public int ID_DetalleFactura { get; set; }

    [ForeignKey("Factura")] public int? ID_Factura { get; set; }

    [ForeignKey("Producto")]  public int? ID_Producto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    [ForeignKey(nameof(ID_Factura))]
    public virtual Factura? ID_FacturaNavigation { get; set; }

    [ForeignKey(nameof(ID_Producto))]
    public virtual Producto? ID_ProductoNavigation { get; set; }
}
