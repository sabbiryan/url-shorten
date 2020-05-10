#### Goal
Build and configur a url shorten service using .net core and deploy it on cloud.

#### Technologies
1. .Net Core 3.1
2. Entity Framework Core 3.1.3
3. NLog
4. Swagger
5. xUnit


#### Features
1. CRUD for tiny url
2. Public APIs for developer
3. Copy to clipboard

#### Instructions
1. Clone repository
2. Open on visual studio 2019
3. Check the connection string on appsettings.json (you may need to change the server name in appsettings.json and appsetting.Development.json)
4. Open package manager console, select UrlShorten.EntityFrameworkCore project and run update-database
5. Be sure UrlShorten.Web is set as startup project
6. Use ctrl + f5 to run the project

#### Note
1. You can repalce appsetting.json "ShortenWebRootPath": "http://localhost:59326/api/Play/" with your own url mapping. also consider the related changes on appsettings.Development.json

Enjoy!!!
