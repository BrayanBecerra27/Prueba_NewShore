#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NEWSHORE_AIR_WEB/NEWSHORE_AIR_WEB.csproj", "NEWSHORE_AIR_WEB/"]
COPY ["NEWSHORE_AIR_WEB/NEWSHORE_AIR_BUSINESS.csproj", "NEWSHORE_AIR_BUSINESS/"]
COPY ["NEWSHORE_AIR_WEB/NEWSHORE_AIR_DATAACCESS.csproj", "NEWSHORE_AIR_DATAACCESS/"]
RUN dotnet restore "NEWSHORE_AIR_WEB/NEWSHORE_AIR_WEB.csproj"
COPY . .
WORKDIR "/src/NEWSHORE_AIR_WEB"
RUN dotnet build "NEWSHORE_AIR_WEB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NEWSHORE_AIR_WEB.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NEWSHORE_AIR_WEB.dll"]