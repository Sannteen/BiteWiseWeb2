
using Microsoft.AspNetCore.Mvc;
using BiteWiseWeb2.Models;
using System.Linq;

namespace BiteWiseWeb2.Controllers
{
    public class ProfileController : Controller
    {
        private readonly BiteWiseDb _context;

        public ProfileController(BiteWiseDb context)
        {
            _context = context;
        }

        // GET: /Profile
        public IActionResult Index()
        {
            // Example: Get the currently logged-in user (replace with actual login logic)
            int userId = 1; // Replace with actual logic to get user ID from session or claims

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var vm = new ProfileViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Gender = user.Gender,
                Height = user.Height,
                Weight = user.Weight,
                FitnessGoal = user.FitnessGoal,
                ActivityLevel = user.ActivityLevel,
                CurrentPassword = "",
                NewPassword = ""
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == model.Id);

            if (user == null)
            {
                return NotFound();
            }

            // Optional: Validate current password here
            // Example (if passwords are stored hashed): Hash the input and compare with stored hash

            // Update user information
            user.Name = model.Name;
            user.Email = model.Email;
            user.Gender = model.Gender;
            user.Height = model.Height;
            user.Weight = model.Weight;
            user.FitnessGoal = model.FitnessGoal;
            user.ActivityLevel = model.ActivityLevel;

            // Update password if new password is provided
            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                // Note: Replace this with secure password hashing
                user.PasswordHash = model.NewPassword;
            }

            _context.SaveChanges();

            ViewBag.Message = "Profile updated successfully!";
            return View(model);
        }
    }
}