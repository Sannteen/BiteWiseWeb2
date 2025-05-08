# BiteWiseWeb

BiteWiseWeb is an ASP.NET Core MVC web application that mirrors the functionality of the desktop BiteWiseApp. It provides a web-based experience for users who prefer logging health data and tracking fitness goals on the go.

## Purpose
The web version offers:
- User registration and login with Identity Authentication.
- A personalized dashboard with links to Food Log, Profile, Goals, and Daily Summaries.
- CRUD operations for meals, caloric goals, and workouts.
- Consistent UI with a turquoise-based theme.

## Technologies Used
- ASP.NET Core MVC
- Entity Framework Core
- Identity for authentication
- SQL Server
- Bootstrap (for styling)

## How to Run
1. Clone this repository to your machine.
2. Open the solution (`BiteWiseWeb2.sln`) in Visual Studio 2022 or later.
3. Ensure the `appsettings.json` connection string is set to your SQL Server instance.
4. Run database migrations if needed.
5. Build and run the project (`Ctrl + F5`).

> ✅ The project does **not** seed demo data. You must authenticate using valid existing credentials in the database.

## Folder Structure
- `Controllers/` – Handles request logic.
- `Models/` – Entity classes and view models.
- `Views/` – Razor views for rendering UI.
- `wwwroot/` – Static files like CSS, JS, and images.
- `Data/` – EF Core DB context and Identity configuration.

## Deployment
This app can be deployed to IIS, Azure, or any compatible ASP.NET hosting platform.

## Contribution
Feel free to open issues or submit pull requests with improvements or bug fixes.

## License
MIT License
