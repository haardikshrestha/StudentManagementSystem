using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class SignupsController : Controller
    {
        private readonly MyDbContext _context;

        public SignupsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Signups
        public async Task<IActionResult> Index()
        {
              return _context.Signups != null ? 
                          View(await _context.Signups.ToListAsync()) :
                          Problem("Entity set 'MyDbContext.Signups'  is null.");
        }

        // GET: Signups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Signups == null)
            {
                return NotFound();
            }

            var signup = await _context.Signups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (signup == null)
            {
                return NotFound();
            }

            return View(signup);
        }

        // GET: Signups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Signups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,Email,PhoneNo,CreatedDate")] Signup signup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(signup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(signup);
        }

        // GET: Signups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Signups == null)
            {
                return NotFound();
            }

            var signup = await _context.Signups.FindAsync(id);
            if (signup == null)
            {
                return NotFound();
            }
            return View(signup);
        }

        // POST: Signups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,Email,PhoneNo,CreatedDate")] Signup signup)
        {
            if (id != signup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(signup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SignupExists(signup.Id))
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
            return View(signup);
        }

        // GET: Signups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Signups == null)
            {
                return NotFound();
            }

            var signup = await _context.Signups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (signup == null)
            {
                return NotFound();
            }

            return View(signup);
        }

        // POST: Signups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Signups == null)
            {
                return Problem("Entity set 'MyDbContext.Signups'  is null.");
            }
            var signup = await _context.Signups.FindAsync(id);
            if (signup != null)
            {
                _context.Signups.Remove(signup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SignupExists(int id)
        {
          return (_context.Signups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
