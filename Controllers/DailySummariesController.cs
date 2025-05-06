using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiteWiseWeb2.Models;

namespace BiteWiseWeb2.Controllers
{
    public class DailySummariesController : Controller
    {
        private readonly BiteWiseDbContext _context;

        public DailySummariesController(BiteWiseDbContext context)
        {
            _context = context;
        }

        // GET: DailySummaries
        public async Task<IActionResult> Index()
        {
            var biteWiseDbContext = _context.DailySummaries.Include(d => d.User);
            return View(await biteWiseDbContext.ToListAsync());
        }

        // GET: DailySummaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySummary = await _context.DailySummaries
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.SummaryId == id);
            if (dailySummary == null)
            {
                return NotFound();
            }

            return View(dailySummary);
        }

        // GET: DailySummaries/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: DailySummaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SummaryId,UserId,Date,TotalCaloriesConsumed,TotalCaloriesBurned,NetCalories")] DailySummary dailySummary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dailySummary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", dailySummary.UserId);
            return View(dailySummary);
        }

        // GET: DailySummaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySummary = await _context.DailySummaries.FindAsync(id);
            if (dailySummary == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", dailySummary.UserId);
            return View(dailySummary);
        }

        // POST: DailySummaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SummaryId,UserId,Date,TotalCaloriesConsumed,TotalCaloriesBurned,NetCalories")] DailySummary dailySummary)
        {
            if (id != dailySummary.SummaryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailySummary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailySummaryExists(dailySummary.SummaryId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", dailySummary.UserId);
            return View(dailySummary);
        }

        // GET: DailySummaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySummary = await _context.DailySummaries
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.SummaryId == id);
            if (dailySummary == null)
            {
                return NotFound();
            }

            return View(dailySummary);
        }

        // POST: DailySummaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailySummary = await _context.DailySummaries.FindAsync(id);
            if (dailySummary != null)
            {
                _context.DailySummaries.Remove(dailySummary);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailySummaryExists(int id)
        {
            return _context.DailySummaries.Any(e => e.SummaryId == id);
        }
    }
}
