using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

public class FileService : IFileService
{
    private readonly string encryptionKey = "12345678901234567890123456789012"; // Debe ser de 32 bytes para AES-256
    //! el es cifrado AES-256.

    public async Task<string> UploadFileAsync(byte[] fileBytes, string fileName) //Recibe un archivo y su nombre
    {
        try
        {
            var encryptedBytes = EncryptFile(fileBytes); //lo encripta utilizando Encryptfile
            var uploadsFolder = "C:\\Uploads"; //Lo almacena en esta raiz
            var filePath = Path.Combine(uploadsFolder, fileName);

            // Crear el directorio si no existe
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            await File.WriteAllBytesAsync(filePath, encryptedBytes);
            return "File uploaded successfully";
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }
//Lógica del encriptado 
    private byte[] EncryptFile(byte[] fileBytes) //Cifra los bytes del archivo utilizando AES-256
    {
        using (Aes aesAlg = Aes.Create()) //Creo la instancia de AES y devuelve una nueva instancia. (sintaxis)
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey); //La clave la convierto en un arreglo de bytes usando UTF8
            aesAlg.GenerateIV(); //Genera un vector de inicializacion aleatorio para cada operación de cifrado. //!vector de inicialización es aleatorio y se combina con la clave AES-256
            var iv = aesAlg.IV; //almaceno el resultado en ==>//! iv

            using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)) 
            using (var msEncrypt = new MemoryStream()) //Se crea un MemoryStream para almacenar los datos cifrados en memoria
            {
                msEncrypt.Write(iv, 0, iv.Length); //Esta parte se encarga de realizar el cifrado
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) //encryptor escribe en msEncrypt que es la memoria del programa ya la parte encriptada
                using (var bwEncrypt = new BinaryWriter(csEncrypt)) //con BinaryWriter escribe los archivos y con csEncrypt los cifra antes de guardarlos.
                {
                    bwEncrypt.Write(fileBytes);
                }

                return msEncrypt.ToArray(); //retorna el archivo encriptado
            }
        }
    }

    public async Task<byte[]> DownloadFileAsync(string fileName) //Descifra los archivos
{
    var filePath = Path.Combine("C:\\Uploads", fileName); //busca por el nombre si el archivo esta en la carpeta. 
    if (File.Exists(filePath)) //si no esta devuelve el mensaje de archivo no encontrado.
    {
        var encryptedBytes = await File.ReadAllBytesAsync(filePath);
        return DecryptFile(encryptedBytes);
    }
    throw new FileNotFoundException("File not found");
}

private byte[] DecryptFile(byte[] encryptedBytes)
{
    using (Aes aesAlg = Aes.Create())
    {
        var iv = new byte[aesAlg.BlockSize / 8];
        var cipherText = new byte[encryptedBytes.Length - iv.Length]; //para almacenar el texto cifrado, excluyendo el IV 

        Array.Copy(encryptedBytes, iv, iv.Length); //esto almacena la clave y el IV
        Array.Copy(encryptedBytes, iv.Length, cipherText, 0, cipherText.Length); //Este alacena el mensaje encriptado

        aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey); //se decodifica la clave utsando UTF8
        aesAlg.IV = iv; 

        using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)) 
        using (var msDecrypt = new MemoryStream(cipherText)) // contiene los datos cifrados (sin el IV).
        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) //Realiza el descifrado
        using (var brDecrypt = new BinaryReader(csDecrypt)) //lee los bytes descifrados
        {
            return brDecrypt.ReadBytes(cipherText.Length);
        }
    }
}

}