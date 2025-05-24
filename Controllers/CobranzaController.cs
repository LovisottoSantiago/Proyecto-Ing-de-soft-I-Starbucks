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
    public class CobranzaController : Controller
    {
        private readonly AppDbContext _context;

        public CobranzaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Cobranza
        
        public async Task<IActionResult> Index()
        {
            var cobranzas = await _context.Cobranza
                .Include(c => c.ID_FormaPagoNavigation)
                .Include(c => c.ID_FacturaNavigation)
                .OrderByDescending(c => c.Fecha)
                .ToListAsync();
            return View(cobranzas);
        }

        // GET: Cobranza/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cobranza = await _context.Cobranza
                .FirstOrDefaultAsync(m => m.ID_Cobranza == id);
            if (cobranza == null)
            {
                return NotFound();
            }

            return View(cobranza);
        }

        // GET: Cobranza/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cobranza/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Cobranza,ID_Factura,Fecha,Monto,ID_FormaPago")] Cobranza cobranza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cobranza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cobranza);
        }

        // GET: Cobranza/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cobranza = await _context.Cobranza.FindAsync(id);
            if (cobranza == null)
            {
                return NotFound();
            }
            return View(cobranza);
        }

        // POST: Cobranza/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Cobranza,ID_Factura,Fecha,Monto,ID_FormaPago")] Cobranza cobranza)
        {
            if (id != cobranza.ID_Cobranza)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cobranza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CobranzaExists(cobranza.ID_Cobranza))
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
            return View(cobranza);
        }

        // GET: Cobranza/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cobranza = await _context.Cobranza
                .FirstOrDefaultAsync(m => m.ID_Cobranza == id);
            if (cobranza == null)
            {
                return NotFound();
            }

            return View(cobranza);
        }

        // POST: Cobranza/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cobranza = await _context.Cobranza.FindAsync(id);
            if (cobranza != null)
            {
                _context.Cobranza.Remove(cobranza);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CobranzaExists(int id)
        {
            return _context.Cobranza.Any(e => e.ID_Cobranza == id);
        }
    }
}
