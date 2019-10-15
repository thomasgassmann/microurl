FROM mcr.microsoft.com/dotnet/core/sdk:3.0.100-disco as build
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash - && apt-get install -y nodejs
WORKDIR /microurl
COPY ./ /microurl
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.0-disco
WORKDIR /microurl
COPY --from=build /microurl/src/bin/Release/netcoreapp3.0/publish /microurl
ENTRYPOINT ["dotnet", "/microurl/MicroUrl.dll"]
