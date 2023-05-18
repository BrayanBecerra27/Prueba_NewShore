# Prueba_NewShore
Prueba Backend NewShore

La siguiente Api se desarrollo en .Net6 con C#, por lo cuál se requiere Visual Studio 2022 para su ejecución. El fuente deñ proyecto se encuentra en la Rama Master

# Estructura de Api
La Api cuenta con 3 capas:
  • WEB
  • Business
  • DataAccess
y un proyecto Test en donde se desarrollaron algunas pruebas unitarias.

# Base de datos
Se utilizó una base de datos de azure sql server

# Logs
Para el manejo de logs, se utilizó un BucketS3 de AWS

# CI/CD
Se implementó CI/CD utilizando Git Action, alojando la imagen de docker en un ECR (Elastic Container Registry) de AWS y con el Servicio App-Runner de AWS para el despliegue de la API.

# Modo de uso de la API
Al contrato estipulado en la prueba se le agregaron dos parametros de entrada RouteType, el cuál tiene como fin consultar alguno de los 3 servicios suministrados en la documentacion. utilizar 1 para ruta única, 2 para múltiples y 3 para múltiples con retorno. El otro parámetro adicional es el de Scale, es opcional y limita las escalas ente los vuelos que se desea consultar (si no se envía un parámetro por defecto se utilizan 4 escalas como máximo, condición que se asignó para no realizar largas iteraciones).