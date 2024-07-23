# Servicio SOAP para implementar WS-Security

![CSHARP](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Visual](https://img.shields.io/badge/Visual_Studio_Code-0078D4?style=for-the-badge&logo=visual%20studio%20code&logoColor=white)
![Windows11](https://img.shields.io/badge/Windows_11-0078d4?style=for-the-badge&logo=windows-11&logoColor=white)

## Nombre del Software: Servicio de Archivo Seguro con SOAP
Descripción General: Este software proporciona un servicio para la carga y descarga de archivos con encriptación y desencriptación utilizando el estándar SOAP. Los archivos se almacenan de manera segura en el servidor utilizando el cifrado AES-256. La aplicación permite a los usuarios subir y bajar archivos de manera segura, asegurando que los datos en tránsito y en reposo estén protegidos.

<p align="center">
  <img src="https://github.com/EliasXVIII/SOAP_Transfer_Back/blob/main/Documento_Encrypt256.PNG" alt="Encrypt" style="margin: auto; width: 800px;">
</p>

## Arquitectura del Programa
FileService.cs:
Implementa la lógica para cargar y descargar archivos.
Utiliza cifrado AES-256 para proteger los archivos antes de almacenarlos y para descifrarlos cuando se descargan.
IFileService.cs:
Define las operaciones disponibles en el servicio SOAP, como UploadFileAsync y DownloadFileAsync.
Program.cs:
Configura y arranca la aplicación, incluyendo la configuración del servicio SOAP y la política de CORS para la gestión del Frontend
FileEncryptionService.csproj:
Archivo de proyecto que define las dependencias y configuraciones del proyecto .NET.

<p align="center">
  <img src="https://github.com/EliasXVIII/SOAP_Transfer_Back/blob/main/Server_Run.PNG" alt="RunServer" style="margin: auto; width: 800px;">
</p> 

## Guía Básica de Uso
Subir un Archivo:
Llama al método UploadFileAsync proporcionando un archivo en formato byte y un nombre de archivo. El archivo será cifrado y almacenado en el servidor.
Descargar un Archivo:
Llama al método DownloadFileAsync proporcionando el nombre del archivo. El archivo será descifrado y devuelto al cliente almacenandolo en la carpeta local Download o Descargas.


## Tecnologías Utilizadas
.NET 8.0: Framework de desarrollo para la construcción de aplicaciones web y servicios.
ASP.NET Core: Plataforma para construir aplicaciones web y servicios en la nube.
SoapCore: Biblioteca para crear y consumir servicios SOAP en ASP.NET Core.
AES-256: Algoritmo de cifrado utilizado para proteger los archivos.


## Descripción de las Tecnologías
.NET 8.0: La última versión estable de .NET proporciona mejoras en el rendimiento y nuevas características para el desarrollo de aplicaciones.
ASP.NET Core: Permite la creación de servicios web robustos y escalables. Ofrece soporte para la construcción de aplicaciones SOAP.
SoapCore: implementa y consume servicios SOAP en aplicaciones ASP.NET Core. Es compatible con las especificaciones de SOAP y permite el uso de la serialización de datos.
AES-256: Estándar de cifrado seguro que protege los datos con una clave de 256 bits, asegurando que los archivos estén protegidos contra accesos no autorizados.


## Tecnologías Descartadas
WCF (Windows Communication Foundation): Aunque WCF es una opción para servicios SOAP en aplicaciones .NET, se ha optado por SoapCore para una integración más sencilla en aplicaciones ASP.NET Core.
Otros algoritmos de cifrado (como DES o 3DES): Se descartaron debido a su menor nivel de seguridad en comparación con AES-256, que es el estándar recomendado para cifrado de datos.


## Programas Necesarios y Recomendados
Programas Necesarios:
Visual Studio o Visual Studio Code: Para el desarrollo del proyecto .NET.
.NET SDK 8.0: Necesario para compilar y ejecutar la aplicación.
DESPLIEGUE: El despliegue en este caso se realiza en localhost.
(RECOMENDADO) Un servidor web: Para desplegar la aplicación, como IIS, Kestrel (integrado en .NET Core), o nginx si se utiliza en un entorno Linux.

## Programas Adicionales Recomendados:
Postman o SoapUI: Para probar las llamadas al servicio SOAP.
Herramientas de gestión de secretos: Para manejar de manera segura las claves de cifrado.

## El Servicio WSDL
http://localhost:5177/FileService.asmx


# Incorporación de WS-Security en una Aplicación C# con ASP.NET Core

## 1. Introducción a WS-Security
WS-Security es un estándar para asegurar mensajes SOAP a través de mecanismos como la firma, el cifrado y la autenticación. Implementar WS-Security en tu servicio puede mejorar la protección de los datos que intercambiamos a través de tus servicios web.


## 2. Beneficios de Implementar WS-Security
Autenticación: Verifica la identidad de los usuarios y servicios.
Integridad: Garantiza que los mensajes no han sido alterados durante el tránsito.
Confidencialidad: Cifra los mensajes para proteger los datos sensibles.


## 3. Herramientas Necesarias
SoapCore: La biblioteca para implementar servicios SOAP en ASP.NET Core. Aunque SoapCore no soporta WS-Security de manera nativa, se puede extender para integrarlo.
Azure API Management / AWS API Gateway: Estos servicios en la nube pueden ofrecer funcionalidades adicionales para asegurar y gestionar los servicios web.


## 4. Programas y Software Recomendado
Visual Studio: IDE para desarrollo, depuración y pruebas de tus aplicaciones ASP.NET Core.
SoapUI: Herramienta para probar y validar los mensajes SOAP y verificar la seguridad.
Postman: Para realizar pruebas de API y verificar que la seguridad esté funcionando como se espera.


## 5. Tecnologías y Soluciones Recomendadas
Microsoft.IdentityModel.Tokens: Para manejar tokens de seguridad y validación de firmas en tus aplicaciones C#.
X.509 Certificates: Utiliza certificados X.509 para firmar y cifrar mensajes SOAP.

### Autor: Elías Javier Riquelme

![Linkedin](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white) - [Elias Javier Riquelme](https://www.linkedin.com/in/elias-javier-riquelme-b62655297/)
