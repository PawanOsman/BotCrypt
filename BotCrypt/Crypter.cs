using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BotCrypt;

public class Crypter
{
    public static string Sha256Hash(string rawData)
    {
        // Create a SHA256   
        using (var sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array  
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            var builder = new StringBuilder();
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public static string EncryptString(string encryptionKey, string data)
    {
        var keyString = Sha256Hash(encryptionKey).Substring(0, 32);
        var key = Encoding.UTF8.GetBytes(keyString);
        var iv = new byte[16];

        string encrypted = null;
        var aes = Aes.Create("AesManaged");
        aes.Key = key;
        aes.IV = iv;
        aes.Mode = CipherMode.CBC;

        try
        {
            var ms = new MemoryStream();

            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write))
            {
                using (var sw = new StreamWriter(cs))
                {
                    sw.Write(data);
                    sw.Close();
                }
                cs.Close();
            }
            var encoded = ms.ToArray();
            encrypted = Convert.ToBase64String(encoded);

            ms.Close();
        }
        catch (CryptographicException e)
        {
            Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
            return null;
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine("A file error occurred: {0}", e.Message);
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred: {0}", e.Message);
        }
        finally
        {
            aes.Clear();
            aes.Dispose();
        }
        return encrypted;
    }
    public static string EncryptByte(string encryptionKey, byte[] data)
    {
        var stringBuilder = new StringBuilder();

        foreach (var byteData in data)
        {
            stringBuilder.Append(Convert.ToString(byteData, 2).PadLeft(8, '0'));
        }

        return EncryptString(encryptionKey, stringBuilder.ToString());
    }

    public static string DecryptString(string decryptionKey, string cipherData)
    {
        var keyString = Sha256Hash(decryptionKey).Substring(0, 32);
        var key = Encoding.UTF8.GetBytes(keyString);
        var iv = new byte[16];
        try
        {
            using (var aes = Aes.Create("AesManaged"))
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                
                using (var memoryStream =
                       new MemoryStream(Convert.FromBase64String(cipherData)))
                using (var cryptoStream =
                       new CryptoStream(memoryStream,
                           aes.CreateDecryptor(key, iv),
                           CryptoStreamMode.Read))
                {
                    return new StreamReader(cryptoStream).ReadToEnd();
                }
            }
        }
        catch (CryptographicException e)
        {
            Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
            return null;
        }
    }

    public static byte[] DecryptByte(string encryptionKey, string data)
    {
        var decryptedData = DecryptString(encryptionKey, data);

        var numOfBytes = decryptedData.Length / 8;
        var bytes = new byte[numOfBytes];
        for (var i = 0; i < numOfBytes; ++i)
        {
            bytes[i] = Convert.ToByte(decryptedData.Substring(8 * i, 8), 2);
        }

        return bytes;
    }
}