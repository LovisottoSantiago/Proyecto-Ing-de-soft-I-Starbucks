using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ing_Soft.Models;

public partial class FormaPago
{
    [Key] public int ID_FormaPago { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Cobranza> Cobranzas { get; set; } = new List<Cobranza>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
