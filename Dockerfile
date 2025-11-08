# Dockerfile for CCMS.Web (.NET 9)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
ARG APP_UID=1000
RUN adduser --disabled-password --gecos "" --uid ${APP_UID} appuser || true
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# cache csproj(s) and nuget
COPY ["NuGet.Config", "."]
COPY ["src/CCMS.Web/CCMS.Web.csproj", "src/CCMS.Web/"]
COPY ["src/CCMS.Application/CCMS.Application.csproj", "src/CCMS.Application/"]
COPY ["src/CCMS.Domain/CCMS.Domain.csproj", "src/CCMS.Domain/"]
COPY ["src/CCMS.Domain.Shared/CCMS.Domain.Shared.csproj", "src/CCMS.Domain.Shared/"]
COPY ["src/CCMS.Application.Contracts/CCMS.Application.Contracts.csproj", "src/CCMS.Application.Contracts/"]
COPY ["src/CCMS.HttpApi/CCMS.HttpApi.csproj", "src/CCMS.HttpApi/"]
COPY ["src/CCMS.EntityFrameworkCore/CCMS.EntityFrameworkCore.csproj", "src/CCMS.EntityFrameworkCore/"]

RUN dotnet restore "src/CCMS.Web/CCMS.Web.csproj"

COPY . .
WORKDIR "/src/src/CCMS.Web"
RUN dotnet build "CCMS.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CCMS.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER root
RUN chown -R appuser:appuser /app || true
USER appuser
ENTRYPOINT ["dotnet", "CCMS.Web.dll"]
