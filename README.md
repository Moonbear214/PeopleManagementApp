# Brief implementation Explenation

The backend for this solution was developed using .NET 8, following Clean Architecture. This pattern was chosen to create a system that is easily testable and maintainable by separating the application into distinct layers.

## The Dependency Rule

A key principle of this architecture is that dependencies must always flow inward. The outer layers can depend on inner layers, but the inner layers know nothing about the outer ones. This ensures our core business logic (Application) is independent of the Api layer and the database (Infrastructure).

## Project Structure Breakdown

The solution is split into the following projects:

### 1. CoFloPeopleManagement.Infrastructure

This contains the core domain entities, the AppDbContext, and the data access logic.

### 2. CoFloPeopleManagement.Application

This contains the core business logic. It defines the DTOs and contains the PeopleService, which orchestrates the application's operations.

### 3. CoFloPeopleManagement.Api

This is the entry point of the application, responsible for handling all HTTP requests and responses.


# How to Deploy CoFlo Management Solution

This solution supports two primary deployment methods:

* **Local Deployment**: Run the .NET API and React frontend directly on your machine for development and testing.
* **Containerized Deployment**: Use Docker to run the backend API in a consistent, production-like environment.

---

## Prerequisites

Ensure the following tools are installed:

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [Node.js (v18 or higher)](https://nodejs.org/)
* [Docker Desktop](https://www.docker.com/products/docker-desktop) (for containerized deployment)
* An IDE such as [Visual Studio Code](https://code.visualstudio.com/) or [JetBrains Rider](https://www.jetbrains.com/rider/)

---

## Option 1: Local Deployment

This method runs the frontend and backend as separate processes on your local machine.

### Step 1: Run the Backend API

1. Open a terminal and navigate to the API project directory (`cd CoFloPeopleManagement/CoFloPeopleManagement.Api`).

2. Restore dependencies:

   ```bash
   dotnet restore
   ```

3. Run the application:

   ```bash
   dotnet run
   ```

4. The API will start and listen on the configured port (`8080`).
   You can access the Swagger UI at:

   ```
   http://localhost:8080/swagger
   ```

### Step 2: Run the Frontend React App

1. Open a new terminal and navigate to the frontend project directory (`cd coflo-web`).

2. Install dependencies:

   ```bash
   npm install
   ```

3. Start the development server:

   ```bash
   npm run dev
   ```

4. The Front End will be accessible at:

   ```
   http://localhost:3000
   ```

The frontend is configured to communicate with the backend running at `http://localhost:8080`.

---

## Option 2: Containerized Deployment (Using Docker)

This method packages the backend API into a Docker container.

### Step 1: Build the Docker Image

1. Ensure Docker Desktop is running.
2. Open a terminal and navigate to the root directory of your solution (`cd CoFloPeopleManagement`).
3. Build the Docker image:

   ```bash
   docker build -t coflo-api .
   ```

This command uses the Dockerfile to build your solution and create an image named `coflo-api`.

### Step 2: Run the Docker Container

Run the container using the following command:

```bash
docker run -d -p 8080:8080 --name coflo-backend coflo-api
```

Explanation of options:

* `-d`: Run the container in detached (background) mode
* `-p 8080:8080`: Map port 8080 on your host to port 8080 in the container
* `--name coflo-backend`: Assign a name to the container

Once running, the backend API will be accessible at:

```
http://localhost:8080/swagger
```

### Step 3: Run the Frontend

Follow the same steps as in Option 1, Step 2 to run the React frontend and access at `http://localhost:3000`. It will connect to the backend API running in Docker at `http://localhost:8080` (Note, with current configurations, the swagger will not be accessible when running using docker).
