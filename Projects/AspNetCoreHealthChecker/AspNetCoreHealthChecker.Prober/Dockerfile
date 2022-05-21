FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AspNetCoreHealthChecker/AspNetCoreHealthChecker.csproj", "AspNetCoreHealthChecker/"]
COPY ["AspNetCoreHealthChecker.Prober/AspNetCoreHealthChecker.Prober.csproj", "AspNetCoreHealthChecker.Prober/"]
RUN dotnet restore "AspNetCoreHealthChecker.Prober/AspNetCoreHealthChecker.Prober.csproj"
COPY . .
RUN dotnet build -c Release -o /app/build "AspNetCoreHealthChecker.Prober/AspNetCoreHealthChecker.Prober.csproj"

FROM build AS publish
RUN ls
RUN dotnet publish -c Release -o /app/publish "AspNetCoreHealthChecker.Prober/AspNetCoreHealthChecker.Prober.csproj"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCoreHealthChecker.Prober.dll"]
