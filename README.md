# HolidayApi

## Deployment
To test this project you first need to clone it to your machine. Then open cmd, navigate to 'HolidayApi' project folder and execute
```sh
dotnet ef database update
```

After configuring database, you can start testing this api. Launch the project by running
```sh
dotnet run
```
Then open https://localhost:{your port}/index.html

Thats it!

## Hosting
This api is hosted on <br />
Azure : https://holidaywebapi.azurewebsites.net/index.html <br />
Heroku : https://holiday-webapi.herokuapp.com/index.html <br />

NOTE: The database is not hosted on the cloud, so for the full testing, you will need to configure api locally as mentioned before.
