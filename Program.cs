
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SoapCore; //permite la creación de servicios SOAP en ASP.NET Core.


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IFileService, FileService>(); 
builder.Services.AddSoapCore(); //Agrega los servicios necesarios para soportar SOAP utilizando SoapCore.
builder.Services.AddCors(options => //agrega CORS para las llamadas desde el front.
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();//Esto es útil para permitir que la API SOAP sea accesible desde cualquier FRONT
    });
});

var app = builder.Build(); //llama al servicio y levanta la aplicación ASP.NET

app.UseRouting(); //enrutados
app.UseCors(); //llama al servicio de cors


//Defino el endpoint SOAP en la ruta "/FileService.asmx"
app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<IFileService>("/FileService.asmx", new SoapEncoderOptions(), SoapSerializer.DataContractSerializer);
});
//En SopaEncoderOptions proporciona opciones para configurar el codificador SOAP.
app.Run();
