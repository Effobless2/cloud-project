# Cloud-Project

## Getting Started

### Prerequisites

.Net Core 3.0

Docker

Installing node10.x and npm (if you don't wanna use Docker)

### Running WebAPI
#### With Command Lines
Go to WebAPI/CloudAPI Folder:
 - run ```dotnet build``` -> Installing dependencies and building Solution
 - run ```dotnet run``` -> Launch API
#### With Visual Studio
 - Open WebAPI/CloudAPI/CloudAPI.sln Solution with Visual Studio
 - Run the solution

### Running Front
#### Without Docker
Go to Front Folder:
 - run ```npm install``` -> Installing depedencies
 - run ```npm start``` Launch app -> http://localhost:8080
 
#### With Docker
Go to Front Folder:
 - run ```docker build -t cloud-project .``` -> build a Docker Image
 - run ```docker run -d -p {PORT}:8080 cloud-project``` -> Runs the docker container
 Access app -> http://localhost:{PORT}

[WARNING] Maybe, the WebAPI will run on port 5001. If it is the case, you should change the destination port in Front/main.js file in variable "apiUrl"

## Authors

* **Maxime Deboffle**
* **Aminah Saidi** 
* **Pierre Sid-Idris**
