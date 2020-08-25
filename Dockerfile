FROM mcr.microsoft.com/dotnet/core/samples:aspnetapp
ENV urls_list=http://server1/\ http://server2/\ http://server3/
COPY ./bin/Release/netcoreapp3.1/publish /WebApp
WORKDIR /WebApp
ENTRYPOINT ["dotnet", "WebApp.dll"]
