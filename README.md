# Medical System API

## Overview

Medical System API is a RESTful web application developed in ASP.NET Core and Entity Framework Core for managing patients, doctors, medications, disease histories, and specialist appointments.

The project was developed as part of the PPPK course and demonstrates the use of PostgreSQL, Docker, Entity Framework Core, Code First development, migrations, and CRUD operations.

---

## Technologies

* ASP.NET Core 8
* Entity Framework Core
* PostgreSQL
* Docker & Docker Compose
* Swagger/OpenAPI
* C#

---

## Features

### Patient Management

* Create, read, update and delete patients
* Unique OIB validation
* Personal information management

### Disease History Management

* Track patient diseases
* Store diagnosis periods
* Link diseases to patients

### Medication Management

* Prescribe medications
* Store dosage information
* Track medication frequency

### Appointment Management

* Schedule specialist examinations
* Connect patients and doctors
* Support different examination types

### Doctor Management

* Store doctor information
* Manage specializations
* Reference doctors in appointments

---

## Database

The application uses PostgreSQL as the primary relational database.

Entity Framework Core Code First approach is used for:

* Entity modeling
* Database creation
* Schema management
* Data migrations

---

## Running the Application

### Start PostgreSQL with Docker

```bash
docker-compose up -d
```

### Apply Migrations

```bash
dotnet ef database update
```

### Run Application

```bash
dotnet run
```

---

## API Documentation

Swagger is enabled and available after application startup:

```text
https://localhost:<port>/swagger
```

---

## Project Structure

```text
MedicalSystemAPI
├── Controllers
├── Models
├── DTOs
├── Data
├── Migrations
├── ORM
├── Helpers
├── docker
├── appsettings.json
└── Program.cs
```

---

## Implemented Requirements

* PostgreSQL database
* Docker containerization
* Entity Framework Core
* Code First development
* Database migrations
* CRUD operations
* Relational data modeling
* Eager loading using Entity Framework Include()
* Validation and constraints

---

## Author

Ana Marija Omrčen

PPPK – Pristup podacima iz programskog koda
