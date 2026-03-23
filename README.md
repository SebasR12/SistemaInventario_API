# 📦 SistemaInventario API (.NET)

![Status](https://img.shields.io/badge/Status-Completado-brightgreen?style=for-the-badge)
![Backend](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Database](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)

**API REST** profesional desarrollada en **.NET** para la gestión integral de inventarios. Este sistema permite administrar productos, categorías y flujos de stock de manera eficiente, segura y altamente escalable con **SQL Server**.

---

## 📖 Descripción General

**SistemaInventario API** es una solución de backend empresarial. Proporciona una interfaz robusta para operaciones **CRUD**, garantizando la integridad referencial de los datos y facilitando la trazabilidad de cada movimiento de mercancía mediante procedimientos optimizados.

## 🚀 Funcionalidades Principales

* ✅ **Gestión de Productos:** Control total de catálogo, precios y metadatos.
* 🗂️ **Administración de Categorías:** Clasificación organizada y relacional.
* 🔄 **Trazabilidad de Movimientos:** Registro histórico de entradas y salidas de almacén.
* 📊 **Stock en Tiempo Real:** Actualización automática de existencias con alta precisión.
* 🏗️ **Arquitectura Limpia:** Implementación basada en controladores y servicios en .NET.

## 🛠️ Stack Tecnológico

| Capa | Tecnología |
| :--- | :--- |
| **Framework** | .NET (Core / Web API) |
| **Lenguaje** | C# |
| **Base de Datos** | Microsoft SQL Server |
| **ORM** | Entity Framework Core / Dapper |
| **Arquitectura** | RESTful API |

---

## 🌐 Endpoints de la API

### 📦 Productos
| Método | Endpoint | Acción |
| :--- | :--- | :--- |
| `GET` | `/api/productos` | Obtener catálogo completo |
| `GET` | `/api/productos/{id}` | Consultar un producto específico |
| `POST` | `/api/productos` | Registrar nuevo producto |
| `PUT` | `/api/productos/{id}` | Actualizar datos técnicos o precios |
| `DELETE` | `/api/productos/{id}` | Eliminar del sistema |

### 🗂️ Categorías y 🔄 Stock
| Método | Endpoint | Acción |
| :--- | :--- | :--- |
| `GET` | `/api/categorias` | Listado de categorías activas |
| `POST` | `/api/movimientos` | Registrar flujo de stock (Entrada/Salida) |
| `GET` | `/api/movimientos` | Auditoría de movimientos históricos |

---

## 📊 Ejemplo de Respuesta (JSON)

La API devuelve objetos tipados según los modelos de C#:

```json
{
  "id": 1,
  "nombre": "Monitor Gamer 24\"",
  "categoriaId": 2,
  "stockActual": 25,
  "precio": 199.99,
  "fechaCreacion": "2026-03-23T15:42:00"
}
