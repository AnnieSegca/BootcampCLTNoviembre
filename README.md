# BootcampCLTNoviembre - Web API en .NET

Este repositorio corresponde a mi práctica y desarrollo de ejercicios dentro del curso de desarrollo de APIs con **ASP.NET Core**. El proyecto parte de un repositorio base proporcionado en clase y contiene las implementaciones, modelos y controladores desarrollados durante el bootcamp.

## Tecnologías y herramientas utilizadas

* **Framework:** .NET 10 / ASP.NET Core Web API
* **Lenguaje:** C#
* **Documentación y pruebas de API:** OpenAPI & Scalar (`Scalar.AspNetCore`)
* **Versionado de API:** `Asp.Versioning.Mvc`
* **Entorno de desarrollo:** Visual Studio

## Estructura del proyecto

* **`Controllers/`**: Controladores con la lógica de los endpoints de la API.
* **`Models/`**: Entidades y modelos de datos del dominio (como `Product`).
* **`Program.cs`**: Configuración principal de servicios, middlewares, versionado y documentación interactiva.
* **`BootcampCLTNoviembre.http`**: Archivo de peticiones HTTP para probar los endpoints directamente desde Visual Studio.

## Cómo ejecutar el proyecto localmente

1. Clona este repositorio en tu máquina local.
2. Abre el archivo de solución (`BootcampCLTNoviembre.slnx` o `.sln`) con **Visual Studio**.
3. Restaura los paquetes NuGet si es necesario y presiona **F5** (o el botón de ejecutar) para levantar el servidor local.
4. Accede a la interfaz de **Scalar** en tu navegador para explorar y probar los endpoints disponibles.
