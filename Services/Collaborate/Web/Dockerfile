# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copiamos los csproj de TODAS las capas para aprovechar el caché de Docker
# Si solo copias el Web y este depende de Application, fallará el restore.
COPY ["Services/Collaborate/Domain/Domain.csproj", "Services/Collaborate/Domain/"]
COPY ["Services/Collaborate/Application/Application.csproj", "Services/Collaborate/Application/"]
COPY ["Services/Collaborate/Infrastructure/Infrastructure.csproj", "Services/Collaborate/Infrastructure/"]
COPY ["Services/Collaborate/Web/Web.csproj", "Services/Collaborate/Web/"]

# 2. Restauramos dependencias
RUN dotnet restore "Services/Collaborate/Web/Web.csproj"

# 3. Copiamos todo el código fuente
COPY . .

# 4. Construimos y Publicamos
WORKDIR "/src/Services/Collaborate/Web"
RUN dotnet publish "Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80

# Copiamos los archivos publicados desde la etapa de build
COPY --from=build /app/publish .

# ENTRYPOINT: Asegúrate que el nombre de la DLL sea correcto. 
# Si tu proyecto se llama "Web.csproj", suele ser "Web.dll" o "AidManager.Collaborate.Web.dll"
# Revisa en tus propiedades del proyecto si tienes un <AssemblyName> específico.
ENTRYPOINT ["dotnet", "Web.dll"]