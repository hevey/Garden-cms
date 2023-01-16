FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Garden.UI/Garden.UI.csproj", "Garden.UI/"]
RUN dotnet restore "Garden.UI/Garden.UI.csproj"
COPY . .
WORKDIR "/src/Garden.UI"
RUN dotnet build "Garden.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Garden.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Garden.UI.dll"]
