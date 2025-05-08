using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BiteWiseWeb2.Models;

namespace BiteWiseWeb2.Models
{
    public class DashboardViewModel
    {
        internal List<Food> Foods;

        // Display properties
        public string UserName { get; set; }
        public DailySummary TodaySummary { get; set; }
        public List<FoodLog> RecentFoodLogs { get; set; } = new List<FoodLog>();

        // Food log form
        [Required]
        [StringLength(100)]
        public string FoodName { get; set; }

        [Required]
        [Range(0, 5000)]
        public int FoodCalories { get; set; }

        [Required]
        public DateTime FoodDate { get; set; } = DateTime.Today;

        // Workout log form
        [Required]
        [StringLength(100)]
        public string ExerciseType { get; set; }

        [Required]
        [Range(1, 240)]
        public int DurationMinutes { get; set; }

        [Required]
        [Range(0, 2000)]
        public int WorkoutCaloriesBurned { get; set; }

        [Required]
        public DateTime WorkoutDate { get; set; } = DateTime.Today;

        // For editing
        public FoodLog FoodLog { get; set; }
    }
}

