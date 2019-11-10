FROM mcr.microsoft.com/dotnet/core/sdk:3.0.100-bionic as build
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash - && apt-get install -y nodejs

COPY ./src/MicroUrl.Web/ClientApp/package.json /microurl/src/MicroUrl.Web/ClientApp/package.json 
COPY ./src/MicroUrl.Web/ClientApp/package-lock.json /microurl/src/MicroUrl.Web/ClientApp/package-lock.json
WORKDIR /microurl/src/MicroUrl.Web/ClientApp
RUN npm ci

WORKDIR /microurl
COPY ./MicroUrl.sln ./
COPY ./global.json ./
COPY ./src ./src
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.0-disco
WORKDIR /microurl
COPY --from=build /microurl /microurl
ENTRYPOINT ["dotnet", "/microurl/MicroUrl.Web.dll"]
