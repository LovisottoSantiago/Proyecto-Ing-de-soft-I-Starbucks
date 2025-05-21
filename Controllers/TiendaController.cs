using Ing_Soft.Data;
using Ing_Soft.Models;
using Microsoft.AspNetCore.Mvc;

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
                .ToList();
            return View(productos);
        }


        // Métodos para implementar:

        // Index() → muestra todos los productos disponibles para comprar.

        // AgregarAlCarrito(int id) → agrega un producto al carrito.

        // Carrito() → muestra los productos agregados.

        // FinalizarCompra() → realiza la compra de los productos en el carrito, tiene relacion con DetalleFactura.


        [HttpPost]
        public IActionResult CarritoParcial(List<int>? ids)
        {
            var productos = ids != null
                ? _context.Producto.Where(p => ids.Contains(p.ID_Producto)).ToList()
                : new List<Producto>();

            return PartialView("CarritoParcial", productos);
        }





    }




}