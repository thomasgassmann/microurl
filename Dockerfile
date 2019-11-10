FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.0-disco
WORKDIR /microurl
COPY ./src/MicroUrl.Web/bin/Release/netcoreapp3.0/publish /microurl
ENTRYPOINT ["dotnet", "/microurl/MicroUrl.Web.dll"]
