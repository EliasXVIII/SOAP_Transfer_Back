// IFileService.cs
using System.ServiceModel;
using System.Threading.Tasks;

[ServiceContract]
public interface IFileService
{
    [OperationContract]
    Task<string> UploadFileAsync(byte[] fileBytes, string fileName);

    [OperationContract]
    Task<byte[]> DownloadFileAsync(string fileName);
}
