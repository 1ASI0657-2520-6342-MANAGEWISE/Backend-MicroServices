# Backend-MicroServices
El proyecto ha sido reorganizado bajo una arquitectura de microservicios, donde cada módulo del sistema opera como un servicio independiente desarrollado en C# .NET. La solución incluye un ApiGateway encargado de centralizar las rutas y manejar el enrutamiento hacia los microservicios.

La carpeta Services contiene los microservicios IAM, Payments, Profiles y Tasks, cada uno con su propio ciclo de desarrollo, endpoints y lógica de negocio. Las capturas de commits recientes evidencian la creación y actualización de estos servicios conforme al avance del Sprint.

Próximamente, se integrará un despliegue completo mediante Docker y Kubernetes para orquestar los contenedores y garantizar una infraestructura más escalable y preparada para producción.
