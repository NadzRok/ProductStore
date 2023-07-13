# ProductStore

Here it is, I have not had time to finish everything, but I did try and learn Angular with this one as well.
I have not learnt been able to get through as much as I wanted to but I do have a basic understanding of how Angular works and just need to put in a little more time to be able to work with it.

This is the API, it is a .NET 7 ASP.NET CORE WEB API.
It has a authorization, I wrote it to use JWT tokens, I did not have time to get to learning about Angular's side to get Auth up and running,
which is why I left the 'WeatherForecast' boilet plate in the api project, this is tagged with the auth so that you can see how it works with the swagger that is running on the project.
add you connection string to the 'myconn' var in the 'appsettings.json' then you can 'Update-Database' from the vs nuget package manager console, This will create the database with all the tables and get things ready so that a user can be created through swagger.
in swagger you will need to run the register endpoint and then the login endpoint with the registered username and password.
This will then give a token in the reply.
copy the tocken and click on the lock next that is shown on the 'WeatherForecast' endpoint.
In the input box, type 'beare '(with the space) and then paste the token after that and click login.
this will give you access to the 'WeatherForecast' endpoint.
all the other endpoint are for the front end but you can use swagger to test them and they will work, you don't need to be logged in for these.

for the Angular projec,
please bear with me, this is the 1st time I have worked with it and there is a lot of things that have not been added, but it runs and you can do .... with it.
I ran it though VS code and the API through VS2022, as long as they are running at the same time, they will talk to each other.

There is a lot more I could have done with this if I had a few days and I could have really beefed up the Product code and Catagory Code generators a bit more.
I have also had to lots of work things to do, additions to the scope after the project, adding more, change requests, the normal life of a dev.
