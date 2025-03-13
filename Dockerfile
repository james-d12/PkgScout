# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY ./src .
COPY ./Directory.Build.props .

WORKDIR PkgScout.Console

RUN dotnet restore PkgScout.Console.csproj

RUN dotnet publish PkgScout.Console.csproj -c Release --no-restore -o /app/PkgScout.Console

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

COPY --from=build "/app/PkgScout.Console" ./

ENTRYPOINT ["./PkgScout.Console"]