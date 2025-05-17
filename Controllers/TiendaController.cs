using Ing_Soft.Data;
using Microsoft.AspNetCore.Mvc;

namespace Ing_Soft.Controllers
{
    public class TiendaController : Controller
    {
        private readonly AppDbContext _context;

        public TiendaController(AppDbContext context)
        {
            _context = context;
        }


        // Métodos para implementar:

        // Index() → muestra todos los productos disponibles para comprar.

        // AgregarAlCarrito(int id) → agrega un producto al carrito.

        // Carrito() → muestra los productos agregados.

        // FinalizarCompra() → realiza la compra de los productos en el carrito, tiene relacion con DetalleFactura.



    }




}