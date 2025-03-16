# Atlassian Demo

## Project Structure

This solution is created according to DDD design pattern. It contains a Service Layer that is referenced by 2 types of apps, a .NetCore 8 WebApi, and a Console app.
The Service layer depends on the other layers of configurations, Models, and Data.

The solution contains a test project targetting the Service Layer. The unit testing is implemented using MSTest.

## Technology/Tools
- .Net Core 8
- Entity Framework Core with MySql Connector
- Docker - MySql Instance
- MSTest

## Screenshots
The WebApi App

<img width="825" alt="image" src="https://github.com/user-attachments/assets/80f44286-8c36-46d3-8e93-0d7ea851870b" />


The Console App

<img width="667" alt="image" src="https://github.com/user-attachments/assets/14558d52-dc07-4688-9121-9e396bb6d5e6" />

## Running

Once the codebase has been cloned, it can be run using the following two commands (note that these commands should be run from the top level directory, where the `docker-compose.yml` file is located):

`docker compose build`

`docker compose up`

This will build and run the MySQL database inside a docker container.

## Database

A blank MySQL database is included inside the container, and will start up when the container is run. The ASP.NET Core backend is already configured with a connection string to this database.


# Before you run the app locally
- You may need to run migration. This will make sure that the database is up to date. You can do so by running the command of update-database in the console.
  
  <img width="730" alt="image" src="https://github.com/user-attachments/assets/95e1568b-afc1-4982-9f84-8e4348e28295" />


