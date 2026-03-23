# 📦 SistemaInventario API (.NET)

![Status](https://img.shields.io/badge/Status-Activo-brightgreen?style=for-the-badge)
![Backend](https://img.shields.io/badge/.NET%206-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Database](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![Architecture](https://img.shields.io/badge/Architecture-Layered-orange?style=for-the-badge)

**SistemaInventario API** es una solución de backend robusta diseñada para la gestión integral de almacenes. Este proyecto demuestra la implementación de una arquitectura limpia y el manejo de flujos de datos relacionales en un entorno empresarial.

---

## 🎯 Valor de Negocio (Lo que resuelve)
Este sistema centraliza el control de inventarios, permitiendo a las empresas:
* **Reducir errores humanos** mediante validaciones de stock automatizadas.
* **Garantizar la trazabilidad** total de cada producto desde su ingreso hasta su salida.
* **Escalar la operación** gracias a una base de datos normalizada y una API de alto rendimiento.

---

## 🧠 Decisiones Técnicas y Arquitectura
Como **Ingeniero Informático**, he diseñado este sistema priorizando la mantenibilidad:

* **Separación de Responsabilidades:** Utilicé una arquitectura de capas (Controllers -> Services -> Data). Esto facilita las pruebas unitarias y permite cambiar la lógica de negocio sin afectar la base de datos.
* **Integridad de Datos:** Implementé **Entity Framework Core** con migraciones para asegurar que el esquema de la base de datos sea consistente en cualquier entorno.
* **Inyección de Dependencias:** El sistema utiliza el contenedor nativo de .NET para desacoplar los servicios de sus implementaciones, siguiendo los principios **SOLID**.

---

## 🛠️ Stack Tecnológico
* **Framework:** .NET 6.0 (Web API)
* **Lenguaje:** C# 10
* **Persistencia:** Microsoft SQL Server
* **ORM:** Entity Framework Core (Code First)
* **Documentación:** Swagger (OpenAPI)

---

## ⚙️ Instalación Rápida
1. **Clonar repositorio:** `git clone https://github.com/SebasR12/SistemaInventario_API.git`
2. **Conexión:** Configurar `DefaultConnection` en `appsettings.json`.
3. **Base de Datos:** Ejecutar `dotnet ef database update` para generar tablas.
4. **Run:** Ejecutar `dotnet run` y navegar a `/swagger` para pruebas.

---

## 🌐 Endpoints Estratégicos
| Método | Endpoint | Impacto |
| :--- | :--- | :--- |
| `POST` | `/api/movimientos` | Punto crítico: Procesa la lógica de stock y registra el historial. |
| `GET` | `/api/productos` | Consulta optimizada del catálogo global. |
| `PUT` | `/api/productos/{id}` | Actualización controlada de activos de la empresa. |

---

## 📈 Roadmap (Próximas Mejoras)
Para demostrar mi visión de crecimiento del sistema, planeo implementar:
* [ ] **Seguridad:** Autenticación mediante JWT (JSON Web Tokens).
* [ ] **Contenedores:** Dockerización para despliegue en la nube (Azure/AWS).
* [ ] **Frontend:** Dashboard administrativo en React o Angular.

---

## 👨‍💻 Sobre el Autor
**Sebastián Rodríguez Valverde**
*Ingeniero Informático en formación enfocado en Backend & Arquitectura.*

* **GitHub:** [@SebasR12](https://github.com/SebasR12)
* **LinkedIn:** [Tu Perfil Aquí]

---
*Este proyecto está bajo la Licencia MIT.*
