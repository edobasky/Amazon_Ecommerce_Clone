# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy only the project files needed to restore dependencies
COPY ["Basket/Basket/Basket.csproj", "Basket/Basket/"]
COPY ["Infrastructure/Common.Logging/Common.Logging.csproj", "Infrastructure/Common.Logging/"]
COPY ["Infrastructure/EventBus.Messages/EventBus.Messages.csproj", "Infrastructure/EventBus.Messages/"]

RUN dotnet restore "Basket/Basket/Basket.csproj"

# Copy only the necessary source folders (no Discount, no COPY . .)
COPY Basket/Basket/. Basket/Basket/
COPY Infrastructure/Common.Logging/. Infrastructure/Common.Logging/
COPY Infrastructure/EventBus.Messages/. Infrastructure/EventBus.Messages/

WORKDIR /src/Basket/Basket
RUN dotnet build "Basket.csproj" -c Release -o /app/build
RUN dotnet publish "Basket.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Basket.dll"]
