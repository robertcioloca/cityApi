## requirements
- .NET 9 SDK
- SQL Server (local)
- VS Code or Visual Studio

- make sure you run migrations with **`dotnet ef database update --project CityApi.Data --startup-project CityApi.Api`**

## to run the api:
cd CityApi.Api
dotnet run

## watch:
dotnet watch --project CityApi.Api

## to run tests:
dotnet test

## example migration:
dotnet ef migrations add InitialCreate --project CityApi.Data --startup-project CityApi.Api <br/>
dotnet ef database update --project CityApi.Data --startup-project CityApi.Api <br/>

## Example usage:
Add a city with the name London, this should return country and weather data after when calling the get endpoint. <br/>
Make sure you add your own API_KEY for openweathermap