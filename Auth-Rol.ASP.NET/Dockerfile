FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5024
EXPOSE 5024

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Auth-Rol.ASP.NET/Auth-Rol.ASP.NET.csproj", "Auth-Rol.ASP.NET/"]
RUN dotnet restore "./Auth-Rol.ASP.NET/Auth-Rol.ASP.NET.csproj"
COPY . .
WORKDIR "/src/Auth-Rol.ASP.NET"
RUN dotnet tool install --global dotnet-ef 
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet build "./Auth-Rol.ASP.NET.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Auth-Rol.ASP.NET.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth-Rol.ASP.NET.dll"]