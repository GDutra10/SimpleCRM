FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/1-Presentation/SimpleCRM.Backoffice.WebApp/SimpleCRM.Backoffice.WebApp.csproj", "src/1-Presentation/SimpleCRM.Backoffice.WebApp/"]
RUN dotnet restore "src/1-Presentation/SimpleCRM.Backoffice.WebApp/SimpleCRM.Backoffice.WebApp.csproj"
COPY . .
WORKDIR "/src/src/1-Presentation/SimpleCRM.Backoffice.WebApp"
RUN dotnet build "SimpleCRM.Backoffice.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleCRM.Backoffice.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleCRM.Backoffice.WebApp.dll"]
