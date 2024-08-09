# concepts-cs 

## Running the application 

In order to run the application as a console app, just execute: 
```
run.cmd
```

## Deployment 

Add the following package reference to the `Concepts.WebApi.csproj`:
```XML
  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Hosting.WindowsServices" Version="3.1.0" />
  </ItemGroup>
```

In `Program.cs` add `.UseWindowsService()` as follows:
```C#
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                ...
```

Publish the app as cross-platform and framework-dependent: 
```
dotnet publish -c Release -o ./publish
```

It's recommended to move all the files and folders from the `publish` folder to the folder, where all of your services are located. 
So create a folder for your project inside the services' folder (e.g. `C:\Services\ConceptsService`).

In order to deploy the application as a Windows Service, open CMD as adiministrator and execute: 
```
sc create ConceptsService binPath=C:\Services\ConceptsService\Concepts.WebApi.exe
sc description ConceptsService "Windows service for studying programming concepts"
```

In order to start the service, execute: 
```
sc start ConceptsService
```

Then test HTTP GET requests of your application by typing: 
```
curl --location --request GET http://localhost:5000/concept/solid/srp
```
or 
```
curl --location --request GET https://localhost:5001/concept/solid/srp
```

According to [this source](https://gist.github.com/subfuzion/08c5d85437d5d4f00e58), you can test POST methods using the following command: 
```
curl -d "{\"name\":\"srp\",\"family\":\"SOLID\"}" -H "Content-Type: application/json" -L -X POST http://localhost:5000/concept
```

Stop the service with the following command: 
```
sc stop ConceptsService
```

In order to delete the Windows Service, execute:
```
sc delete ConceptsService
```
