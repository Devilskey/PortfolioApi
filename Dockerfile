#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["webApi.csproj", "webApi/"]
RUN dotnet restore "webApi/webApi.csproj"
COPY . webApi/
WORKDIR "/webApi"
RUN dotnet build "webApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "webApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "webApi.dll"]

EXPOSE 5244
