using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ing_Soft.Data;
using Ing_Soft.Models;
using Ing_Soft.Filters;
using Microsoft.AspNetCore.Authorization;

namespace Ing_Soft.Controllers
{
    public class TiendaController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public TiendaController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var productos = _context.Producto
                .Where(p => p.Estado == true)
                .ToList();
            return View(productos);
        }

        [HttpPost]
        public IActionResult CarritoParcial(List<int>? ids)
        {
            var productos = ids != null
                ? _context.Producto.Where(p => ids.Contains(p.ID_Producto)).ToList()
                : new List<Producto>();

            return PartialView("CarritoParcial", productos);
        }

        [RolAuthorize("Usuario")]
        [HttpPost]
        public async Task<IActionResult> RealizarVenta([FromBody] List<DetalleFactura> detallesEntrada)
        {
            if (detallesEntrada == null || !detallesEntrada.Any())
                return BadRequest("No se recibieron productos para la venta.");

            var idClienteSesion = HttpContext.Session.GetInt32("idCliente");
                if (!idClienteSesion.HasValue)
                {
                    return Unauthorized("Debe iniciar sesión para realizar la compra.");
                }

            var factura = new Factura
            {
                Fecha = DateTime.Now,
                ID_Cliente = idClienteSesion.Value,    // TODO: Reemplazar con el ID real del cliente autenticado
                ID_FormaPago = 1,     // TODO: Reemplazar con el ID real de la forma de pago
                DetalleFacturas = new List<DetalleFactura>()
            };

            decimal total = 0;

            foreach (var entrada in detallesEntrada)
            {
                var producto = await _context.Producto.FindAsync(entrada.ID_Producto);
                if (producto == null)
                    return NotFound($"Producto con ID {entrada.ID_Producto} no encontrado.");

                if (producto.Stock < entrada.Cantidad)
                    return BadRequest($"Stock insuficiente para el producto {producto.Nombre}.");

                // Restar stock
                producto.Stock -= entrada.Cantidad;

                var detalle = new DetalleFactura
                {
                    ID_Producto = producto.ID_Producto,
                    Cantidad = entrada.Cantidad,
                    PrecioUnitario = producto.PrecioUnitario
                };

                total += (decimal)((detalle.PrecioUnitario ?? 0) * detalle.Cantidad);

                factura.DetalleFacturas.Add(detalle);
            }

            factura.Total = total;

            _context.Factura.Add(factura);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Compra realizada con éxito.",
                idFactura = factura.ID_Factura,
                total = factura.Total
            });
        }





    }




}