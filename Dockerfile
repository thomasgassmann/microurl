FROM mcr.microsoft.com/dotnet/core/sdk:3.0.100-bionic as build
RUN apt-get update && apt-get install -y ca-certificates curl gnupg && mkdir -p /etc/apt/keyrings
RUN curl -fsSL https://deb.nodesource.com/gpgkey/nodesource-repo.gpg.key | gpg --dearmor -o /etc/apt/keyrings/nodesource.gpg

ENV NODE_MAJOR=16
RUN echo "deb [signed-by=/etc/apt/keyrings/nodesource.gpg] https://deb.nodesource.com/node_$NODE_MAJOR.x nodistro main" | tee /etc/apt/sources.list.d/nodesource.list

RUN apt-get update && apt-get install nodejs -y
RUN curl -L https://npmjs.org/install.sh | sh

COPY ./src/MicroUrl.Web/ClientApp/package.json /microurl/src/MicroUrl.Web/ClientApp/package.json 
COPY ./src/MicroUrl.Web/ClientApp/package-lock.json /microurl/src/MicroUrl.Web/ClientApp/package-lock.json
WORKDIR /microurl/src/MicroUrl.Web/ClientApp
RUN npm ci

WORKDIR /microurl
COPY ./MicroUrl.sln ./
COPY ./global.json ./
COPY ./Directory.Build.targets ./
COPY ./src ./src
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.0-disco
WORKDIR /microurl
COPY --from=build /microurl/src/MicroUrl.Web/bin/Release/netcoreapp3.0/publish /microurl
ENTRYPOINT ["dotnet", "/microurl/MicroUrl.Web.dll"]
