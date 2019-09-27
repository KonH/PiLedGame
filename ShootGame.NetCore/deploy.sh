dotnet publish -r linux-arm
rsync -avh bin/Debug/netcoreapp2.1/linux-arm/publish /Volumes/share
