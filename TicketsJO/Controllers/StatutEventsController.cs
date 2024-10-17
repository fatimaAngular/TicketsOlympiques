using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketsJO.Data;
using TicketsJO.Models;

namespace TicketsJO.Controllers
{
    public class StatutEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatutEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StatutEvents
        public async Task<IActionResult> Index()
        {
              return _context.StatutEvents != null ? 
                          View(await _context.StatutEvents.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.StatutEvents'  is null.");
        }

        // GET: StatutEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StatutEvents == null)
            {
                return NotFound();
            }

            var statutEvent = await _context.StatutEvents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statutEvent == null)
            {
                return NotFound();
            }

            return View(statutEvent);
        }

        // GET: StatutEvents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatutEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] StatutEvent statutEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statutEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statutEvent);
        }

        // GET: StatutEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StatutEvents == null)
            {
                return NotFound();
            }

            var statutEvent = await _context.StatutEvents.FindAsync(id);
            if (statutEvent == null)
            {
                return NotFound();
            }
            return View(statutEvent);
        }

        // POST: StatutEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] StatutEvent statutEvent)
        {
            if (id != statutEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statutEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatutEventExists(statutEvent.Id))
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
            return View(statutEvent);
        }

        // GET: StatutEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StatutEvents == null)
            {
                return NotFound();
            }

            var statutEvent = await _context.StatutEvents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statutEvent == null)
            {
                return NotFound();
            }

            return View(statutEvent);
        }

        // POST: StatutEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StatutEvents == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StatutEvents'  is null.");
            }
            var statutEvent = await _context.StatutEvents.FindAsync(id);
            if (statutEvent != null)
            {
                _context.StatutEvents.Remove(statutEvent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatutEventExists(int id)
        {
          return (_context.StatutEvents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
