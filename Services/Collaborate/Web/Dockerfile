# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj
COPY Services/Collaborate/Web/Web.csproj Services/Collaborate/Web/
COPY Services/Collaborate/Application/Application.csproj Services/Collaborate/Application/
COPY Services/Collaborate/Infrastructure/Infrastructure.csproj Services/Collaborate/Infrastructure/

RUN dotnet restore Services/Collaborate/Web/Web.csproj

# Copiar todo el repo
COPY . .

# Publicar
RUN dotnet publish Services/Collaborate/Web/Web.csproj -c Release -o /app

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app .

ENTRYPOINT ["dotnet", "Web.dll"]


