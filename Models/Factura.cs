using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ing_Soft.Models;

public partial class Factura
{
    [Key] public int ID_Factura { get; set; }

    public DateTime? Fecha { get; set; }

    [ForeignKey("Cliente")] public int? ID_Cliente { get; set; }

    public decimal? Total { get; set; }

    [ForeignKey("FormaPago")] public int? ID_FormaPago { get; set; }

    public virtual ICollection<Cobranza> Cobranzas { get; set; } = new List<Cobranza>();

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    [ForeignKey(nameof(ID_Cliente))]
    public virtual Cliente? ID_ClienteNavigation { get; set; }

    [ForeignKey(nameof(ID_FormaPago))]
    public virtual FormaPago? ID_FormaPagoNavigation { get; set; }
}
