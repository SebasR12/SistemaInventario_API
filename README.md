# 📦 SistemaInventario API (.NET)

![Status](https://img.shields.io/badge/Status-Completado-brightgreen?style=for-the-badge)
![Backend](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Database](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)

**API REST** robusta para la gestión de inventarios, productos y movimientos de stock en tiempo real.

---

## 🛠️ Stack Tecnológico

| Capa | Tecnología |
| :--- | :--- |
| **Framework** | .NET 6.0+ (C#) |
| **Base de Datos** | Microsoft SQL Server |
| **ORM** | Entity Framework Core |

## 🚀 Funcionalidades Clave

* ✅ **CRUD Completo:** Productos y Categorías.
* 🔄 **Stock:** Registro de entradas y salidas automatizado.
* 🏗️ **Arquitectura:** Controladores y Servicios (Limpia y escalable).
* 🧪 **Documentación:** Swagger UI integrado.

---

## ⚙️ Instalación Rápida

1. **Clonar:** `git clone https://github.com/SebasR12/SistemaInventario_API.git`
2. **Base de Datos:** Configura tu conexión en `appsettings.json`.
3. **Migrar:** Ejecuta `Update-Database` en la consola de NuGet.
4. **Correr:** Presiona `F5` o ejecuta `dotnet run`.

---

## 🌐 Endpoints Principales

| Método | Endpoint | Acción |
| :--- | :--- | :--- |
| `GET` | `/api/productos` | Listar inventario |
| `POST` | `/api/productos` | Agregar producto |
| `POST` | `/api/movimientos` | Registrar entrada/salida |
| `GET` | `/api/categorias` | Listar categorías |

---

## 📊 Ejemplo de Respuesta

```json
{
  "id": 1,
  "nombre": "Monitor 24\"",
  "stockActual": 10,
  "precio": 150.00
}
