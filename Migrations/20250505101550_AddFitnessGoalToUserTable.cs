using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteWiseWeb2.Migrations
{
    /// <inheritdoc />
    public partial class AddFitnessGoalToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    exercise_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    category = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    calories_burned_per_min = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Exercise__C121418E7CE45E86", x => x.exercise_id);
                });

            migrationBuilder.CreateTable(
                name: "JamaicanRecipes",
                columns: table => new
                {
                    recipe_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recipename = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    calories_per_serving = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    ingredients = table.Column<string>(type: "text", nullable: true),
                    instructions = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Jamaican__3571ED9B0052AF7E", x => x.recipe_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    age = table.Column<int>(type: "int", nullable: true),
                    gender = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    height = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    weight = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    activity_level = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    FitnessGoal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    goal = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    lockStatus = table.Column<short>(type: "smallint", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Login_Attempts = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__B9BE370F9D8A29D4", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "DailySummary",
                columns: table => new
                {
                    summary_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
                    total_calories_consumed = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    total_calories_burned = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    net_calories = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DailySum__85F93E834A617B96", x => x.summary_id);
                    table.ForeignKey(
                        name: "FK__DailySumm__user___4BAC3F29",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    food_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    calories = table.Column<int>(type: "int", nullable: false),
                    protein = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    carbs = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    fats = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    fiber = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    serving_size = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    category = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    is_local = table.Column<short>(type: "smallint", nullable: true, defaultValue: (short)0),
                    user_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Food__2F4C4DD89BE1A308", x => x.food_id);
                    table.ForeignKey(
                        name: "FK_Food_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "FoodLog",
                columns: table => new
                {
                    Record_Id = table.Column<int>(type: "int", nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Food_name = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    Cals_Consumed = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodLog", x => x.Record_Id);
                    table.ForeignKey(
                        name: "FK_FoodLog_Users",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    goal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    target_weight = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    daily_caloric_target = table.Column<decimal>(type: "decimal(10,0)", nullable: true),
                    weekly_weight_change_goal = table.Column<decimal>(type: "decimal(10,0)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Goals__76679A248525153B", x => x.goal_id);
                    table.ForeignKey(
                        name: "FK__Goals__user_id__4E88ABD4",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Picture",
                columns: table => new
                {
                    user_Id = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_Pic__B9BF332771FDF7D5", x => x.user_Id);
                    table.ForeignKey(
                        name: "FK_User_Picture_Users",
                        column: x => x.user_Id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMeasurements",
                columns: table => new
                {
                    measurement_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
                    weight = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    waist_size = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    hip_size = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    chest_size = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    arm_size = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    leg_size = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserMeas__E3D1E1C14B2B2C89", x => x.measurement_id);
                    table.ForeignKey(
                        name: "FK__UserMeasu__user___3C69FB99",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                columns: table => new
                {
                    preference_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    diet_type = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    preferred_foods = table.Column<string>(type: "text", nullable: true),
                    allergies = table.Column<string>(type: "text", nullable: true),
                    disliked_foods = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserPref__FB41DBCF8F8F81C7", x => x.preference_id);
                    table.ForeignKey(
                        name: "FK__UserPrefe__user___534D60F1",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutLogs",
                columns: table => new
                {
                    log_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    exercise_id = table.Column<int>(type: "int", nullable: true),
                    duration = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    calories_burned = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WorkoutL__9E2397E0167BE287", x => x.log_id);
                    table.ForeignKey(
                        name: "FK_WorkoutLogs_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__WorkoutLo__exerc__48CFD27E",
                        column: x => x.exercise_id,
                        principalTable: "Exercises",
                        principalColumn: "exercise_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    meal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    food_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    meal_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    consumed_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Meals__2910B00F234F698A", x => x.meal_id);
                    table.ForeignKey(
                        name: "FK__Meals__food_id__4316F928",
                        column: x => x.food_id,
                        principalTable: "Food",
                        principalColumn: "food_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Meals__user_id__4222D4EF",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailySummary_user_id",
                table: "DailySummary",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Food_user_id",
                table: "Food",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_FoodLog_User_Id",
                table: "FoodLog",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_user_id",
                table: "Goals",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_food_id",
                table: "Meals",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_user_id",
                table: "Meals",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserMeasurements_user_id",
                table: "UserMeasurements",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_user_id",
                table: "UserPreferences",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__AB6E61648C1A38CF",
                table: "Users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutLogs_exercise_id",
                table: "WorkoutLogs",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutLogs_user_id",
                table: "WorkoutLogs",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailySummary");

            migrationBuilder.DropTable(
                name: "FoodLog");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "JamaicanRecipes");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "User_Picture");

            migrationBuilder.DropTable(
                name: "UserMeasurements");

            migrationBuilder.DropTable(
                name: "UserPreferences");

            migrationBuilder.DropTable(
                name: "WorkoutLogs");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
