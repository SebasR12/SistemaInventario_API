# 📦 SistemaInventario API

![Status](https://img.shields.io/badge/Status-Activo-green?style=flat-square)
![Platform](https://img.shields.io/badge/.NET-6.0-blue?style=flat-square&logo=dotnet)
![DB](https://img.shields.io/badge/SQL_Server-Database-red?style=flat-square&logo=microsoftsqlserver)

API REST minimalista para la gestión de productos y control de stock, desarrollada bajo estándares de arquitectura limpia.

---

## 🛠️ Stack Técnico
* **Backend:** .NET 6 (Web API) con C#.
* **Acceso a Datos:** Entity Framework Core (Code First).
* **Base de Datos:** Microsoft SQL Server.
* **Documentación:** Swagger UI (OpenAPI).

---

## 🏗️ Arquitectura (Simple & Escalable)
El proyecto utiliza una estructura de **3 Capas** para separar responsabilidades:
1. **Controllers:** Gestionan las rutas y peticiones externas.
2. **Services:** Contienen la lógica de negocio (validaciones de stock).
3. **Models & Data:** Definen las entidades y la persistencia en base de datos.

---

## ⚙️ Instalación Rápida
1. Clonar: `git clone https://github.com/SebasR12/SistemaInventario_API.git`
2. Configurar `DefaultConnection` en `appsettings.json`.
3. Migrar DB: `dotnet ef database update`
4. Correr: `dotnet run`

---

## 🌐 Endpoints
* `GET /api/productos` - Listado general.
* `POST /api/productos` - Registro de nuevos items.
* `POST /api/movimientos` - Control de entradas/salidas de stock.

---

## 👨‍💻 Autor
**Sebastián Rodríguez Valverde**
*Ingeniero Informático en formación.*

---
*Licencia MIT*
