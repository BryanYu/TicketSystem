#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TicketSystem.API/TicketSystem.API.csproj", "TicketSystem.API/"]
COPY ["TicketSystem.Core/TicketSystem.Core.csproj", "TicketSystem.Core/"]
RUN dotnet restore "TicketSystem.API/TicketSystem.API.csproj"
COPY . .
WORKDIR "/src/TicketSystem.API"
RUN dotnet build "TicketSystem.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicketSystem.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketSystem.API.dll"]