# STAGE01 - Build application and its dependencies
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk as build
WORKDIR /src

COPY Site.csproj ./
RUN dotnet restore Site.csproj
COPY . .

WORKDIR /src
RUN dotnet publish -c Release -o /app

FROM build as publish
RUN dotnet publish Site.csproj -c Release -o /app

FROM base as final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "Site.dll"]