using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ing_Soft.Models;

public partial class DetalleOrdenCompra
{
    [Key] public int ID_DetalleOrdenCompra { get; set; }

    [ForeignKey("OrdenCompra")] public int? ID_OrdenCompra { get; set; }

    [ForeignKey("Producto")] public int? ID_Producto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    [ForeignKey(nameof(ID_OrdenCompra))]
    public virtual OrdenCompra? ID_OrdenCompraNavigation { get; set; }

    [ForeignKey(nameof(ID_Producto))]
    public virtual Producto? ID_ProductoNavigation { get; set; }
    
}
