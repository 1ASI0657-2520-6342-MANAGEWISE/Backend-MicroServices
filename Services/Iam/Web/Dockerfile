# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj
COPY Services/Iam/Web/Web.csproj Services/Iam/Web/
COPY Services/Iam/Application/Application.csproj Services/Iam/Application/
COPY Services/Iam/Infrastructure/Infrastructure.csproj Services/Iam/Infrastructure/

RUN dotnet restore Services/Iam/Web/Web.csproj

# Copiar todo el repo
COPY . .

# Publicar
RUN dotnet publish Services/Iam/Web/Web.csproj -c Release -o /app

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app .

ENTRYPOINT ["dotnet", "Web.dll"]

