#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Adapters/TechChallenge.Api/TechChallenge.Api.csproj", "src/Adapters/TechChallenge.Api/"]
COPY ["src/Adapters/TechChallenge.Infra/TechChallenge.Infra.csproj", "src/Adapters/TechChallenge.Infra/"]
COPY ["src/Core/TechChallenge.Application/TechChallenge.Application.csproj", "src/Core/TechChallenge.Application/"]
COPY ["src/Core/TechChallenge.Domain/TechChallenge.Domain.csproj", "src/Core/TechChallenge.Domain/"]
RUN dotnet restore "src/Adapters/TechChallenge.Api/TechChallenge.Api.csproj"
COPY . .
WORKDIR "/src/src/Adapters/TechChallenge.Api"
RUN dotnet build "TechChallenge.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TechChallenge.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechChallenge.Api.dll"]