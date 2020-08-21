FROM mcr.microsoft.com/dotnet/core/samples:aspnetapp
COPY ./bin/Release/netcoreapp3.1/publish /WebApp
WORKDIR /WebApp
ENTRYPOINT ["dotnet", "WebApp.dll"]
