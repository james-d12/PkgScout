# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY ./src .

WORKDIR PkgScout.CLI

RUN dotnet restore PkgScout.CLI.csproj

RUN dotnet publish PkgScout.CLI.csproj -c Release --no-restore -o /app/out/PkgScout.CLI

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

COPY --from=build "/app/out/PkgScout.CLI" ./

ENTRYPOINT ["./PkgScout.CLI"]