using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiteWiseWeb2.Data;
using BiteWiseWeb2.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BiteWiseWeb2.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly BiteWiseDbContext _dbContext;

        public DashboardController(UserManager<IdentityUser> userManager, BiteWiseDbContext dbContext)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            var identityUser = await _userManager.GetUserAsync(User);
            if (identityUser == null)
            {
                return Challenge(); // Redirect to login
            }

            // Find User by Email
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == identityUser.Email);

            // Create User if missing
            if (user == null)
            {
                user = new User
                {
                    Email = identityUser.Email ?? throw new InvalidOperationException("Email cannot be null."),
                    Name = identityUser.UserName ?? identityUser.Email ?? throw new InvalidOperationException("UserName and Email cannot both be null."),
                    Password = "TempPassword", // Placeholder; not used for auth
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    LockStatus = 0,
                    LoginAttempts = 0
                };
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
            }

            var today = DateOnly.FromDateTime(DateTime.Today);
            var summary = await _dbContext.DailySummaries
                .FirstOrDefaultAsync(s => s.UserId == user.UserId && s.Date == today);

            var recentLogs = await _dbContext.FoodLogs
                .Where(l => l.UserId == user.UserId)
                .OrderByDescending(l => l.Date)
                .Take(3)
                .Include(l => l.User)
                .ToListAsync();

            var model = new DashboardViewModel
            {
                UserName = user.Name ?? identityUser.Email ?? throw new InvalidOperationException("UserName and Email cannot both be null."),
                TodaySummary = summary ?? new DailySummary(),
                RecentFoodLogs = recentLogs ?? new System.Collections.Generic.List<FoodLog>()
            };

            return View(model);
        }

        // POST: Dashboard/FoodLogCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFoodLog([Bind("FoodName, Calories, Date")] FoodLog foodLog)
        {
            var identityUser = await _userManager.GetUserAsync(User);
            if (identityUser == null)
            {
                return Challenge();
            }

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == identityUser.Email);

            if (user == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                foodLog.UserId = user.UserId;
                _dbContext.Add(foodLog);  // Add food log to database
                await _dbContext.SaveChangesAsync();  // Save changes

                // Set a success message in TempData
                TempData["SuccessMessage"] = "Food log added successfully!";

                return RedirectToAction(nameof(Index));  // Redirect to the Index action after saving
            }

            return View(foodLog);  // Return view with validation errors if any
        }



        // Fix for CS0019 and CS0029 errors by ensuring proper conversion between DateOnly and DateTime
        private static DateOnly ConvertToDateOnly(DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime);
        }

        private static DateOnly? ConvertToNullableDateOnly(DateTime? dateTime)
        {
            return dateTime.HasValue ? DateOnly.FromDateTime(dateTime.Value) : null;
        }

        // GET: Dashboard/EditLog/5
        public async Task<IActionResult> EditLog(int id)
        {
            var log = await _dbContext.FoodLogs.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }

            return View(log);
        }

        // POST: Dashboard/EditLog/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLog(int id, [Bind("RecordId,FoodName,CalsConsumed,Date")] FoodLog foodLog)
        {
            if (id != foodLog.RecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(foodLog);

                    // Update DailySummary if needed
                    var today = DateOnly.FromDateTime(foodLog.Date);
                    var summary = await _dbContext.DailySummaries
                        .FirstOrDefaultAsync(s => s.UserId == foodLog.UserId && s.Date == today);

                    if (summary != null)
                    {
                        // Optionally recalculate based on all logs
                        var totalConsumed = await _dbContext.FoodLogs
                            .Where(f => f.UserId == foodLog.UserId && f.Date == foodLog.Date)
                            .SumAsync(f => f.CalsConsumed);

                        summary.TotalCaloriesConsumed = totalConsumed;
                        summary.NetCalories = summary.TotalCaloriesConsumed - summary.TotalCaloriesBurned;
                        _dbContext.Update(summary);
                    }

                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dbContext.FoodLogs.Any(e => e.RecordId == foodLog.RecordId))
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

            return View(foodLog);
        }

        // POST: Dashboard/DeleteLog/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLog(int id)
        {
            var log = await _dbContext.FoodLogs.FindAsync(id);
            if (log != null)
            {
                _dbContext.FoodLogs.Remove(log);

                // Update DailySummary if needed
                var summary = await _dbContext.DailySummaries
                    .FirstOrDefaultAsync(s => s.UserId == log.UserId && s.Date == DateOnly.FromDateTime(log.Date));

                if (summary != null)
                {
                    summary.TotalCaloriesConsumed -= log.CalsConsumed;
                    summary.NetCalories = summary.TotalCaloriesConsumed - summary.TotalCaloriesBurned;
                    _dbContext.Update(summary);
                }

                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddFoodLog([Bind("FoodName,Calories,Date")] FoodLog foodLog)
        //{
        //    var identityUser = await _userManager.GetUserAsync(User);
        //    if (identityUser == null)
        //    {
        //        return Challenge();
        //    }

        //    var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == identityUser.Email);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        foodLog.UserId = user.UserId;
        //        _dbContext.FoodLogs.Add(foodLog);

        //        var today = DateOnly.FromDateTime(foodLog.Date);
        //        var summary = await _dbContext.DailySummaries
        //            .FirstOrDefaultAsync(s => s.UserId == user.UserId && s.Date == today);

        //        if (summary == null)
        //        {
        //            summary = new DailySummary
        //            {
        //                UserId = user.UserId,
        //                Date = today,
        //                TotalCaloriesConsumed = foodLog.CalsConsumed,
        //                TotalCaloriesBurned = 0,
        //                NetCalories = foodLog.CalsConsumed
        //            };
        //            _dbContext.DailySummaries.Add(summary);
        //        }
        //        else
        //        {
        //            summary.TotalCaloriesConsumed += foodLog.CalsConsumed;
        //            summary.NetCalories = summary.TotalCaloriesConsumed - summary.TotalCaloriesBurned;
        //            _dbContext.DailySummaries.Update(summary);
        //        }

        //        await _dbContext.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    // Instead of returning a missing view, go back to dashboard
        //    return RedirectToAction(nameof(Index));
        //}

    }
}
