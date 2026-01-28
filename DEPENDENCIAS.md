# DEPENDENCIAS --- GenteFit (.NET)

Este documento describe todas las dependencias necesarias para poder
ejecutar correctamente el proyecto **GenteFit**, tanto para la
aplicación principal en .NET como para la integración con Odoo.

------------------------------------------------------------------------

## Aplicación principal (.NET)

### Requisitos de software

Para ejecutar la aplicación GenteFit es necesario disponer de:

-   **Windows 10 / Windows 11**
-   **Visual Studio Community 2022** (o superior)
-   **.NET Desktop Runtime**
-   **SQL Server** (recomendado: SQL Server Express)

La aplicación está desarrollada en:

-   C#
-   .NET
-   WinForms
-   ADO.NET

------------------------------------------------------------------------

### Instalación de Visual Studio

1.  Descargar Visual Studio Community desde:
    https://visualstudio.microsoft.com/

2.  Durante la instalación, seleccionar al menos:

-   Desarrollo de escritorio con .NET

Esto instalará automáticamente:

-   SDK de .NET
-   Herramientas WinForms
-   Compilador C#

------------------------------------------------------------------------

### Instalación de SQL Server

1.  Descargar SQL Server Express desde el sitio oficial de Microsoft.

2.  Ejecutar el instalador y elegir instalación básica.

3.  Una vez instalado, verificar que el servicio SQL Server esté activo.

4.  (Opcional) Instalar **SQL Server Management Studio (SSMS)** para administrar la base de datos visualmente.

------------------------------------------------------------------------

## Configuración del proyecto .NET

### Archivo appsettings.json

Por motivos de seguridad, el archivo real no se incluye en el
repositorio.

Debe crearse a partir del ejemplo:

```
    appsettings.json.example → appsettings.json
```

Dentro de `appsettings.json` es necesario configurar:

-   Cadena de conexión a SQL Server
-   Nombre del servidor
-   Base de datos

Ejemplo:

    Server=localhost;
    Database=GenteFit;
    Trusted_Connection=True;

------------------------------------------------------------------------

### Base de datos

-   La base de datos se aloja en SQL Server.
-   Tras la instalación existe una herramienta de **carga inicial de datos**.
-   Esta carga es obligatoria para poder iniciar sesión en la aplicación.

Incluye datos mínimos como:

-   Usuarios iniciales
-   Roles
-   Actividades
-   Salas

------------------------------------------------------------------------

## Integración con Odoo

La integración con Odoo se desarrolló como extensión del proyecto
principal.

### Requisitos adicionales

-   **Python 3**
-   Instancia de **Odoo**
-   Acceso a la base de datos de Odoo

------------------------------------------------------------------------

### Configuración del entorno Python

1.  Instalar Python desde: https://www.python.org/

2.  Crear el archivo:

```
.env.example → .env
```

3.  Configurar en `.env`:

-   URL del servidor Odoo
-   Usuario
-   Contraseña
-   Base de datos

------------------------------------------------------------------------

### Funcionalidad de integración

La integración permite:

-   Enviar datos desde GenteFit hacia Odoo
-   Centralizar información en el ERP
-   Trabajar con módulos personalizados

La comunicación se realiza mediante:

-   Python
-   API de Odoo
-   Intercambio de datos XML

------------------------------------------------------------------------

## Resumen de dependencias

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

## Notas

-   Las credenciales reales no se almacenan en el repositorio.
-   Es obligatorio configurar SQL Server y Odoo para probar todas las funcionalidades.
-   Para más detalles consultar los README principales.


[**README Inicial**](README.md)

[**Documentación en español**](README_es.md)