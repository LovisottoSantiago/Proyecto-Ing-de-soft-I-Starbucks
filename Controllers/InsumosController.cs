using Ing_Soft.Data;
using Ing_Soft.Filters;
using Ing_Soft.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ing_Soft.Controllers
{
    [RolAuthorize("Administrador")]
    public class InsumosController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public InsumosController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var productos = _context.Producto
                .Include(p => p.Proveedor) // <--- ESTA LÍNEA ES CLAVE
                .ToList();
                
            var proveedores = productos
                .Where(p => p.Proveedor != null)
                .Select(p => p.Proveedor!)
                .DistinctBy(p => p.ID_Proveedor) 
                .ToList();
            ViewBag.Proveedores = proveedores;

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





    }




}