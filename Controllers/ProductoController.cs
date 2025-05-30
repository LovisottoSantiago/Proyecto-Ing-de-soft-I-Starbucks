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
    // [RolAuthorize("Administrador")]
    public class ProductoController : Controller
    {
        private readonly AppDbContext _context;

        public ProductoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Producto
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var productosConProveedor = _context.Producto.Include(p => p.Proveedor);
            return View(await productosConProveedor.ToListAsync());
        }

        // GET: Producto/Details/5
        [RolAuthorize("Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.ID_Producto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Producto/Create
        [RolAuthorize("Administrador")]
        public IActionResult Create()
        {
            ViewData["ID_Proveedor"] = new SelectList(_context.Proveedor, "ID_Proveedor", "Nombre");
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RolAuthorize("Administrador")]
        public async Task<IActionResult> Create([Bind("ID_Producto,Nombre,Descripcion,PrecioCosto,Stock,Estado,ImagenUrl,Categoria,ID_Proveedor")] Producto producto)
        {
            producto.PrecioUnitario = producto.PrecioCosto * 2.2m;

            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Producto/Edit/5
        [RolAuthorize("Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
                return NotFound();

            // Cargar lista de proveedores para el dropdown
            ViewData["ID_Proveedor"] = new SelectList(_context.Proveedor, "ID_Proveedor", "Nombre", producto.ID_Proveedor);

            return View(producto);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RolAuthorize("Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Producto,Nombre,Descripcion,PrecioCosto,Stock,Estado,ImagenUrl,Categoria,ID_Proveedor")] Producto producto)
        {
            if (id != producto.ID_Producto)
            {
                return NotFound();
            }

            // Calculamos el PrecioUnitario desde el PrecioCosto recibido
            producto.PrecioUnitario = producto.PrecioCosto * 2.2m;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.ID_Producto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Producto/Delete/5
        [RolAuthorize("Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.ID_Producto == id);
            if (producto == null)
            {
                return NotFound();
            }

            ViewData["ID_Proveedor"] = new SelectList(_context.Proveedor, "ID_Proveedor", "Nombre", producto.ID_Proveedor);
            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RolAuthorize("Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [RolAuthorize("Administrador")]
        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.ID_Producto == id);
        }
        


        // POST: Producto/ToggleEstado/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RolAuthorize("Administrador")]
        public async Task<IActionResult> ToggleEstado(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            // Cambia el estado al valor contrario
            producto.Estado = !producto.Estado;

            _context.Update(producto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



    }
}
