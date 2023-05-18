# Prueba_NewShore
Prueba Backend NewShore

La siguiente Api se desarrollo en .Net6 con C#, por lo cuál se requiere Visual Studio 2022 para su ejecución. El fuente de proyecto se encuentra en la Rama Master.
Para ejecutar la Api en local, utilizar el proyecto NEWSHPRE_AIR_WEB como proyecto de inicio.

# Estructura de Api
La Api cuenta con 3 capas:
  • WEB
  • Business
  • DataAccess
y un proyecto Test en donde se desarrollaron algunas pruebas unitarias.

# Base de datos
Se utilizó una base de datos de azure sql server en el appsettingjson se encuentra la cadena de cónexión por si se requiere visualizar la información que allí se está almacenando.

# Logs
Para el manejo de logs, se utilizó un BucketS3 de AWS utilizando el nuget Serilog.

# CI/CD
Se implementó CI/CD utilizando Git Action, el alojamiento de la imagen de docker se realiza un ECR (Elastic Container Registry) de AWS y con el Servicio App-Runner de AWS se ejecuta el despliegue cada vez que se cambia la imagen del API en el ECR.

# Modo de uso de la API
Al contrato estipulado en la prueba se le agregaron dos parametros de entrada: 
  • RouteType: el cuál tiene como fin consultar alguno de los 3 servicios suministrados en la documentacion. utilizar:
   1 para ruta única (Se retorna vuelos directos).
   2 para múltiples (Se retorna vuelos con escalas).
   3 para múltiples con retorno (Se retorna vuelos ida y vuelta con escalas).
  • Scale: es opcional y limita las escalas entre los vuelos que se desea consultar (si no se envía un parámetro por defecto se asigna 4 escalas como máximo, condición que se asignó para no realizar largas iteraciones).

  # Servicios
  Despliegue: https://2si3erf8p2.us-east-1.awsapprunner.com/swagger/index.html