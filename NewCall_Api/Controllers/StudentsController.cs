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
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public StudentsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Students
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            return _context.Students != null ?
                      Ok(await _context.Students.ToListAsync()) :
                      Problem("Entity set 'ApplicationDBContext.Student' is null.");
        }

        // GET: Students/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .FirstOrDefaultAsync(m => m.id == id);
            if (students == null)
            {
                return NotFound();
            }

            return Ok(students);
        }

        // GET: Students/Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return Ok();
        }

        // POST: Students/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("id,firstname,lastname,statut")] Students students)
        {
            if (ModelState.IsValid)
            {
                _context.Add(students);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(students);
        }

        // GET: Students/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }
            return Ok(students);
        }

        // POST: Students/Edit/5
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("id,firstname,lastname,statut")] Students students)
        {
            if (id != students.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(students);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsExists(students.id))
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
            return Ok(students);
        }

        // GET: Students/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .FirstOrDefaultAsync(m => m.id == id);
            if (students == null)
            {
                return NotFound();
            }

            return Ok(students);
        }

        // POST: Students/Delete/5
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Student' is null.");
            }
            var students = await _context.Students.FindAsync(id);
            if (students != null)
            {
                _context.Students.Remove(students);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentsExists(int id)
        {
            return (_context.Students?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
