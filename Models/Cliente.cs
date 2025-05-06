using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ing_Soft.Models;

public partial class Cliente
{
    [Key] public int ID_Cliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
