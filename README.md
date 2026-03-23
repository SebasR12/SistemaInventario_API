# 📦 SistemaInventario API (.NET)

![Status](https://img.shields.io/badge/Status-Completado-brightgreen?style=for-the-badge) ![Backend](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) ![Database](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white) ![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)

**API REST profesional para la gestión de inventarios**, diseñada para administrar productos, categorías y flujos de stock de forma eficiente y escalable.

---

## 📖 Resumen del Proyecto
Este backend permite un control completo de inventario mediante operaciones **CRUD** y trazabilidad de movimientos en tiempo real. Implementa una arquitectura limpia basada en servicios para garantizar integridad y mantenibilidad.

- ✅ **Productos:** Gestión completa de catálogo y precios  
- 🗂️ **Categorías:** Clasificación organizada y relacional  
- 🔄 **Stock:** Registro automático de entradas y salidas  
- 🧪 **Documentación:** Swagger UI para pruebas interactivas  

---

## 🛠️ Stack Tecnológico

| Componente | Tecnología |
| :--- | :--- |
| **Framework** | .NET 6.0+ (Web API) |
| **Lenguaje** | C# |
| **Base de Datos** | Microsoft SQL Server |
| **ORM** | Entity Framework Core |

---

## ⚙️ Instalación y Configuración

### 1️⃣ Clonar repositorio
git clone https://github.com/SebasR12/SistemaInventario_API.git  
cd SistemaInventario_API  

### 2️⃣ Configurar base de datos
Editar `appsettings.json` y colocar:  
"ConnectionStrings": { "DefaultConnection": "Server=TU_SERVIDOR;Database=SistemaInventarioDB;Trusted_Connection=True;" }

### 3️⃣ Ejecutar migraciones
Update-Database

### 4️⃣ Ejecutar el proyecto
dotnet run

---

## 🌐 Endpoints Principales

| Método | Endpoint | Acción |
| :--- | :--- | :--- |
| GET | /api/productos | Listar todo el inventario |
| POST | /api/productos | Registrar nuevo producto |
| POST | /api/movimientos | Registrar entrada/salida de stock |
| GET | /api/categorias | Listar categorías |

---

## 📊 Ejemplo de Respuesta (JSON)

{
  "id": 1,
  "nombre": "Monitor Gamer 24\"",
  "stockActual": 25,
  "precio": 199.99,
  "fechaCreacion": "2026-03-23"
}

---

## 👨‍💻 Autor

**Sebastián Rodríguez Valverde**  
💻 Ingeniero Informático  
🔗 https://github.com/SebasR12  
🚀 Backend & Automatización  

---

## 📄 Licencia

Este proyecto está bajo la licencia **MIT**.
