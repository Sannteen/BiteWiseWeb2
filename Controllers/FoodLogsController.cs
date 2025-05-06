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
    public class FoodLogsController : Controller
    {
        private readonly BiteWiseDbContext _context;

        public FoodLogsController(BiteWiseDbContext context)
        {
            _context = context;
        }

        // GET: FoodLogs
        public async Task<IActionResult> Index()
        {
            var biteWiseDbContext = _context.FoodLogs.Include(f => f.User);
            return View(await biteWiseDbContext.ToListAsync());
        }

        // GET: FoodLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodLog = await _context.FoodLogs
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (foodLog == null)
            {
                return NotFound();
            }

            return View(foodLog);
        }

        // GET: FoodLogs/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: FoodLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordId,UserId,FoodName,CalsConsumed,Date")] FoodLog foodLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", foodLog.UserId);
            return View(foodLog);
        }

        // GET: FoodLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodLog = await _context.FoodLogs.FindAsync(id);
            if (foodLog == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", foodLog.UserId);
            return View(foodLog);
        }

        // POST: FoodLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordId,UserId,FoodName,CalsConsumed,Date")] FoodLog foodLog)
        {
            if (id != foodLog.RecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodLogExists(foodLog.RecordId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", foodLog.UserId);
            return View(foodLog);
        }

        // GET: FoodLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodLog = await _context.FoodLogs
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (foodLog == null)
            {
                return NotFound();
            }

            return View(foodLog);
        }

        // POST: FoodLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodLog = await _context.FoodLogs.FindAsync(id);
            if (foodLog != null)
            {
                _context.FoodLogs.Remove(foodLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodLogExists(int id)
        {
            return _context.FoodLogs.Any(e => e.RecordId == id);
        }
    }
}
