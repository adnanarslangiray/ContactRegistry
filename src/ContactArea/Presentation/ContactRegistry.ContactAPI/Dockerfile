#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ContactArea/Presentation/ContactRegistry.ContactAPI/ContactRegistry.ContactAPI.csproj", "src/ContactArea/Presentation/ContactRegistry.ContactAPI/"]
COPY ["src/EventBusArea/RabbitMQEventBus/RabbitMQEventBus.csproj", "src/EventBusArea/RabbitMQEventBus/"]
COPY ["src/ContactArea/Core/ContantRegistry.Application/ContantRegistry.Application.csproj", "src/ContactArea/Core/ContantRegistry.Application/"]
COPY ["src/Common/ContactRegistry.Common/ContactRegistry.Common.csproj", "src/Common/ContactRegistry.Common/"]
COPY ["src/ContactArea/Core/ContactRegistery.Domain/ContactRegistry.Domain.csproj", "src/ContactArea/Core/ContactRegistery.Domain/"]
COPY ["src/ContactArea/Infrastructure/ContactRegistry.Persistence/ContactRegistry.Persistence.csproj", "src/ContactArea/Infrastructure/ContactRegistry.Persistence/"]
RUN dotnet restore "src/ContactArea/Presentation/ContactRegistry.ContactAPI/ContactRegistry.ContactAPI.csproj"
COPY . .
WORKDIR "/src/src/ContactArea/Presentation/ContactRegistry.ContactAPI"
RUN dotnet build "ContactRegistry.ContactAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactRegistry.ContactAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactRegistry.ContactAPI.dll"]