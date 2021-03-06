# PsuHistory-Back

dotnet ef migrations add FixService -p PsuHistory.Data.EF.SQL -s PsuHistory.Data.EF.SQL -c PsuHistoryDbContext --output-dir Migrations/MSSQLMigrations

dotnet ef database update -p PsuHistory.Data.EF.SQL -s PsuHistory.Data.EF.SQL -c PsuHistoryDbContext










#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PsuHistory/PsuHistory.csproj", "PsuHistory/"]
COPY ["PsuHistory.Data.Service/PsuHistory.Data.Service.csproj", "PsuHistory.Data.Service/"]
COPY ["PsuHistory.Data.EF.SQL/PsuHistory.Data.EF.SQL.csproj", "PsuHistory.Data.EF.SQL/"]
COPY ["PsuHistory.Data.Domain/PsuHistory.Data.Domain.csproj", "PsuHistory.Data.Domain/"]
RUN dotnet restore "PsuHistory/PsuHistory.csproj"
COPY . .
WORKDIR "/src/PsuHistory"
RUN dotnet build "PsuHistory.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PsuHistory.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PsuHistory.dll"]