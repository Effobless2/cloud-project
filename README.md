# Cloud-Project 🍆

## Getting Started

### Prerequisites

 - SqlServer
 - .Net Core 3.0
 - Docker
 - Node.js and npm (if you don't wanna use Docker)

### Installing DataBase
 - Create a Database called 'TodoDB'
 - Run the Script from DBFiles/Todo.sql
 [Note] If you don't create the database locally, you should update WebAPI/CloudAPI/appsettings.json File and changing the "ConnectionString" value with the correct one.
 
### Running WebAPI
#### With Command Lines
Go to WebAPI/CloudAPI Folder:
 1. run ```dotnet build``` -> Installing dependencies and building Solution
 2. run ```dotnet run``` -> Launch API
#### With Visual Studio
 1. Open WebAPI/CloudAPI/CloudAPI.sln Solution with Visual Studio
 2. Run the solution

### Running Front
#### Without Docker
Go to Front Folder:
 1. run ```npm install``` -> Installing depedencies
 2. run ```npm start``` Launch app -> http://localhost:8080
 
#### With Docker
Go to Front Folder:
 1. run ```docker build -t cloud-project .``` -> build a Docker Image
 2. run ```docker run -d -p {PORT}:8080 cloud-project``` -> Runs the docker container
 Access app -> http://localhost:{PORT}

[WARNING] Maybe, the WebAPI will run on port 5001. If it is the case, you should change the destination port in Front/main.js file in variable "apiUrl"

## Authors

* **Maxime Deboffle**
* **Aminah Saidi** 
* **Pierre Sid-Idris**
