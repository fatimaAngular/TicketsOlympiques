﻿using System;
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
    public class DisciplinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisciplinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Disciplines
        public async Task<IActionResult> Index()
        {
              return _context.Disciplines != null ? 
                          View(await _context.Disciplines.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Disciplines'  is null.");
        }

        // GET: Disciplines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Disciplines == null)
            {
                return NotFound();
            }

            var discipline = await _context.Disciplines
                .FirstOrDefaultAsync(m => m.ID == id);
            if (discipline == null)
            {
                return NotFound();
            }


            return View(discipline);
        }

        // GET: Disciplines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Disciplines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description")] Discipline discipline)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discipline);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discipline);
        }

        // GET: Disciplines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Disciplines == null)
            {
                return NotFound();
            }

            var discipline = await _context.Disciplines.FindAsync(id);
            if (discipline == null)
            {
                return NotFound();
            }
            return View(discipline);
        }

        // POST: Disciplines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description")] Discipline discipline)
        {
            if (id != discipline.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discipline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplineExists(discipline.ID))
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
            return View(discipline);
        }

        // GET: Disciplines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Disciplines == null)
            {
                return NotFound();
            }

            var discipline = await _context.Disciplines
                .FirstOrDefaultAsync(m => m.ID == id);
            if (discipline == null)
            {
                return NotFound();
            }

            return View(discipline);
        }

        // POST: Disciplines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Disciplines == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Disciplines'  is null.");
            }
            var discipline = await _context.Disciplines.FindAsync(id);
            if (discipline != null)
            {
                _context.Disciplines.Remove(discipline);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplineExists(int id)
        {
          return (_context.Disciplines?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
