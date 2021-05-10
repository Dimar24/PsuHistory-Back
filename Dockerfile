#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#ENV PORT=8080

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["server/PsuHistory-Back/PsuHistory/PsuHistory.csproj", "PsuHistory/"]
COPY ["server/PsuHistory-Back/PsuHistory.Data.Service/PsuHistory.Data.Service.csproj", "PsuHistory.Data.Service/"]
COPY ["server/PsuHistory-Back/PsuHistory.Data.EF.SQL/PsuHistory.Data.EF.SQL.csproj", "PsuHistory.Data.EF.SQL/"]
COPY ["server/PsuHistory-Back/PsuHistory.Data.Domain/PsuHistory.Data.Domain.csproj", "PsuHistory.Data.Domain/"]
RUN dotnet restore "PsuHistory/PsuHistory.csproj"
COPY ["server/PsuHistory-Back/PsuHistory/", "PsuHistory/"]
COPY ["server/PsuHistory-Back/PsuHistory.Data.Service", "PsuHistory.Data.Service/"]
COPY ["server/PsuHistory-Back/PsuHistory.Data.EF.SQL", "PsuHistory.Data.EF.SQL/"]
COPY ["server/PsuHistory-Back/PsuHistory.Data.Domain", "PsuHistory.Data.Domain/"]
WORKDIR "/src/PsuHistory"
RUN dotnet build "PsuHistory.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PsuHistory.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PsuHistory.dll"]
#EXPOSE $PORT