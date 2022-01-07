FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/*.csproj ./src/
RUN dotnet restore

# copy everything else and build app
COPY src/. ./src/
WORKDIR /app/src
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/src/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "afl-dakboard.dll"]
