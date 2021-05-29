#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PsuHistory/PsuHistory.csproj", "PsuHistory/"]
COPY ["PsuHistory.Data.Service/PsuHistory.Data.Repository.csproj", "PsuHistory.Data.Service/"]
COPY ["PsuHistory.Data.EF.SQL/PsuHistory.Data.EF.SQL.csproj", "PsuHistory.Data.EF.SQL/"]
COPY ["PsuHistory.Data.Domain/PsuHistory.Data.Domain.csproj", "PsuHistory.Data.Domain/"]
COPY ["PsuHistory.Business.Service/PsuHistory.Business.Service.csproj", "PsuHistory.Business.Service/"]
COPY ["PsuHistory.Resource/PsuHistory.Resource.csproj", "PsuHistory.Resource/"]
COPY ["PsuHistory.Business.DTO/PsuHistory.Business.DTO.csproj", "PsuHistory.Business.DTO/"]
COPY ["PsuHistory.Models/PsuHistory.Models.csproj", "PsuHistory.Models/"]
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