# Atlassian Demo

## Project Structure

This solution is created according to DDD design pattern. It contains a Service Layer that is exposed by 2 types of apps, a .NetCore 8 WebApi, and a Console app.
The Service layer depends on the other layers of configurations, Models, and Data. 

## Running

Once the codebase has been cloned, it can be run using the following two commands (note that these commands should be run from the top level directory, where the `docker-compose.yml` file is located):

`docker compose build`

`docker compose up`

This will build and run the MySQL database inside a docker container.

## Database

A blank MySQL database is included inside the container, and will start up when the container is run. The ASP.NET Core backend is already configured with a connection string to this database.


# Before you run the app locally
- You may need to run migration. This will make sure that the database is up to date. You can do so by running the command of update-database in the console.
  
  <img width="577" alt="image" src="https://github.com/user-attachments/assets/30e7a977-3153-41df-b69b-fbeee91f5266" />

