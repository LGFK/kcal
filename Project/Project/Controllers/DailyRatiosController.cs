using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class DailyRatiosController : Controller
    {
        private readonly KcalContext _context;

        public DailyRatiosController(KcalContext context)
        {
            _context = context;
        }

        // GET: DailyRatios
        public async Task<IActionResult> Index()
        {
              return _context.DailyRatio != null ? 
                          View(await _context.DailyRatio.ToListAsync()) :
                          Problem("Entity set 'KcalContext.DailyRatio'  is null.");
        }

        // GET: DailyRatios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DailyRatio == null)
            {
                return NotFound();
            }

            var dailyRatio = await _context.DailyRatio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dailyRatio == null)
            {
                return NotFound();
            }

            return View(dailyRatio);
        }

        // GET: DailyRatios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DailyRatios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,DailyKcalGoal,CcalAlreadyUsed,UserId")] DailyRatio dailyRatio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dailyRatio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dailyRatio);
        }

        // GET: DailyRatios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DailyRatio == null)
            {
                return NotFound();
            }

            var dailyRatio = await _context.DailyRatio.FindAsync(id);
            if (dailyRatio == null)
            {
                return NotFound();
            }
            return View(dailyRatio);
        }

        // POST: DailyRatios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,DailyKcalGoal,CcalAlreadyUsed,UserId")] DailyRatio dailyRatio)
        {
            if (id != dailyRatio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyRatio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyRatioExists(dailyRatio.Id))
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
            return View(dailyRatio);
        }

        // GET: DailyRatios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DailyRatio == null)
            {
                return NotFound();
            }

            var dailyRatio = await _context.DailyRatio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dailyRatio == null)
            {
                return NotFound();
            }

            return View(dailyRatio);
        }

        // POST: DailyRatios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DailyRatio == null)
            {
                return Problem("Entity set 'KcalContext.DailyRatio'  is null.");
            }
            var dailyRatio = await _context.DailyRatio.FindAsync(id);
            if (dailyRatio != null)
            {
                _context.DailyRatio.Remove(dailyRatio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyRatioExists(int id)
        {
          return (_context.DailyRatio?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
