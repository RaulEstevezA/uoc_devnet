# GenteFit (.NET) — Academic Project

<p align="center">
  <img src="images/gentefit.png" alt="GenteFit overview" width="350">
</p>

GenteFit is a desktop application developed in **C# (.NET / WinForms)** as part of the academic course  
**“Técnicas de persistencia de datos con .NET y programas ERP”** at the **Universitat Oberta de Catalunya (UOC)**.

It is a gym management application designed to support daily operations such as user management, activity scheduling, room assignment, and client reservations.

This repository contains the complete project developed during the course, including application design, layered architecture, data persistence using **ADO.NET**, and XML handling.  
As an extension of the core .NET application, the project also includes integration with the ERP system **Odoo** using **Python**.

The project is also presented as part of a professional portfolio developed by the **Dev.Net** team.

---

## Documentation

Extended documentation is available in both languages:

- 🇬🇧 [**English documentation**](README_en.md)
- 🇪🇸 [**Documentación en español**](README_es.md)

---

## Dependencies & Requirements

Technical requirements and external libraries are documented separately:

- 🇬🇧 [**Dependencies (English)**](DEPENDENCIES.md)
- 🇪🇸 [**Dependencias (Español)**](DEPENDENCIAS.md)

---

## Team

This project was developed by the **Dev.Net** team:

- **Daniel Carrasco** [GitHub](https://github.com/CarrasDev) | [LinkedIn](https://www.linkedin.com/in/danielcarrascoluque/)

- **Marcos Llimera** [GitHub](https://github.com/MarcodigoDev) | [LinkedIn](https://www.linkedin.com/in/marcos-llimera-aparisi-024779298/)

- **Raul Estevez** [GitHub](https://github.com/RaulEstevezA) | [LinkedIn](https://www.linkedin.com/in/raulesteveza/)

---

## License

This project is licensed under the MIT License.
See the [LICENSE](LICENSE) file for details.

---

## Notes

- To use all features, you must set up:
  - A **SQL Server** database for the .NET application
  - An **Odoo** instance (ERP) for the integration features
- Credentials are not stored in the repository:
  - `.NET`: copy `appsettings.json.example` → `appsettings.json` and update the SQL Server connection string
  - `Python/Odoo`: copy `.env.example` → `.env` and update the Odoo connection settings
- See the documentation files for full setup instructions and usage.