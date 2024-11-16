# Biokudi: Aplicativo Web para la Promoci�n de Lugares Ecotur�sticos en Cundinamarca

**Biokudi** es una plataforma que busca facilitar el acceso a informaci�n actualizada sobre destinos ecotur�sticos en Cundinamarca, Colombia. A trav�s de una interfaz intuitiva y un sistema de mapa interactivo, los usuarios pueden explorar lugares de inter�s ecol�gico, compartir rese�as, calificar experiencias y, en el futuro, acceder a una variedad de funcionalidades adicionales como noticias, listas personalizadas y planes de pago. Este repositorio contiene el c�digo y la documentaci�n t�cnica del proyecto.

### Caracter�sticas Principales

- **Exploraci�n de destinos ecotur�sticos:** Mapa interactivo que permite a los usuarios visualizar y explorar distintos lugares.
- **Sistema de calificaci�n y rese�as:** Los usuarios pueden dejar comentarios y calificar los lugares, fomentando una comunidad activa y confiable.
- **Autenticaci�n y seguridad avanzadas:** Implementaci�n de JWT, reCaptcha, encriptaci�n RSA y medidas adicionales para proteger los datos del usuario.
- **Escalabilidad futura:** Planeaci�n para integrar inteligencia artificial, listas personalizadas, noticias y modelos de pago.


## Arquitectura
Biokudi emplea **Onion Architecture** para una separaci�n clara de responsabilidades:
1. **Dominio**: Contiene la l�gica de negocio central y las entidades principales.
2. **Aplicaci�n**: Gestiona las operaciones que involucran reglas de negocio aplicadas a las entidades.
3. **Infraestructura**: Implementa acceso a datos y servicios externos.
4. **Interfaz de Usuario**: Manejada a trav�s de controladores para la interacci�n con el usuario.

Este enfoque modular facilita la escalabilidad y la adaptabilidad del sistema.

## Tecnolog�a y Herramientas

- **Backend**: .NET Core 8 con PostgreSQL como base de datos.
- **Frontend**: Mapas interactivos implementados con Leaflet.
- **Seguridad**: Autenticaci�n JWT, pol�ticas CORS, protecci�n contra SQL Injection y XSS, encriptaci�n RSA y manejo de captcha.
- **Infraestructura CI/CD**: Despliegues automatizados con GitHub Actions.
- **An�lisis y Monitoreo**: Codacy para an�lisis de calidad del c�digo y BetterStack para monitorizaci�n y m�tricas de rendimiento.

## Seguridad y Buenas Pr�cticas

- **Validaciones**: Utilizaci�n de FluentValidation para asegurar la entrada de datos.
- **Control de Acceso**: Gesti�n de sesiones con JWT y protecci�n CORS espec�fica para `biokudi.site`.
- **Rate Limiting**: Limitaci�n de solicitudes para prevenir ataques DoS.
- **Manejo de Excepciones**: Excepciones personalizadas para manejo de errores de negocio y base de datos.
  
## Rendimiento

- **Compresi�n de Respuestas**: Reducci�n de tama�o de datos transmitidos para mejorar tiempos de respuesta.
- **Caching**: Cacheo en memoria y output caching para optimizar cargas y tiempos de respuesta.
- **Minimizaci�n de Payload**: Uso de System.Text.Json para serializaci�n, mejorando la eficiencia en transmisi�n de datos.

## Herramientas de Desarrollo

- **IDE**: Visual Studio 2022
- **Base de Datos**: PostgreSQL gestionada con pgAdmin.

## Convenciones de Commit y Ramas

- **Ramas**:
  - `alpha`: Rama individual de cada desarrollador.
  - `beta`: Rama de pruebas.
  - `main`: Rama de producci�n.
- **Convenci�n de Commit**: Prefijos en ingl�s seguidos de una breve descripci�n, por ejemplo, `CF: File configuration`, con descripci�n detallada en espa�ol.

## Servicios Utilizados en la Infraestructura

- **Hostinger**: Gesti�n de dominio (`biokudi.site`).
- **Cloudflare**: Seguridad a nivel DNS.
- **Vercel**: Alojamiento del frontend.
- **Somee** y **Heliohost**: Alojamiento del backend y base de datos, respectivamente.
- **ImgBB**: Almacenamiento de im�genes.
- **BetterStack**: Monitorizaci�n en tiempo real.
- **Codacy**: An�lisis est�tico de c�digo.
- **Leaflet**: Biblioteca de mapas interactivos.

## M�s Informaci�n

Para obtener m�s detalles sobre la arquitectura, configuraci�n, y otros aspectos t�cnicos del proyecto, revisa la [Wiki del repositorio](https://github.com/RFGRONA/BioKudi-Backend/wiki). Ah� encontrar�s documentaci�n adicional.

---

#### Biokudi es una iniciativa en constante evoluci�n, orientada a ofrecer a los usuarios la mejor experiencia en la exploraci�n de destinos ecotur�sticos. Nos enfocamos en mantener altos est�ndares de calidad, seguridad y rendimiento en cada etapa de su desarrollo.
