//Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSoapCore();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseRouting();
app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<IFileService>("/FileService.asmx", new SoapEncoderOptions(), SoapSerializer.DataContractSerializer);
});

app.Run();
