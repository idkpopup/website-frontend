FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
FROM microsoft/dotnet:2.2-sdk as build

WORKDIR /src

COPY Site.csproj ./
RUN dotnet restore Site.csproj
COPY . .

RUN dotnet build "Site.csproj" -c Release -o /app

FROM build as publish

RUN dotnet publish Site.csproj -c Release -o /app

FROM base as final
WORKDIR /app
COPY --from=publish /app .

ENV ASPNETCORE_URLS http://+:5000;https://+:5002
EXPOSE 5000
EXPOSE 5002
ENTRYPOINT ["dotnet", "Site.dll"]