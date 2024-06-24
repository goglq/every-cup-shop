FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY *.sln .
COPY EveryCupShop.Core/*.csproj ./EveryCupShop.Core/
COPY EveryCupShop.Infrastructure/*.csproj ./EveryCupShop.Infrastructure/
COPY EveryCupShop.WebApi/*.csproj ./EveryCupShop.WebApi/
RUN dotnet restore

COPY . .
WORKDIR /src/EveryCupShop.WebApi
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 80

ENTRYPOINT ["dotnet", "EveryCupShop.WebApi.dll"]