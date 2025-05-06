using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ing_Soft.Data;
using Ing_Soft.Models;

namespace Ing_Soft.Controllers
{
    public class DetalleOrdenCompraController : Controller
    {
        private readonly AppDbContext _context;

        public DetalleOrdenCompraController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DetalleOrdenCompra
        public async Task<IActionResult> Index()
        {
            return View(await _context.DetalleOrdenCompra.ToListAsync());
        }

        // GET: DetalleOrdenCompra/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleOrdenCompra = await _context.DetalleOrdenCompra
                .FirstOrDefaultAsync(m => m.ID_DetalleOrdenCompra == id);
            if (detalleOrdenCompra == null)
            {
                return NotFound();
            }

            return View(detalleOrdenCompra);
        }

        // GET: DetalleOrdenCompra/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetalleOrdenCompra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_DetalleOrdenCompra,ID_OrdenCompra,ID_Producto,Cantidad,PrecioUnitario")] DetalleOrdenCompra detalleOrdenCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleOrdenCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detalleOrdenCompra);
        }

        // GET: DetalleOrdenCompra/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleOrdenCompra = await _context.DetalleOrdenCompra.FindAsync(id);
            if (detalleOrdenCompra == null)
            {
                return NotFound();
            }
            return View(detalleOrdenCompra);
        }

        // POST: DetalleOrdenCompra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_DetalleOrdenCompra,ID_OrdenCompra,ID_Producto,Cantidad,PrecioUnitario")] DetalleOrdenCompra detalleOrdenCompra)
        {
            if (id != detalleOrdenCompra.ID_DetalleOrdenCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleOrdenCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleOrdenCompraExists(detalleOrdenCompra.ID_DetalleOrdenCompra))
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
            return View(detalleOrdenCompra);
        }

        // GET: DetalleOrdenCompra/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleOrdenCompra = await _context.DetalleOrdenCompra
                .FirstOrDefaultAsync(m => m.ID_DetalleOrdenCompra == id);
            if (detalleOrdenCompra == null)
            {
                return NotFound();
            }

            return View(detalleOrdenCompra);
        }

        // POST: DetalleOrdenCompra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleOrdenCompra = await _context.DetalleOrdenCompra.FindAsync(id);
            if (detalleOrdenCompra != null)
            {
                _context.DetalleOrdenCompra.Remove(detalleOrdenCompra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleOrdenCompraExists(int id)
        {
            return _context.DetalleOrdenCompra.Any(e => e.ID_DetalleOrdenCompra == id);
        }
    }
}
