dotnet publish -c Release
docker build -t keepr ./bin/Release/net5.0/publish
docker tag keepr registry.heroku.com/dan-keepr/web
docker push registry.heroku.com/dan-keepr/web
heroku container:release web -a dan-keepr
echo press any key
read end