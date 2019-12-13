# Cloud-Project


## Getting Started (Without docker)

### Prerequisites

Installing node10.x and npm

### Installing dependence

run `npm install` at project root.

### Running app

run `npm start` at project root. Acces app -> http://localhost:8080


## Getting Started (With docker)

### Prerequisites

Docker

.Net Core 3.0

### Running app

#### Running WebAPI
Go to WebAPI/CloudAPI Folder:
    - dotnet build
    - dotnet run

#### Running Front
Go to Front Folder:
    - run `docker build -t cloud-project .` at project root for build image

    - run `docker run -d -p {PORT}:8080 cloud-project` Acces app -> http://localhost:{PORT}

[WARNING] Maybe, the WebAPI will run on port 5001. If it is the case, you should change the destination port in Front/main.js file in variable "apiUrl"


## Authors

* **Maxime Deboffle**
* **Aminah Saidi** 
* **Pierre Sid-Idris**


