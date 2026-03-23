# 📦 SistemaInventario API

![Status](https://img.shields.io/badge/Status-En%20Desarrollo-orange?style=for-the-badge)
![Backend](https://img.shields.io/badge/Backend-Node.js-green?style=for-the-badge&logo=nodedotjs)
![License](https://img.shields.io/badge/License-MIT-blue?style=for-the-badge)

**API REST** robusta para la gestión de inventarios, diseñada para administrar productos, categorías y movimientos de stock de manera eficiente, escalable y segura.

---

## 📖 Descripción

**SistemaInventario API** es un motor de backend que centraliza el control de inventario mediante operaciones **CRUD**. Permite una integración fluida con aplicaciones web y móviles, facilitando la trazabilidad de productos y movimientos de almacén en tiempo real.

## 🚀 Características Principales

* ✅ **Gestión de Productos:** Control total de catálogo.
* 🗂️ **Administración de Categorías:** Clasificación organizada.
* 🔄 **Control de Movimientos:** Registro detallado de entradas y salidas.
* 📊 **Stock en Tiempo Real:** Actualización automática de existencias.
* 🏗️ **Arquitectura Modular:** Código limpio y fácil de mantener.

## 🛠️ Stack Tecnológico

| Componente | Tecnología |
| :--- | :--- |
| **Entorno** | Node.js / Express |
| **Base de Datos** | MySQL / PostgreSQL |
| **ORM** | Sequelize / TypeORM |
| **Estilo** | RESTful API |

---

## 🌐 Endpoints de la API

### 📦 Gestión de Productos
| Método | Endpoint | Descripción |
| :--- | :--- | :--- |
| `GET` | `/api/productos` | Listar todos los productos |
| `GET` | `/api/productos/:id` | Obtener detalle por ID |
| `POST` | `/api/productos` | Crear nuevo producto |
| `PUT` | `/api/productos/:id` | Actualizar información |
| `DELETE` | `/api/productos/:id` | Eliminar registro |

### 🗂️ Categorías y 🔄 Movimientos
| Método | Endpoint | Descripción |
| :--- | :--- | :--- |
| `GET` | `/api/categorias` | Listar categorías disponibles |
| `POST` | `/api/movimientos` | Registrar entrada o salida de stock |
| `GET` | `/api/movimientos` | Consultar historial de stock |

---

## 📊 Ejemplo de Uso

### Crear un nuevo producto
**Request:** `POST /api/productos`

```json
{
  "nombre": "Laptop",
  "categoria_id": 1,
  "stock": 15,
  "precio": 850.00
}
