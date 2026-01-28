# DEPENDENCIES --- GenteFit (.NET)

This document describes all the dependencies required to run the
**GenteFit** project correctly, both for the main .NET application and
for the Odoo integration.

------------------------------------------------------------------------

## Core Application (.NET)

### Software Requirements

To run the GenteFit application you need:

-   **Windows 10 / Windows 11**
-   **Visual Studio Community 2022** (or newer)
-   **.NET Desktop Runtime**
-   **SQL Server** (recommended: SQL Server Express)

The application is built with:

-   C#
-   .NET
-   WinForms
-   ADO.NET

------------------------------------------------------------------------

### Installing Visual Studio

1.  Download Visual Studio Community from:
    https://visualstudio.microsoft.com/

2.  During installation, select at least:

-   **.NET desktop development**

This will install:

-   .NET SDK
-   WinForms tools
-   C# compiler

------------------------------------------------------------------------

### Installing SQL Server

1.  Download SQL Server Express from Microsoft official website.

2.  Run the installer and select the **Basic** installation.

3.  Once installed, verify that the SQL Server service is running.

4.  (Optional) Install **SQL Server Management Studio (SSMS)** to manage
    the database visually.

------------------------------------------------------------------------

## .NET Project Configuration

### appsettings.json file

For security reasons, real credentials are not included in the
repository.

Create the real configuration file from the example:

```
    appsettings.json.example → appsettings.json
```

Inside `appsettings.json` you must configure:

-   SQL Server connection string
-   Server name
-   Database name

Example:

    Server=localhost;
    Database=GenteFit;
    Trusted_Connection=True;

------------------------------------------------------------------------

### Database

-   The database is hosted in SQL Server.
-   After installation, an **initial data loading tool** is provided.
-   This initial load is required to log in and use the application.

It includes minimal data such as:

-   Initial users
-   Roles
-   Activities
-   Rooms

------------------------------------------------------------------------

## Odoo Integration

Odoo integration was developed as an extension of the core project.

### Additional Requirements

-   **Python 3**
-   An **Odoo** instance
-   Access to the Odoo database

------------------------------------------------------------------------

### Python Environment Setup

1.  Install Python from: https://www.python.org/

2.  Create the configuration file:

```
.env.example → .env
```

3.  Configure in `.env`:

-   Odoo server URL
-   Username
-   Password
-   Database name

------------------------------------------------------------------------

### Integration Functionality

The integration allows:

-   Sending data from GenteFit to Odoo
-   Centralizing information in the ERP
-   Working with custom modules

Communication is handled through:

-   Python
-   Odoo API
-   XML data exchange

------------------------------------------------------------------------

## Dependencies Summary

### Core

-   Windows
-   Visual Studio Community
-   .NET
-   SQL Server
-   ADO.NET

### ERP

-   Python
-   Odoo
-   XML

------------------------------------------------------------------------

## Notes

-   Real credentials are not stored in the repository.
-   SQL Server and Odoo must be configured to test all features.
-   See the main README files for full setup and usage instructions.


[**Main project README**](README.md)

[**English documentation**](README_en.md)
