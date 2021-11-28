FROM mcr.microsoft.com/dotnet/aspnet:3.1-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1-focal AS build
WORKDIR /src
COPY ["API1.csproj", "./"]
RUN dotnet restore "API1.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "API1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API1.dll"]
