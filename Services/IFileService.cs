
using System.ServiceModel;
using System.Threading.Tasks;

//En esta interfaz defino ambos servicios que tengo en FileService 
//Estan definidas dentro de ServiceContract ya que asi se utiliza para realizar llamadas a estos servicios de manera asíncrona.
[ServiceContract]
public interface IFileService
{
    [OperationContract]
    Task<string> UploadFileAsync(byte[] fileBytes, string fileName);

    [OperationContract]
    Task<byte[]> DownloadFileAsync(string fileName);
}
