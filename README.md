## recipes-prn231
### Generate your database
Go to *BusinessObjects* & *API* -> *appsettings.json*, change the connection string to match yours.

Open the BusinessObjects project in terminal, run

`dotnet ef database update`
