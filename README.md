# jobs application api

Creation of project to learn ASP.NET Core.  The project is about to create job vacancy and application on vacancy

## Available Scripts

In the project directory, you can run:

### `dotnet ef migrations add InitialMigration -o Persistence/Migrations`

To generate migrations of persistence in the database, before of anything command or will not be possible to save the data.

### `dotnet ef migrations remove`

This command will remove migrations generated.

### `dotnet ef migrations update`

When there is changes in the persistence files, the migrations need to be updates.

### `dotnet run`

To run the app in the development.<br>
Open [https://localhost:7206/swagger](https://localhost:7206/swagger) to view it in the browser.
