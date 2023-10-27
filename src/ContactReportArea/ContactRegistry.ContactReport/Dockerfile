#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ContactReportArea/ContactRegistry.ContactReport/ContactRegistry.ContactReport.csproj", "src/ContactReportArea/ContactRegistry.ContactReport/"]
COPY ["src/Common/ContactRegistry.Common/ContactRegistry.Common.csproj", "src/Common/ContactRegistry.Common/"]
COPY ["src/EventBusArea/RabbitMQEventBus/RabbitMQEventBus.csproj", "src/EventBusArea/RabbitMQEventBus/"]
RUN dotnet restore "src/ContactReportArea/ContactRegistry.ContactReport/ContactRegistry.ContactReport.csproj"
COPY . .
WORKDIR "/src/src/ContactReportArea/ContactRegistry.ContactReport"
RUN dotnet build "ContactRegistry.ContactReport.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactRegistry.ContactReport.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactRegistry.ContactReport.dll"]