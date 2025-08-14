# Task_Progress_Generate_HTTP_Links

## Overview
This project is a **URL Shortener** with built-in statistics and visualizations.

### Key Features
- Generate short URLs from original links
- Redirect users to the original link
- Track and store visit statistics
- Display **two tables**:
  1. Unique daily visits (each IP counted only once per day)
  2. Top 10 IP addresses by total visits
- Display **line chart** for unique visits per day
- All styles loaded from `/wwwroot/css/site.css`

---

## 🛠 Prerequisites
- [.NET 6+ SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server Express
- (Optional) [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup)

---

## 🚀 Setup

### 1. Clone the repository
```bash
git clone <repository_url>
cd Task_Progress_Generate_HTTP_Links

### 2. Configure database connection
```bash
Edit appsettings.json:

"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=UrlShortenerDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}


Change Server=localhost to match your SQL Server instance name.
