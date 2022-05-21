FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SearchServiceAPI/SearchServiceAPI.csproj", "SearchServiceAPI/"]
RUN dotnet restore "SearchServiceAPI/SearchServiceAPI.csproj"
COPY . .
WORKDIR "/src/SearchServiceAPI"
RUN dotnet build "SearchServiceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SearchServiceAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SearchServiceAPI.dll"]
