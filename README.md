# UserEngagementService
Microservice to send messages to users prefered communication channels

Functionality:


# Api documentation

Api Swagger documentation could be found after starting the application at: 
http://localhost:5000/user-engagement/swagger/index.html


# Database
In order to use API MS SQl Server is needed. 
In appsetings.json connection string is configured to 1439 Port, and for default password @test123. 

---

For Mac/Linux users its possible to use MS Sql Docker image (https://hub.docker.com/_/microsoft-mssql-server)

To pull docker image use command:
docker pull mcr.microsoft.com/mssql/server

To run MS SQL Server instance on docker use command: 
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=@test123" -p 1439:1433 -d mcr.microsoft.com/mssql/server:2022-latest
 
