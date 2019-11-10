FROM mcr.microsoft.com/dotnet/core/sdk:3.0 as build
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash - && apt-get install -y nodejs

COPY ./src/MicroUrl.Web/ClientApp/package.json /microurl/src/MicroUrl.Web/ClientApp/package.json 
COPY ./src/MicroUrl.Web/ClientApp/package-lock.json /microurl/src/MicroUrl.Web/ClientApp/package-lock.json
WORKDIR /microurl/src/MicroUrl.Web/ClientApp
RUN npm ci

WORKDIR /microurl
COPY ./ /microurl
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.0-disco
WORKDIR /microurl
COPY --from=build /microurl /microurl
ENTRYPOINT ["dotnet", "/microurl/MicroUrl.Web.dll"]
