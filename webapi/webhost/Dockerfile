FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY webhost/set-material-ui-webhost.csproj webhost/
RUN dotnet restore webhost/set-material-ui-webhost.csproj
COPY . .
WORKDIR /src/webhost
RUN dotnet build set-material-ui-webhost.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish set-material-ui-webhost.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "set-material-ui-webhost.dll"]
