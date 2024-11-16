# Biokudi: Aplicativo Web para la Promoción de Lugares Ecoturísticos en Cundinamarca

**Biokudi** es una plataforma que busca facilitar el acceso a información actualizada sobre destinos ecoturísticos en Cundinamarca, Colombia. A través de una interfaz intuitiva y un sistema de mapa interactivo, los usuarios pueden explorar lugares de interés ecológico, compartir reseñas, calificar experiencias y, en el futuro, acceder a una variedad de funcionalidades adicionales como noticias, listas personalizadas y planes de pago. Este repositorio contiene el código y la documentación técnica del proyecto.

### Características Principales

- **Exploración de destinos ecoturísticos:** Mapa interactivo que permite a los usuarios visualizar y explorar distintos lugares.
- **Sistema de calificación y reseñas:** Los usuarios pueden dejar comentarios y calificar los lugares, fomentando una comunidad activa y confiable.
- **Autenticación y seguridad avanzadas:** Implementación de JWT, reCaptcha, encriptación RSA y medidas adicionales para proteger los datos del usuario.
- **Escalabilidad futura:** Planeación para integrar inteligencia artificial, listas personalizadas, noticias y modelos de pago.


## Arquitectura
Biokudi emplea **Onion Architecture** para una separación clara de responsabilidades:
1. **Dominio**: Contiene la lógica de negocio central y las entidades principales.
2. **Aplicación**: Gestiona las operaciones que involucran reglas de negocio aplicadas a las entidades.
3. **Infraestructura**: Implementa acceso a datos y servicios externos.
4. **Interfaz de Usuario**: Manejada a través de controladores para la interacción con el usuario.

Este enfoque modular facilita la escalabilidad y la adaptabilidad del sistema.

## Tecnología y Herramientas

- **Backend**: .NET Core 8 con PostgreSQL como base de datos.
- **Frontend**: Mapas interactivos implementados con Leaflet.
- **Seguridad**: Autenticación JWT, políticas CORS, protección contra SQL Injection y XSS, encriptación RSA y manejo de captcha.
- **Infraestructura CI/CD**: Despliegues automatizados con GitHub Actions.
- **Análisis y Monitoreo**: Codacy para análisis de calidad del código y BetterStack para monitorización y métricas de rendimiento.

## Seguridad y Buenas Prácticas

- **Validaciones**: Utilización de FluentValidation para asegurar la entrada de datos.
- **Control de Acceso**: Gestión de sesiones con JWT y protección CORS específica para `biokudi.site`.
- **Rate Limiting**: Limitación de solicitudes para prevenir ataques DoS.
- **Manejo de Excepciones**: Excepciones personalizadas para manejo de errores de negocio y base de datos.
  
## Rendimiento

- **Compresión de Respuestas**: Reducción de tamaño de datos transmitidos para mejorar tiempos de respuesta.
- **Caching**: Cacheo en memoria y output caching para optimizar cargas y tiempos de respuesta.
- **Minimización de Payload**: Uso de System.Text.Json para serialización, mejorando la eficiencia en transmisión de datos.

## Herramientas de Desarrollo

- **IDE**: Visual Studio 2022
- **Base de Datos**: PostgreSQL gestionada con pgAdmin.

## Convenciones de Commit y Ramas

- **Ramas**:
  - `alpha`: Rama individual de cada desarrollador.
  - `beta`: Rama de pruebas.
  - `main`: Rama de producción.
- **Convención de Commit**: Prefijos en inglés seguidos de una breve descripción, por ejemplo, `CF: File configuration`, con descripción detallada en español.

## Servicios Utilizados en la Infraestructura

- **Hostinger**: Gestión de dominio (`biokudi.site`).
- **Cloudflare**: Seguridad a nivel DNS.
- **Vercel**: Alojamiento del frontend.
- **Somee** y **Heliohost**: Alojamiento del backend y base de datos, respectivamente.
- **ImgBB**: Almacenamiento de imágenes.
- **BetterStack**: Monitorización en tiempo real.
- **Codacy**: Análisis estático de código.
- **Leaflet**: Biblioteca de mapas interactivos.

## Más Información

Para obtener más detalles sobre la arquitectura, configuración, y otros aspectos técnicos del proyecto, revisa la [Wiki del repositorio](https://github.com/RFGRONA/BioKudi-Backend/wiki). Ahí encontrarás documentación adicional.

---

#### Biokudi es una iniciativa en constante evolución, orientada a ofrecer a los usuarios la mejor experiencia en la exploración de destinos ecoturísticos. Nos enfocamos en mantener altos estándares de calidad, seguridad y rendimiento en cada etapa de su desarrollo.
