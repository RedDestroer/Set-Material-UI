FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 35358
EXPOSE 44315

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["webhost/webhost.csproj", "webhost/"]
RUN dotnet restore "webhost/webhost.csproj"
COPY . .
WORKDIR "/src/webhost"
RUN dotnet build "webhost.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "webhost.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "webhost.dll"]