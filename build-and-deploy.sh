cd keepr-client # open client folder
npm run build # run build script (if you don't haven't installed your dependencies, this will fail)
cd ../keepr-server # open server folder
# deploy.sh won't work
# run deploy.sh won't either
# so... I'll just paste the deploy.sh code in here:
dotnet publish -c Release
docker build -t keepr ./bin/Release/net5.0/publish
docker tag keepr registry.heroku.com/dan-keepr/web
docker push registry.heroku.com/dan-keepr/web
heroku container:release web -a dan-keepr
echo press any key
read end