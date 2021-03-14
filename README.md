# Simple Leaderboard

This is a simple app to display an event which houses several leaderboards with a combined view. It uses guids rather that user auth and is intended for simple competitions via sharing a public URL.

The easiest way to see this app work it to set up an Azure App Service and SQL Server (Note: As of March 2021 Blazor WASM isnt supported on Linux App Service). Update the connection strings and navigate to {your-app}.azurewebsites.net/admin to create an event.

