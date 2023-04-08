FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["service.csproj", "user-worker-service/"]
RUN dotnet restore "user-worker-service/service.csproj"

WORKDIR "/src/user-worker-service"
COPY . .

RUN dotnet build "service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "service.dll"]
