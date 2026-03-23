# 📦 SistemaInventario API

![.NET](https://img.shields.io/badge/.NET-6.0-blue?logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-red?logo=microsoftsqlserver)
![Status](https://img.shields.io/badge/Status-Activo-green)

API REST para gestión de inventarios con control de productos, categorías y movimientos de stock.

---

## 🚀 Funcionalidades

- Gestión completa de productos (CRUD)
- Administración de categorías
- Control de entradas y salidas de stock
- Registro de movimientos en tiempo real
- Documentación interactiva con Swagger

---

## 🛠️ Tecnologías

- .NET 6 (Web API)
- C#
- Entity Framework Core
- SQL Server

---

## 🧠 Arquitectura

El proyecto sigue una estructura basada en capas para mejorar la mantenibilidad:

- **Controllers** → Manejo de endpoints  
- **Services** → Lógica de negocio  
- **Data / DbContext** → Acceso a datos  
- **Models** → Entidades del sistema  

---

## ⚙️ Instalación

1. Clonar repositorio  
   `git clone https://github.com/SebasR12/SistemaInventario_API.git`

2. Configurar base de datos en `appsettings.json`

3. Ejecutar migraciones  
   `Update-Database`

4. Ejecutar proyecto  
   `dotnet run`

---

## 🌐 Endpoints

- `GET /api/productos` → Listar productos  
- `POST /api/productos` → Crear producto  
- `POST /api/movimientos` → Registrar movimiento  
- `GET /api/categorias` → Listar categorías  

---

## 📊 Ejemplo de Respuesta

```json
{
  "id": 1,
  "nombre": "Monitor Gamer 24\"",
  "stockActual": 25,
  "precio": 199.99,
  "fechaCreacion": "2026-03-23"
}
