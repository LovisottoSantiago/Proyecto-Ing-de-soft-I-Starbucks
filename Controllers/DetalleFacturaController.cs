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

namespace Ing_Soft.Controllers
{
    [RolAuthorize("Administrador")]
    public class DetalleFacturaController : Controller
    {
        private readonly AppDbContext _context;

        public DetalleFacturaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DetalleFactura
        public async Task<IActionResult> Index()
        {
            return View(await _context.DetalleFactura.ToListAsync());
        }

        // GET: DetalleFactura/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleFactura = await _context.DetalleFactura
                .FirstOrDefaultAsync(m => m.ID_DetalleFactura == id);
            if (detalleFactura == null)
            {
                return NotFound();
            }

            return View(detalleFactura);
        }

        // GET: DetalleFactura/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetalleFactura/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_DetalleFactura,ID_Factura,ID_Producto,Cantidad,PrecioUnitario")] DetalleFactura detalleFactura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleFactura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detalleFactura);
        }

        // GET: DetalleFactura/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleFactura = await _context.DetalleFactura.FindAsync(id);
            if (detalleFactura == null)
            {
                return NotFound();
            }
            return View(detalleFactura);
        }

        // POST: DetalleFactura/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_DetalleFactura,ID_Factura,ID_Producto,Cantidad,PrecioUnitario")] DetalleFactura detalleFactura)
        {
            if (id != detalleFactura.ID_DetalleFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleFactura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleFacturaExists(detalleFactura.ID_DetalleFactura))
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
            return View(detalleFactura);
        }

        // GET: DetalleFactura/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleFactura = await _context.DetalleFactura
                .FirstOrDefaultAsync(m => m.ID_DetalleFactura == id);
            if (detalleFactura == null)
            {
                return NotFound();
            }

            return View(detalleFactura);
        }

        // POST: DetalleFactura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleFactura = await _context.DetalleFactura.FindAsync(id);
            if (detalleFactura != null)
            {
                _context.DetalleFactura.Remove(detalleFactura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleFacturaExists(int id)
        {
            return _context.DetalleFactura.Any(e => e.ID_DetalleFactura == id);
        }
    }
}
