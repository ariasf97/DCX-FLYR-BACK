# DCX FLYR BACK

## Descripción
La evaluación técnica de Flyr es un desafío integral que engloba diversos aspectos del desarrollo de software. La solución se apoya en una arquitectura limpia y robusta de cuatro niveles: presentación, aplicación, dominio e infraestructura. 

## Versión
.NET 8

## Arquitectura Limpia
El proyecto sigue una estructura clara de cuatro capas.
| Capa         | Descripción                                                      |
|--------------|------------------------------------------------------------------|
| Presentación | Controladores de API que manejan las solicitudes HTTP            |
| Aplicación   | Lógica de aplicación y manejo de solicitudes, incluidas las reglas de negocio |
| Dominio      | Modelos de datos y reglas de negocio                              |
| Infraestructura        | Acceso a datos utilizando Entity Framework                        |

## Endpoints
- `POST /api/flight/GetFlightsByType-airports`: Obtiene los vuelos de Oneway.
- `POST /api/flight/GetRoundtripFlights-airports`: Obtiene los vuelos de Roundtrip.
