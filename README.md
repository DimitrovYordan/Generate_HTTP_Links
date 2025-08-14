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
     dotnet ef database update
     ```

   - **Option B: Run SQL Script**  
     Execute `DatabaseBackup/UrlShortenerDb.sql` using SQL Server Management Studio.

   - **Option C: Restore from Backup**  
     ```sql
     RESTORE DATABASE UrlShortenerDb
     FROM DISK = 'DatabaseBackup/UrlShortenerDb.bak'
     WITH MOVE 'UrlShortenerDb' TO 'C:\SQLData\UrlShortenerDb.mdf',
          MOVE 'UrlShortenerDb_log' TO 'C:\SQLData\UrlShortenerDb_log.ldf',
          REPLACE;
     ```

4. **Run the application**  
   ```bash
   dotnet run
   ```
   Then open in your browser:  
   **https://localhost:5001** or **http://localhost:5000**
