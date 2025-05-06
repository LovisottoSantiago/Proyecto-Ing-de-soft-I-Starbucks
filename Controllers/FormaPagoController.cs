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
    public class FormaPagoController : Controller
    {
        private readonly AppDbContext _context;

        public FormaPagoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: FormaPago
        public async Task<IActionResult> Index()
        {
            return View(await _context.FormaPago.ToListAsync());
        }

        // GET: FormaPago/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaPago = await _context.FormaPago
                .FirstOrDefaultAsync(m => m.ID_FormaPago == id);
            if (formaPago == null)
            {
                return NotFound();
            }

            return View(formaPago);
        }

        // GET: FormaPago/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FormaPago/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_FormaPago,Descripcion")] FormaPago formaPago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formaPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formaPago);
        }

        // GET: FormaPago/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaPago = await _context.FormaPago.FindAsync(id);
            if (formaPago == null)
            {
                return NotFound();
            }
            return View(formaPago);
        }

        // POST: FormaPago/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_FormaPago,Descripcion")] FormaPago formaPago)
        {
            if (id != formaPago.ID_FormaPago)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formaPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormaPagoExists(formaPago.ID_FormaPago))
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
            return View(formaPago);
        }

        // GET: FormaPago/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaPago = await _context.FormaPago
                .FirstOrDefaultAsync(m => m.ID_FormaPago == id);
            if (formaPago == null)
            {
                return NotFound();
            }

            return View(formaPago);
        }

        // POST: FormaPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formaPago = await _context.FormaPago.FindAsync(id);
            if (formaPago != null)
            {
                _context.FormaPago.Remove(formaPago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormaPagoExists(int id)
        {
            return _context.FormaPago.Any(e => e.ID_FormaPago == id);
        }
    }
}
