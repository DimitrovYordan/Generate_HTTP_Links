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

1. **Clone the repository and navigate to it:**
```bash
git clone https://github.com/YOUR_USERNAME/YOUR_REPO.git
cd YOUR_REPO
