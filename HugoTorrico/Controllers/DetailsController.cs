using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HugoTorrico.Models;

namespace HugoTorrico.Controllers
{
    public class DetailsController : Controller
    {
        private readonly StoreContext _context;

        public DetailsController(StoreContext context)
        {
            _context = context;
        }

        // GET: Details
        public async Task<IActionResult> Index()
        {
            var storeContext = _context.Details
                .Include(d => d.Invoice)
                .Include(d => d.Product)
                .Where(d => d.Active); // Filtrar solo los activos
            return View(await storeContext.ToListAsync());
        }

        // GET: Details/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detail = await _context.Details
                .Include(d => d.Invoice)
                .Include(d => d.Product)
                .FirstOrDefaultAsync(m => m.DetailId == id);
            if (detail == null)
            {
                return NotFound();
            }

            return View(detail);
        }

        // GET: Details/Create
        public IActionResult Create()
        {
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "InvoiceId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetailId,Amount,Price,Subtotal,InvoiceId,ProductId")] Detail detail)
        {

                _context.Add(detail);
                detail.Active = true; // Asegurarse de que el detalle se cree como activo
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "InvoiceId", detail.InvoiceId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", detail.ProductId);
            return View(detail);
        }

        // GET: Details/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detail = await _context.Details.FindAsync(id);
            if (detail == null)
            {
                return NotFound();
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "InvoiceId", detail.InvoiceId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", detail.ProductId);
            return View(detail);
        }

        // POST: Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetailId,Amount,Price,Subtotal,InvoiceId,ProductId")] Detail detail)
        {
            if (id != detail.DetailId)
            {
                return NotFound();
            }


                try
                {
                    detail.Active = true; // Asegurarse de que el detalle permanezca activo
                    _context.Update(detail);
             // Asegurarse de que el detalle permanezca activo después de la edición
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailExists(detail.DetailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "InvoiceId", detail.InvoiceId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", detail.ProductId);
            return View(detail);
        }

        // GET: Details/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detail = await _context.Details
                .Include(d => d.Invoice)
                .Include(d => d.Product)
                .FirstOrDefaultAsync(m => m.DetailId == id);
            if (detail == null)
            {
                return NotFound();
            }

            return View(detail);
        }

        // POST: Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detail = await _context.Details.FindAsync(id);
            if (detail != null)
            {
                detail.Active = false; // Marcamos como inactivo
                _context.Update(detail);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DetailExists(int id)
        {
            return _context.Details.Any(e => e.DetailId == id);
        }
    }
}
