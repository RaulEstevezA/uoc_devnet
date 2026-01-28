# GenteFit (.NET) — Proyecto Académico

<p align="center">
  <img src="images/gentefit.png" alt="Vista general de GenteFit" width="350">
</p>

GenteFit es una aplicación de escritorio desarrollada en **C# (.NET / WinForms)** como parte de la asignatura  
**“Técnicas de persistencia de datos con .NET y programas ERP”** en la **Universitat Oberta de Catalunya (UOC)**.

Se trata de una aplicación de gestión de gimnasio diseñada para apoyar las operaciones diarias del centro, como la gestión de usuarios, programación de actividades, asignación de salas y reservas de clientes.

Este repositorio contiene el proyecto completo desarrollado durante el curso, incluyendo diseño de la aplicación, arquitectura en capas, persistencia de datos mediante **ADO.NET** y manejo de XML.  
Como extensión del núcleo de la aplicación .NET, el proyecto también incluye integración con el ERP **Odoo** utilizando **Python**.

El proyecto se presenta además como parte de un **portfolio profesional** desarrollado por el equipo **Dev.Net**.

---

## Índice

- [Descripción general](#descripción-general)
- [Funcionalidades principales](#funcionalidades-principales)
- [Arquitectura del proyecto](#arquitectura-del-proyecto)
- [Tecnologías utilizadas](#tecnologías-utilizadas)
- [Estructura del repositorio](#estructura-del-repositorio)
- [Instalación y configuración](#instalación-y-configuración)
- [Integración con Odoo](#integración-con-odoo)
- [Equipo](#equipo)
- [Licencia](#licencia)
- [Notas](#notas)

---

## Descripción general

GenteFit simula el funcionamiento de un gimnasio real desde el punto de vista de la gestión interna.  
Permite administrar usuarios con distintos roles, crear actividades, programar sesiones en salas concretas y gestionar reservas de clientes.

El objetivo académico del proyecto es demostrar:

- Uso de arquitectura en capas
- Persistencia de datos con ADO.NET
- Separación de responsabilidades mediante DAO
- Trabajo con bases de datos SQL Server
- Integración con un ERP externo (Odoo)
- Intercambio de datos mediante XML

---

## Funcionalidades principales

- Gestión de usuarios (clientes, monitores, administradores, etc.)
- Sistema de login con roles y permisos
- Alta y gestión de salas
- Creación de actividades
- Programación de sesiones (actividad + sala + monitor + horario)
- Reservas de clientes con control de plazas y lista de espera
- Persistencia completa de datos en SQL Server
- Carga inicial de datos tras la instalación
- Integración con Odoo como sistema ERP externo

---

## Arquitectura del proyecto

La aplicación está organizada siguiendo una **arquitectura en capas**:

- **Capa de presentación**: Interfaces WinForms.
- **Capa de lógica de negocio**: Validaciones y reglas del sistema.
- **Capa de acceso a datos (DAO)**: Comunicación con SQL Server mediante ADO.NET.

Este enfoque permite:

- Separar responsabilidades
- Facilitar el mantenimiento
- Reutilizar código
- Cambiar la base de datos sin afectar a la interfaz
- Mejorar la escalabilidad del proyecto

El acceso a datos se realiza mediante clases DAO que encapsulan las operaciones CRUD (Create, Read, Update, Delete).

---

## Tecnologías utilizadas

### Aplicación principal

- C#
- .NET
- WinForms
- ADO.NET
- SQL Server
- XML

### Integración ERP

- Python
- Odoo

---

## Estructura del repositorio

La estructura puede variar ligeramente, pero de forma general incluye:

- Proyecto principal .NET (WinForms)
- Clases de modelo
- DAO de acceso a datos
- Scripts y utilidades de carga de datos
- Archivos XML
- Proyecto Python para integración con Odoo
- Documentación adicional
- Archivos de configuración de ejemplo

---

## Instalación y configuración

### Requisitos

Para utilizar todas las funcionalidades es necesario:

- SQL Server instalado localmente
- Instancia de Odoo para la integración ERP
- Visual Studio Community

### Configuración de la aplicación .NET

1. Copiar el archivo:

```
appsettings.json.example → appsettings.json
```

2. Editar `appsettings.json` e indicar la cadena de conexión a SQL Server.

3. Compilar y ejecutar el proyecto desde Visual Studio.

### Base de datos

- La base de datos se crea en SQL Server.
- Tras la instalación existe una herramienta para realizar la **carga inicial de datos**, necesaria para poder iniciar sesión y usar la aplicación.

---

## Integración con Odoo

Como parte del proyecto, se implementó una integración con Odoo para simular un entorno empresarial real.

Esta integración permite:

- Enviar datos desde GenteFit hacia Odoo
- Centralizar información en el ERP
- Trabajar con módulos personalizados

La comunicación se realiza mediante Python y mecanismos de intercambio de datos (XML / API).

### Configuración Odoo

1. Copiar:

```
.env.example → .env
```

2. Configurar credenciales y parámetros de conexión con Odoo.

---

## Equipo

Proyecto desarrollado por el equipo **Dev.Net**:

- **Daniel Carrasco**  
  GitHub: https://github.com/CarrasDev  
  LinkedIn: https://www.linkedin.com/in/danielcarrascoluque/

- **Marcos Llimera**  
  GitHub: https://github.com/MarcodigoDev  
  LinkedIn: https://www.linkedin.com/in/marcos-llimera-aparisi-024779298/

- **Raúl Estévez**  
  GitHub: https://github.com/RaulEstevezA  
  LinkedIn: https://www.linkedin.com/in/raulesteveza/

---

## Licencia

Este proyecto está licenciado bajo la licencia MIT.  
Consulta el archivo [LICENSE](LICENSE) para más detalles.

---

## Notas

- Las credenciales reales no se almacenan en el repositorio.
- Es obligatorio configurar SQL Server y Odoo para probar todas las funcionalidades.
- Consulta la documentación adicional para instrucciones detalladas de uso.

[**README Inicial**](README.md)

[**Dependencias (Español)**](DEPENDENCIAS.md)
