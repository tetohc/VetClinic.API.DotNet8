# 🐾 API RESTful – Te Chineo Tu Lomito (Backend)

Este repositorio contiene el desarrollo del **backend académico** para la veterinaria *Te Chineo Tu Lomito*, construido con **.NET 8 Web API** y **C# 12**.  
El sistema expone servicios RESTful que son consumidos por el [Frontend MVC](https://github.com/tetohc/VetClinic-Net8.git), permitiendo operaciones CRUD completas y reportes dinámicos.

---

## 🧱 Arquitectura

El proyecto sigue principios de **SOLID**, **inyección de dependencias** y una estructura modular que facilita la escalabilidad y el mantenimiento:

- **Controllers**: exponen los endpoints RESTful para cada entidad del sistema.
- **Services**: capa DAL que gestiona la lógica de acceso a datos mediante **Entity Framework**.
- **Models**: incluye `Entities`, `Dtos`, `Enums`.
- **Validators**: validación de entrada con **FluentValidation**.
- **Mappers**: transformación entre entidades y DTOs.
- **Responses**: estructura estándar para respuestas HTTP.
- **Settings**: configuración de dependencias y servicios.
- **Swagger**: documentación interactiva de la API.

---

## 📌 Controladores disponibles

| Controlador              | Descripción                                                                 |
|--------------------------|------------------------------------------------------------------------------|
| **EmployeesController**  | Gestión de empleados.                                                       |
| **PetsController**       | Gestión de mascotas asociadas a clientes.                                   |
| **CustomersController**  | Registro y administración de clientes.                                      |
| **PetProceduresController** | Registro de procedimientos veterinarios vinculados a cliente y mascota. |
| **ReportsController**    | Reportes dinámicos (ej. vacunación anual próxima semana).                   |
| **ProvincesController**  | Consulta de provincias para formularios.                                    |
| **CantonsController**    | Consulta de cantones para formularios.                                      |
| **DistrictsController**  | Consulta de distritos para formularios.                                     |

---

## 🎯 Objetivos del proyecto

- Exponer servicios RESTful independientes para ser consumidos por el frontend MVC.
- Implementar validaciones robustas y respuestas estandarizadas.
- Persistir datos en una base de datos relacional normalizada (mínimo 2FN).
- Aplicar buenas prácticas de arquitectura limpia y modularidad.

---

## 🔗 Proyecto relacionado

Este backend es consumido por el siguiente proyecto frontend:

👉 [Frontend MVC – Te Chineo Tu Lomito](https://github.com/tetohc/VetClinic-Net8.git)

---

## 🚀 Tecnologías utilizadas

| 🛠️ Categoría        | ⚡ Tecnologías |
|----------------------|----------------|
| **Lenguaje**         | C# 12 |
| **Framework**        | .NET 8 |
| **ORM**              | Entity Framework |
| **Validación**       | FluentValidation |
| **Documentación**    | Swagger UI |
| **Principios**       | SOLID, Inyección de dependencias |
| **Persistencia**     | Base de datos relacional SQL Server |

---


