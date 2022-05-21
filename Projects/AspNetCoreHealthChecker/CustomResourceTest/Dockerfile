FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CustomResourceTest/CustomResourceTest.csproj", "CustomResourceTest/"]
RUN dotnet restore "CustomResourceTest/CustomResourceTest.csproj"
COPY . .
WORKDIR "/src/CustomResourceTest"
RUN dotnet build "CustomResourceTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomResourceTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomResourceTest.dll"]
