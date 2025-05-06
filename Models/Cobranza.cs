using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ing_Soft.Models;

public partial class Cobranza
{
    [Key] public int ID_Cobranza { get; set; }

    [ForeignKey("Factura")] public int? ID_Factura { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? Monto { get; set; }

    [ForeignKey("FormaPago")] public int? ID_FormaPago { get; set; }

    [ForeignKey(nameof(ID_Factura))]
    public virtual Factura? ID_FacturaNavigation { get; set; }

    [ForeignKey(nameof(ID_FormaPago))]
    public virtual FormaPago? ID_FormaPagoNavigation { get; set; }
}
