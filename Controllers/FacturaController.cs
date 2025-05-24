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
    public class FacturaController : Controller
    {
        private readonly AppDbContext _context;

        public FacturaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Factura
        public async Task<IActionResult> Index()
        {
            var facturas = await _context.Factura
                .Include(f => f.ID_FormaPagoNavigation)
                .Include(f => f.ID_ClienteNavigation)
                .OrderByDescending(f => f.Fecha)
                .ToListAsync();
            return View(facturas);
        }

        // GET: Factura/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura
                .FirstOrDefaultAsync(m => m.ID_Factura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Factura/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Factura/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Factura,Fecha,ID_Cliente,Total,ID_FormaPago")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(factura);
        }

        // GET: Factura/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            ViewData["ID_FormaPago"] = new SelectList(_context.FormaPago, "ID_FormaPago", "Descripcion", factura.ID_FormaPago);
            return View(factura);
        }

        // POST: Factura/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Factura,Fecha,ID_Cliente,Total,ID_FormaPago")] Factura factura)
        {
            if (id != factura.ID_Factura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.ID_Factura))
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
            return View(factura);
        }

        // GET: Factura/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura
                .FirstOrDefaultAsync(m => m.ID_Factura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Obtener detalles asociados a la factura
            var detalles = await _context.DetalleFactura
                .Where(d => d.ID_Factura == id)
                .ToListAsync();

            // Eliminar los detalles
            _context.DetalleFactura.RemoveRange(detalles);

            // Eliminar la factura
            var factura = await _context.Factura.FindAsync(id);
            if (factura != null)
            {
                _context.Factura.Remove(factura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool FacturaExists(int id)
        {
            return _context.Factura.Any(e => e.ID_Factura == id);
        }


        // Metodo GET: para mostrar el detalle de la factura completo
        public async Task<IActionResult> VerDetalleFactura(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalles = await _context.DetalleFactura
                .Include(d => d.ID_ProductoNavigation)
                .Where(d => d.ID_Factura == id)
                .ToListAsync();

            if (detalles == null || detalles.Count == 0)
            {
                return NotFound("No se encontraron detalles para esta factura.");
            }

            return View(detalles); 
        }



    }
}
