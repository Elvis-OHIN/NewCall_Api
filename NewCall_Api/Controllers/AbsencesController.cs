using System;
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
    [Route("api/[controller]")]
    [ApiController]
    public class AbsencesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public AbsencesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Absences
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Absences;
            return Ok(await applicationDBContext.ToListAsync());
        }
        // GET: Absences
        [HttpGet("Date/{date}")]
        public async Task<IActionResult> GetDate(DateTime? dateTime)
        {
            if (dateTime == null || _context.Absences == null)
            {
                return NotFound();
            }

            var absences = await _context.Absences
                .FirstOrDefaultAsync(m => m.startDate == dateTime);
            if (absences == null)
            {
                return NotFound();
            }

            return Ok(absences);
        }
        // GET: Absences/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Absences == null)
            {
                return NotFound();
            }

            var absences = await _context.Absences
                .FirstOrDefaultAsync(m => m.id == id);
            if (absences == null)
            {
                return NotFound();
            }

            return Ok(absences);
        }

     

        // POST: Absences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        public async Task<IActionResult> Create([Bind("id,studentId,startDate,endDate,reason,comments")] Absences absences)
        {
            if (ModelState.IsValid)
            {
                _context.Add(absences);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["studentId"] = new SelectList(_context.Students, "id", "firstname", absences.studentId);
            return Ok(absences);
        }


        // POST: Absences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPut("Edit/{id}")]
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
            return Ok(absences);
        }

     
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Absences == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Absences' is null.");
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
