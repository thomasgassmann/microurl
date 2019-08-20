FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.4-stretch-slim
WORKDIR /microurl
COPY src/MicroUrl/bin/Release/netcoreapp2.2/publish /microurl
ENTRYPOINT ["dotnet", "/microurl/MicroUrl.dll"]
