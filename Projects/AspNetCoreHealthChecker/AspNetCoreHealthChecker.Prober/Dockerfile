FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AspNetCoreHealthChecker.Prober/AspNetCoreHealthChecker.Prober.csproj", "AspNetCoreHealthChecker.Prober/"]
RUN dotnet restore "AspNetCoreHealthChecker.Prober/AspNetCoreHealthChecker.Prober.csproj"
COPY . .
WORKDIR "/src/AspNetCoreHealthChecker.Prober"
RUN dotnet build "AspNetCoreHealthChecker.Prober.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetCoreHealthChecker.Prober.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCoreHealthChecker.Prober.dll"]
