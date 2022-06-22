# Description
This Project was created following the course: https://www.youtube.com/watch?v=CqCDOosvZIk, which gave me a basic understanding of how microservices work. We used MongoDb as the main data storage for both microservices. We also used Rabbit MQ for broker messaging among services.
The architecture can be deployed using docker-compose, but each microservice have to be runned individually.

## Commands

Pack source (Play.Catalog.Contracts Play.Common)
dotnet pack -o ../../../packages


Pack a new version of the common library
dotnet pack -p:PackageVersion=1.0.1 -o ..\..\..\packages\

Add local source
dotnet nuget add source C:\Users\Gonzalo\projects\net-microservices\packages -n PlayEconomy

Add "Common" package
dotnet add package Play.Common  

Remove Package
dotnet remove package Play.Common 
dotnet nuget remove source PlayEconomy

Add Reference to Contracts 
dotnet add reference ..\Play.Catalog.Contracts\Play.Catalog.Contracts.csproj