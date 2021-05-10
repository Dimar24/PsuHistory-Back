# PsuHistory-Back

dotnet ef migrations add FixService -p PsuHistory.Data.EF.SQL -s PsuHistory.Data.EF.SQL -c PsuHistoryDbContext --output-dir Migrations/MSSQLMigrations

dotnet ef database update -p PsuHistory.Data.EF.SQL -s PsuHistory.Data.EF.SQL -c PsuHistoryDbContext