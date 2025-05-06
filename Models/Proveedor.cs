using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ing_Soft.Models;

public partial class Proveedor
{
    [Key] public int ID_Proveedor { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<OrdenCompra> OrdenCompras { get; set; } = new List<OrdenCompra>();
}
