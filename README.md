# ProductStore

This is the API, it is a .NET 7 ASP.NET CORE WEB API.
It has a authorization, I wrote it to use JWT tokens.
Which is why I left the 'WeatherForecast' boilet plate in the api project, this is tagged with the auth so that you can see how it works with the swagger that is running on the project.
add you connection string to the 'myconn' var in the 'appsettings.json' then you can 'Update-Database' from the vs nuget package manager console, This will create the database with all the tables and get things ready so that a user can be created through swagger.
in swagger you will need to run the register endpoint and then the login endpoint with the registered username and password.
This will then give a token in the reply.
copy the tocken and click on the lock next that is shown on the 'WeatherForecast' endpoint.
In the input box, type 'beare '(with the space) and then paste the token after that and click login.
this will give you access to the 'WeatherForecast' endpoint.
all the other endpoint are for the front end but you can use swagger to test them and they will work, you don't need to be logged in for these.
