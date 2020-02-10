FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build

WORKDIR /src

COPY Site.csproj .
RUN dotnet restore
COPY . .

RUN dotnet publish Site.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
COPY --from=build /app .

EXPOSE 80
ENTRYPOINT ["dotnet", "Site.dll"]
