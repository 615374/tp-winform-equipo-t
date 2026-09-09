# TPWinForm_EquipoT

Proyecto de escritorio desarrollado en **C# (.NET / WinForms)** para la materia **Programación III** de la Universidad Tecnológica Nacional (UTN). 

Esta aplicación permite la gestión integral de un catálogo de artículos comercializable, incluyendo la administración de productos, marcas, categorías e imágenes asociadas, conectada a una base de datos relacional SQL Server.

---

## Funcionalidades Principales

- **Listado y Navegación de Artículos:** Visualización general con soporte para múltiples imágenes por producto.
- **Búsqueda y Filtros:** Búsqueda rápida por texto y filtros avanzados por diferentes criterios (categoría, marca, rango de precio).
- **Gestión de Artículos (CRUD):**
  - Alta de nuevos artículos con validaciones.
  - Modificación de datos existentes e imágenes.
  - Bajas lógicas / físicas de productos.
  - Vista detallada e individualizada de cada artículo.
- **Administración de Auxiliares:** Gestión dinámica de **Marcas** y **Categorías**.

---

## Tecnologías Utilizadas

- **Lenguaje:** C# (.NET Framework / .NET Core WinForms)
- **Base de Datos:** Microsoft SQL Server (`CATALOGO_P3_DB`)
- **Acceso a Datos:** ADO.NET (`SqlConnection`, `SqlCommand`, `SqlDataReader`)
- **Arquitectura:** Estructura en capas (Presentación / WinForms, Negocio / Business, Dominio / Entities, Acceso a Datos / DB)

---

## Integrantes del Equipo

- **Gisela Grisel Lanzillotta**
- **Sol Dánae Lezcano**
- **Henry José Vazquez Velasquez**

---

## Configuración e Instalación

1. **Clonar el repositorio:**
   ```bash
   git clone [https://github.com/615374/tp-winform-equipo-t.git](https://github.com/615374/tp-winform-equipo-t.git)
