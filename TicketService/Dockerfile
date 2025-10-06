# =========================
# Etapa 1: Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /workspace

# Copiamos los archivos de proyecto reales
COPY TicketService/TicketService.csproj TicketService/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/

# Restauramos dependencias
RUN dotnet restore TicketService/TicketService.csproj

# Copiamos todo el código fuente
COPY . .

# Compilamos
WORKDIR /workspace/TicketService
RUN dotnet build -c Release -o /app/build

# =========================
# Etapa 2: Publish
# =========================
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# =========================
# Etapa 3: Runtime
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Puerto interno
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketService.dll"]
