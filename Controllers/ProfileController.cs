using BiteWiseWeb2.Data;
using BiteWiseWeb2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiteWiseWeb2.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly BiteWiseDbContext _context;

        public ProfileController(UserManager<ApplicationUser> userManager, BiteWiseDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);  
            if (user == null)
            {
                return RedirectToAction("Login", "Account");  
            }

            // Map the user info to a ProfileViewModel
            var model = new ProfileViewModel
            {
                FullName = user.FullName,
                Age = user.Age,
                Weight = user.Weight,
                Height = user.Height,
                Gender = user.Gender,
                FitnessGoal = user.FitnessGoal,
                ActivityLevel = user.ActivityLevel,
                ProfilePicturePath = user.ProfilePicturePath
            };

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Update user information
                user.FullName = model.FullName;
                user.Age = model.Age;
                user.Weight = model.Weight;
                user.Height = model.Height;
                user.Gender = model.Gender;
                user.FitnessGoal = model.FitnessGoal;
                user.ActivityLevel = model.ActivityLevel;

                

                // Save changes to the database
                await _userManager.UpdateAsync(user);

                
                TempData["SuccessMessage"] = "Your profile has been updated successfully!";
                return RedirectToAction("Index");
            }

          
            return View(model);
        }
    }

}
