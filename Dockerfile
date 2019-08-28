FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash - && apt-get install -y nodejs
WORKDIR /microurl
COPY src/ /microurl
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.4-stretch-slim
WORKDIR /microurl
COPY --from=build /microurl/bin/Release/netcoreapp3.0/publish /microurl
ENTRYPOINT ["dotnet", "/microurl/MicroUrl.dll"]
