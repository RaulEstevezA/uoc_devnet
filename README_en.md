# GenteFit (.NET) --- Academic Project

<p align="center">
  <img src="images/gentefit.png" alt="Vista general de GenteFit" width="350">
</p>

GenteFit is a desktop application developed in **C# (.NET / WinForms)**
as part of the academic course\
**"Técnicas de persistencia de datos con .NET y programas ERP"** at the
**Universitat Oberta de Catalunya (UOC)**.

It is a gym management application designed to support daily operations
such as user management, activity scheduling, room assignment, and
client reservations.

This repository contains the complete project developed during the
course, including application design, layered architecture, and data
persistence using **ADO.NET**, as well as XML handling.\
As an extension of the core .NET application, the project also includes
integration with the ERP system **Odoo** using **Python**.

The project is also presented as part of a **professional portfolio**
developed by the **Dev.Net** team.

------------------------------------------------------------------------

## Table of Contents

-   [Overview](#overview)
-   [Main Features](#main-features)
-   [Project Architecture](#project-architecture)
-   [Technologies Used](#technologies-used)
-   [Repository Structure](#repository-structure)
-   [Installation and Setup](#installation-and-setup)
-   [Odoo Integration](#odoo-integration)
-   [Team](#team)
-   [License](#license)
-   [Notes](#notes)

------------------------------------------------------------------------

## Overview

GenteFit simulates the internal management of a real gym.\
It allows administrators to manage users with different roles, create
activities, schedule sessions in specific rooms, and handle client
reservations.

The academic goals of this project are to demonstrate:

-   Layered architecture
-   Data persistence with ADO.NET
-   Separation of responsibilities using DAO
-   SQL Server database management
-   Integration with an external ERP system (Odoo)
-   Data exchange using XML

------------------------------------------------------------------------

## Main Features

-   User management (clients, instructors, administrators, etc.)
-   Login system with roles and permissions
-   Room creation and management
-   Activity creation
-   Session scheduling (activity + room + instructor + timetable)
-   Client reservations with capacity control and waiting list
-   Full data persistence in SQL Server
-   Initial data loading after installation
-   Integration with Odoo as an external ERP system

------------------------------------------------------------------------

## Project Architecture

The application follows a **layered architecture**:

-   **Presentation layer**: WinForms user interfaces.
-   **Business logic layer**: System rules and validations.
-   **Data access layer (DAO)**: Communication with SQL Server using
    ADO.NET.

This approach provides:

-   Clear separation of responsibilities
-   Easier maintenance
-   Code reusability
-   Ability to change the database without affecting the UI
-   Improved scalability

Data access is implemented through DAO classes that encapsulate CRUD
operations (Create, Read, Update, Delete).

------------------------------------------------------------------------

## Technologies Used

### Core Application

-   C#
-   .NET
-   WinForms
-   ADO.NET
-   SQL Server
-   XML

### ERP Integration

-   Python
-   Odoo

------------------------------------------------------------------------

## Repository Structure

The structure may vary slightly, but generally includes:

-   Main .NET (WinForms) project
-   Model classes
-   DAO data access classes
-   Data loading scripts and utilities
-   XML files
-   Python project for Odoo integration
-   Additional documentation
-   Example configuration files

------------------------------------------------------------------------

## Installation and Setup

### Requirements

To use all features, you will need:

-   SQL Server installed locally
-   An Odoo instance for ERP integration
-   Visual Studio Community

### .NET Application Setup

1.  Copy:

```
appsettings.json.example → appsettings.json
```

2.  Edit `appsettings.json` and set your SQL Server connection string.

3.  Build and run the project from Visual Studio.

### Database

-   The database is created in SQL Server.
-   After installation, an **initial data loading tool** is provided and
    must be executed in order to log in and use the application.

------------------------------------------------------------------------

## Odoo Integration

As part of the project, integration with Odoo was implemented to
simulate a real business environment.

This integration allows:

-   Sending data from GenteFit to Odoo
-   Centralizing information in the ERP
-   Working with custom modules

Communication is handled using Python and data exchange mechanisms (XML
/ API).

### Odoo Configuration

1.  Copy:

```
.env.example → .env
```


2.  Configure credentials and connection parameters for Odoo.

------------------------------------------------------------------------

## Team

Project developed by the **Dev.Net** team:

-   **Daniel Carrasco**\
    GitHub: https://github.com/CarrasDev\
    LinkedIn: https://www.linkedin.com/in/danielcarrascoluque/

-   **Marcos Llimera**\
    GitHub: https://github.com/MarcodigoDev\
    LinkedIn:
    https://www.linkedin.com/in/marcos-llimera-aparisi-024779298/

-   **Raúl Estévez**\
    GitHub: https://github.com/RaulEstevezA\
    LinkedIn: https://www.linkedin.com/in/raulesteveza/

------------------------------------------------------------------------

## License

This project is licensed under the MIT License.
See the [LICENSE](LICENSE) file for details.

------------------------------------------------------------------------

## Notes

-   Real credentials are not stored in the repository.
-   SQL Server and Odoo must be configured to test all features.
-   Please refer to the additional documentation for detailed usage
    instructions.

[**Main project README**](README.md)

[**Dependencies**](DEPENDENCIES.md)
