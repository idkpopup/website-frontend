FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY Site.csproj .
COPY ["Site.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet build "Site.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish Site.csproj -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Site.dll"]
