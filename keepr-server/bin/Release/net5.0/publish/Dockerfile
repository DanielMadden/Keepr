FROM mcr.microsoft.com/dotnet/sdk:5.0

COPY . App/

WORKDIR /App

CMD ASPNETCORE_URLS=http://*:$PORT dotnet keepr.dll