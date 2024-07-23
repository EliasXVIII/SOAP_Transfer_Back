using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

public class FileService : IFileService
{
    private readonly string encryptionKey = "12345678901234567890123456789012"; // Debe ser de 32 bytes para AES-256

    public async Task<string> UploadFileAsync(byte[] fileBytes, string fileName)
    {
        try
        {
            var encryptedBytes = EncryptFile(fileBytes);
            var uploadsFolder = "C:\\Uploads";
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

    private byte[] EncryptFile(byte[] fileBytes)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey);
            aesAlg.GenerateIV();
            var iv = aesAlg.IV;

            using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
            using (var msEncrypt = new MemoryStream())
            {
                msEncrypt.Write(iv, 0, iv.Length);
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var bwEncrypt = new BinaryWriter(csEncrypt))
                {
                    bwEncrypt.Write(fileBytes);
                }

                return msEncrypt.ToArray();
            }
        }
    }

    public async Task<byte[]> DownloadFileAsync(string fileName)
{
    var filePath = Path.Combine("C:\\Uploads", fileName);
    if (File.Exists(filePath))
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
        var cipherText = new byte[encryptedBytes.Length - iv.Length];

        Array.Copy(encryptedBytes, iv, iv.Length);
        Array.Copy(encryptedBytes, iv.Length, cipherText, 0, cipherText.Length);

        aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey);
        aesAlg.IV = iv;

        using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
        using (var msDecrypt = new MemoryStream(cipherText))
        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        using (var brDecrypt = new BinaryReader(csDecrypt))
        {
            return brDecrypt.ReadBytes(cipherText.Length);
        }
    }
}

}