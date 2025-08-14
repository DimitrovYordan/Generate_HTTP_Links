Task Progress Generate HTTP Links

A simple ASP.NET Core MVC application for shortening URLs, tracking visits, and viewing statistics.

📦 Features

URL Shortening: Generate a short link from a long URL.

Visit Tracking:

Stores client IP address and visit timestamp.

Counts unique visits per day (each IP counted at most once per day).

Statistics Page:

Unique visits per day (chart).

Top 10 IP addresses by total visits.

Persistent Storage: SQL Server (EF Core).

All styles served from /wwwroot/css/site.css.

🛠 Prerequisites

.NET 6+ SDK

SQL Server or SQL Server Express

(Optional) SQL Server Management Studio (SSMS)

🚀 Setup

Clone the repository and navigate into it

git clone https://github.com/YOUR_USERNAME/YOUR_REPO.git
cd YOUR_REPO


Configure database connection — edit appsettings.json and set your connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=UrlShortenerDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}


Change Server=localhost to match your SQL Server instance (e.g. .\SQLEXPRESS).

Create the database — choose one of the following options:

Option A — EF Core Migrations (recommended)

Install EF tools if not already installed:

dotnet tool install --global dotnet-ef


Apply migrations (this will create the DB schema):

dotnet ef database update


Option B — SQL Script

Open DatabaseBackup/UrlShortenerDb.sql in SSMS and execute it to create the schema.

Option C — Restore from Backup

Restore DatabaseBackup/UrlShortenerDb.bak in SSMS (example T-SQL):

RESTORE DATABASE UrlShortenerDb
FROM DISK = 'C:\path\to\DatabaseBackup\UrlShortenerDb.bak'
WITH MOVE 'UrlShortenerDb' TO 'C:\SQLData\UrlShortenerDb.mdf',
     MOVE 'UrlShortenerDb_log' TO 'C:\SQLData\UrlShortenerDb_log.ldf',
     REPLACE;


Adjust file paths to your environment.

Run the application

dotnet run


Then open the app in your browser at:

https://localhost:5001  or  http://localhost:5000

📊 Usage

Open the app in your browser.

Paste a long URL into the form and click Shorten.

Copy the generated short link (it will redirect to the original URL).

To view statistics for a shortened URL, open the secret stats link that was generated when you created the short URL, e.g.:

https://yourhost/stats/{secret}


On the stats page you will see:

Unique visits per day (chart + table)

Top 10 IP addresses (all-time) table

🗂 Project structure
Task_Progress_Generate_HTTP_Links/
├── Controllers/          # MVC controllers
├── Data/                 # DbContext, migrations
├── Models/               # EF models and ViewModels
├── Services/             # Background services (optional)
├── Views/                # Razor views
├── wwwroot/              # Static files: css, js, images
│   └── css/
│       └── site.css      # All site styles (tables, footer, layout)
├── DatabaseBackup/       # SQL script and optional .bak
│   ├── UrlShortenerDb.sql
│   └── UrlShortenerDb.bak
├── appsettings.json
└── README.md

🧾 Migrations and DB scripts

The repo contains EF Core migrations under Data/Migrations/ (or Migrations/) — include them in VCS so dotnet ef database update works out-of-the-box.

SQL creation script is available at DatabaseBackup/UrlShortenerDb.sql.

Optional .bak backup file can be included in DatabaseBackup/ if you prefer restoring.

⚙️ Notes & tips

The stats secret URL is generated using a cryptographically strong random string — keep it private to view stats.

Visit logging is done recording IP (prefers X-Forwarded-For when present) and VisitedAt (UTC).

For production: use migrations, secure connection strings, and consider a proper background queue for visit logging if traffic is high.

📜 License

This project is released under the MIT License.
