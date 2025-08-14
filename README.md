# Task Progress Generate HTTP Links

A simple ASP.NET Core MVC application for shortening URLs, tracking visits, and viewing statistics.

---

## 📦 Features
- **URL Shortening**: Generate a short link from a long URL.
- **Visit Tracking**:
  - Stores IP address and visit timestamp.
  - Counts unique visits per day.
- **Statistics Page**:
  - Unique visits per day (chart view).
  - Top 10 IP addresses of all time for the shortened URL.
- **Persistent Storage**: SQL Server database.
- **Bootstrap Table Styling** from `/wwwroot/css/site.css`.

---

## 🛠 Prerequisites
- [.NET 6+ SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server Express
- (Optional) [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup)

---

## 🚀 Setup

## ​ Setup

1. **Clone the repository and navigate into it**  
   ```bash
   git clone https://github.com/YOUR_USERNAME/YOUR_REPO.git
   cd YOUR_REPO
   ```

2. **Configure database connection in** `appsettings.json`:  
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=UrlShortenerDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

3. **Create the database** (choose one option):

   - **Option A: EF Core Migrations (recommended)**  
     ```bash
     dotnet tool install --global dotnet-ef
     cd Task_Progress_Generate_HTTP_Links
     dotnet ef database update
     ```

   - **Option B: Run SQL Script**  
     Execute `DatabaseBackup/UrlShortenerDb.sql` using SQL Server Management Studio.

4. **Run the application**  
   ```bash
   dotnet run
   ```

## Usage
1. Clone the repository:
   ```bash
   git clone https://github.com/DimitrovYordan/Generate_HTTP_Links.git
2. Open the solution in Visual Studio.

3. Restore NuGet packages.

4. Configure the database connection string in appsettings.json.

5. Apply migrations or restore the database (see section below).

6. Run the application using IIS Express or dotnet run.

7. Open the browser at http://localhost:port you will see at console
   ```

Notes & tips

Database configuration: Ensure that your SQL Server instance is running and that the connection string in appsettings.json is correct.

Ports and SSL: The sample uses https://localhost:44324. If your port differs, adjust the URL accordingly.

Static files: All styles are located in wwwroot/css/site.css. Modify this file for UI changes.

Logging: Visit logs are recorded asynchronously to avoid blocking the main thread.

Chart rendering: The statistics page uses Chart.js loaded from a CDN.

Seeding data: If you use the SQL script or backup file, initial sample data will be included.
