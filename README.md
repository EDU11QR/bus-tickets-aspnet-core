# 🚌 Bus Ticket System

Sistema web desarrollado con **ASP.NET Core MVC** para la gestión y reserva de pasajes de buses.  
El proyecto permite administrar rutas, horarios, buses y reservas mediante una arquitectura organizada y escalable utilizando Entity Framework Core y SQL Server.

---

## 🚀 Características

- ✅ Gestión de buses
- ✅ Administración de rutas
- ✅ Programación de horarios
- ✅ Sistema de reservas
- ✅ Gestión de ubicaciones
- ✅ Autenticación de usuarios
- ✅ Reportes del sistema
- ✅ CRUD completo con Entity Framework Core
- ✅ Arquitectura MVC organizada
- ✅ Interfaz responsive con Bootstrap

---

## 🛠️ Tecnologías utilizadas

### Backend
- ASP.NET Core MVC (.NET 8)
- C#
- Entity Framework Core
- SQL Server
- ASP.NET Identity

### Frontend
- Razor Views
- HTML5
- CSS3
- Bootstrap
- JavaScript

---

## 📂 Estructura del proyecto

```bash
BusTicketSystem/
│
├── Controllers/      # Controladores MVC
├── Models/           # Modelos de datos
├── Views/            # Vistas Razor
├── Repositories/     # Acceso a datos
├── Data/             # Contexto de base de datos
├── ViewModels/       # Modelos para vistas
├── wwwroot/          # Archivos estáticos
└── Database/         # Scripts y recursos SQL
```

---

## ⚙️ Funcionalidades principales

### 🚌 Gestión de buses
- Registrar buses
- Editar información
- Eliminar buses
- Visualizar listado

### 🛣️ Gestión de rutas
- Crear rutas de viaje
- Actualizar destinos
- Gestionar trayectos

### 🕒 Gestión de horarios
- Programar salidas
- Administrar horarios disponibles
- Relación entre rutas y buses

### 🎫 Reservas
- Registro de reservas
- Control de pasajeros
- Visualización de reservas realizadas

### 📊 Reportes
- Reportes administrativos
- Consulta de información del sistema

### 🔐 Autenticación
- Login de usuarios
- Control de acceso mediante Identity

---

## 🧩 Arquitectura utilizada

El proyecto sigue el patrón de arquitectura **MVC (Model - View - Controller)**:

- **Models** → Representación de entidades y datos
- **Views** → Interfaz visual del sistema
- **Controllers** → Lógica de negocio y flujo de la aplicación
- **Repositories** → Acceso y manipulación de datos

---

## 🗄️ Base de datos

El sistema utiliza **SQL Server** junto con **Entity Framework Core** para la persistencia de datos.

---

## ▶️ Cómo ejecutar el proyecto

### 1️⃣ Clonar el repositorio

```bash
git clone https://github.com/EDU11QR/bus-tickets-aspnet-core.git
```

### 2️⃣ Abrir el proyecto

Abrir la solución en:

```bash
Visual Studio 2022
```

### 3️⃣ Configurar la cadena de conexión

Editar el archivo:

```bash
appsettings.json
```

Configurar:

```json
"ConnectionStrings": {
  "cn": "TU_CADENA_DE_CONEXION"
}
```

### 4️⃣ Ejecutar migraciones

```bash
Update-Database
```

### 5️⃣ Ejecutar el proyecto

```bash
Ctrl + F5
```

---

## 📸 Capturas del sistema

> Puedes agregar aquí imágenes del proyecto para mejorar la presentación visual de tu repositorio.

---

## 📈 Objetivo del proyecto

Este proyecto fue desarrollado con fines de:

- Aprendizaje de ASP.NET Core MVC
- Aplicación de arquitectura por capas
- Gestión de sistemas de transporte
- Práctica de Entity Framework Core
- Fortalecimiento de habilidades Full Stack

---

## 👨‍💻 Autor

### DevEdu

---

## ⭐ Repositorio

Si te gusta este proyecto puedes dejar una ⭐ en el repositorio.
