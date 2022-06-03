## Commands

Pack source
dotnet pack -o ../../../packages

Add local source
dotnet nuget add source C:\Users\Gonzalo\projects\net-microservices\packages -n PlayEconomy

Add "Common" package
dotnet add package Play.Common  

Remove Package
dotnet remove package Play.Common 
dotnet nuget remove source PlayEconomy