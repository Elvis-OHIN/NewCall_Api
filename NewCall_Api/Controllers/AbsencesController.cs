﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewCall_Api.Database;
using NewCall_Api.Models;

namespace NewCall_Api.Controllers
{
    public class AbsencesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public AbsencesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Absences
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Absences.Include(a => a.student);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Absences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Absences == null)
            {
                return NotFound();
            }

            var absences = await _context.Absences
                .Include(a => a.student)
                .FirstOrDefaultAsync(m => m.id == id);
            if (absences == null)
            {
                return NotFound();
            }

            return View(absences);
        }

        // GET: Absences/Create
        public IActionResult Create()
        {
            ViewData["studentId"] = new SelectList(_context.Students, "id", "firstname");
            return View();
        }

        // POST: Absences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,studentId,startDate,endDate,reason,comments")] Absences absences)
        {
            if (ModelState.IsValid)
            {
                _context.Add(absences);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["studentId"] = new SelectList(_context.Students, "id", "firstname", absences.studentId);
            return View(absences);
        }

        // GET: Absences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Absences == null)
            {
                return NotFound();
            }

            var absences = await _context.Absences.FindAsync(id);
            if (absences == null)
            {
                return NotFound();
            }
            ViewData["studentId"] = new SelectList(_context.Students, "id", "firstname", absences.studentId);
            return View(absences);
        }

        // POST: Absences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,studentId,startDate,endDate,reason,comments")] Absences absences)
        {
            if (id != absences.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(absences);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbsencesExists(absences.id))
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
            ViewData["studentId"] = new SelectList(_context.Students, "id", "firstname", absences.studentId);
            return View(absences);
        }

        // GET: Absences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Absences == null)
            {
                return NotFound();
            }

            var absences = await _context.Absences
                .Include(a => a.student)
                .FirstOrDefaultAsync(m => m.id == id);
            if (absences == null)
            {
                return NotFound();
            }

            return View(absences);
        }

        // POST: Absences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Absences == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Absences'  is null.");
            }
            var absences = await _context.Absences.FindAsync(id);
            if (absences != null)
            {
                _context.Absences.Remove(absences);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbsencesExists(int id)
        {
          return (_context.Absences?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
