using System;
using System.Collections.Generic;
using BiteWiseWeb2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BiteWiseWeb2.Data;

public partial class BiteWiseDbContext : DbContext
{
    public BiteWiseDbContext()
    {
    }

    public BiteWiseDbContext(DbContextOptions<BiteWiseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DailySummary> DailySummaries { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<FoodLog> FoodLogs { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<JamaicanRecipe> JamaicanRecipes { get; set; }

    public virtual DbSet<Meal> Meals { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserInfoDetail> UserInfoDetails { get; set; }

    public virtual DbSet<UserMeasurement> UserMeasurements { get; set; }

    public virtual DbSet<UserPicture> UserPictures { get; set; }

    public virtual DbSet<UserPreference> UserPreferences { get; set; }

    public virtual DbSet<UserWorkLog> UserWorkLogs { get; set; }

    public virtual DbSet<WorkoutLog> WorkoutLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Data Source=23.95.235.16,1433;Initial Catalog=BiteWiseDB;User ID=vtdi_student;Password=P@ssword1; Encrypt=false; Trusted_Connection=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DailySummary>(entity =>
        {
            entity.HasKey(e => e.SummaryId).HasName("PK__DailySum__85F93E834A617B96");

            entity.ToTable("DailySummary");

            entity.Property(e => e.SummaryId).HasColumnName("summary_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.NetCalories)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("net_calories");
            entity.Property(e => e.TotalCaloriesBurned)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("total_calories_burned");
            entity.Property(e => e.TotalCaloriesConsumed)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("total_calories_consumed");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.DailySummaries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__DailySumm__user___4BAC3F29");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("PK__Exercise__C121418E7CE45E86");

            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.CaloriesBurnedPerMin)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("calories_burned_per_min");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.FoodId).HasName("PK__Food__2F4C4DD89BE1A308");

            entity.ToTable("Food");

            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.Calories).HasColumnName("calories");
            entity.Property(e => e.Carbs)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("carbs");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.Fats)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("fats");
            entity.Property(e => e.Fiber)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("fiber");
            entity.Property(e => e.IsLocal)
                .HasDefaultValue((short)0)
                .HasColumnName("is_local");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Protein)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("protein");
            entity.Property(e => e.ServingSize)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("serving_size");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Foods)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Food_Users");
        });

        modelBuilder.Entity<FoodLog>(entity =>
        {
            entity.HasKey(e => e.RecordId);

            entity.ToTable("FoodLog");

            entity.Property(e => e.RecordId)
                .ValueGeneratedNever()
                .HasColumnName("Record_Id");
            entity.Property(e => e.CalsConsumed)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Cals_Consumed");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.FoodName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Food_name");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.FoodLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_FoodLog_Users");
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.HasKey(e => e.GoalId).HasName("PK__Goals__76679A248525153B");

            entity.Property(e => e.GoalId).HasColumnName("goal_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DailyCaloricTarget)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("daily_caloric_target");
            entity.Property(e => e.TargetWeight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("target_weight");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WeeklyWeightChangeGoal)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("weekly_weight_change_goal");

            entity.HasOne(d => d.User).WithMany(p => p.Goals)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Goals__user_id__4E88ABD4");
        });

        modelBuilder.Entity<JamaicanRecipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Jamaican__3571ED9B0052AF7E");

            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.CaloriesPerServing)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("calories_per_serving");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Ingredients)
                .HasColumnType("text")
                .HasColumnName("ingredients");
            entity.Property(e => e.Instructions)
                .HasColumnType("text")
                .HasColumnName("instructions");
            entity.Property(e => e.Recipename)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("recipename");
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.HasKey(e => e.MealId).HasName("PK__Meals__2910B00F234F698A");

            entity.Property(e => e.MealId).HasColumnName("meal_id");
            entity.Property(e => e.ConsumedAt)
                .HasColumnType("datetime")
                .HasColumnName("consumed_at");
            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.MealType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("meal_type");
            entity.Property(e => e.Quantity)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("quantity");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Food).WithMany(p => p.Meals)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Meals__food_id__4316F928");

            entity.HasOne(d => d.User).WithMany(p => p.Meals)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Meals__user_id__4222D4EF");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F9D8A29D4");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E61648C1A38CF").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ActivityLevel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("activity_level");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Goal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("goal");
            entity.Property(e => e.Height)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("height");
            entity.Property(e => e.LockStatus).HasColumnName("lockStatus");
            entity.Property(e => e.LoginAttempts).HasColumnName("Login_Attempts");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("weight");
        });

        modelBuilder.Entity<UserInfoDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("User_info_Details");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Height).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePicture)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Profile Picture");
            entity.Property(e => e.UserGoal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("User Goal");
            entity.Property(e => e.UserId).HasColumnName("User ID");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<UserMeasurement>(entity =>
        {
            entity.HasKey(e => e.MeasurementId).HasName("PK__UserMeas__E3D1E1C14B2B2C89");

            entity.Property(e => e.MeasurementId).HasColumnName("measurement_id");
            entity.Property(e => e.ArmSize)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("arm_size");
            entity.Property(e => e.ChestSize)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("chest_size");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.HipSize)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("hip_size");
            entity.Property(e => e.LegSize)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("leg_size");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WaistSize)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("waist_size");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("weight");

            entity.HasOne(d => d.User).WithMany(p => p.UserMeasurements)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UserMeasu__user___3C69FB99");
        });

        modelBuilder.Entity<UserPicture>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User_Pic__B9BF332771FDF7D5");

            entity.ToTable("User_Picture");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_Id");
            entity.Property(e => e.Image).HasColumnName("image");

            entity.HasOne(d => d.User).WithOne(p => p.UserPicture)
                .HasForeignKey<UserPicture>(d => d.UserId)
                .HasConstraintName("FK_User_Picture_Users");
        });

        modelBuilder.Entity<UserPreference>(entity =>
        {
            entity.HasKey(e => e.PreferenceId).HasName("PK__UserPref__FB41DBCF8F8F81C7");

            entity.Property(e => e.PreferenceId).HasColumnName("preference_id");
            entity.Property(e => e.Allergies)
                .HasColumnType("text")
                .HasColumnName("allergies");
            entity.Property(e => e.DietType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("diet_type");
            entity.Property(e => e.DislikedFoods)
                .HasColumnType("text")
                .HasColumnName("disliked_foods");
            entity.Property(e => e.PreferredFoods)
                .HasColumnType("text")
                .HasColumnName("preferred_foods");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserPreferences)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UserPrefe__user___534D60F1");
        });

        modelBuilder.Entity<UserWorkLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("User_Work_log");

            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Duration)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("duration");
            entity.Property(e => e.ExerciseName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Exercise Name");
            entity.Property(e => e.TotalCaloriesBurnt)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Total Calories Burnt");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("User Name");
        });

        modelBuilder.Entity<WorkoutLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__WorkoutL__9E2397E0167BE287");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.CaloriesBurned)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("calories_burned");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Duration)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("duration");
            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Exercise).WithMany(p => p.WorkoutLogs)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__WorkoutLo__exerc__48CFD27E");

            entity.HasOne(d => d.User).WithMany(p => p.WorkoutLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkoutLogs_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
