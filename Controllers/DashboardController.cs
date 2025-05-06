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
        public async Task<IActionResult> FoodLogCreate([Bind("FoodName,Calories,Date")] FoodLog foodLog)
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
                _dbContext.Add(foodLog);

                // Update or create DailySummary
                var today = DateOnly.FromDateTime(foodLog.Date);
                var summary = await _dbContext.DailySummaries
                    .FirstOrDefaultAsync(s => s.UserId == user.UserId && s.Date == today);

                if (summary == null)
                {
                    summary = new DailySummary
                    {
                        UserId = user.UserId,
                        Date = today,
                        TotalCaloriesConsumed = foodLog.CalsConsumed,
                        TotalCaloriesBurned = 0,
                        NetCalories = foodLog.CalsConsumed
                    };
                    _dbContext.DailySummaries.Add(summary);
                }
                else
                {
                    summary.TotalCaloriesConsumed += foodLog.CalsConsumed;
                    summary.NetCalories = summary.TotalCaloriesConsumed - summary.TotalCaloriesBurned;
                    _dbContext.DailySummaries.Update(summary);
                }

                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodLog);
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
    }
}