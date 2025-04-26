# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution, projects and restore

COPY ["SupplementsShop.Web/SupplementsShop.Web.csproj", "SupplementsShop.Web/"]
COPY ["SupplementsShop.Application/SupplementsShop.Application.csproj", "SupplementsShop.Application/"]
COPY ["SupplementsShop.Infrastructure/SupplementsShop.Infrastructure.csproj", "SupplementsShop.Infrastructure/"]
COPY ["SupplementsShop.Domain/SupplementsShop.Domain.csproj", "SupplementsShop.Domain/"]

RUN dotnet restore "SupplementsShop.Web/SupplementsShop.Web.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/SupplementsShop.Web"
RUN dotnet publish -c Release -o /app/publish

# Stage 2: final runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "SupplementsShop.Web.dll"]