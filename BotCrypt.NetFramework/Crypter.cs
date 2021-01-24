using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace BotCrypt.NetFramework
{

    public class Crypter
    {

        internal static string Sha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string EncryptString(string EncryptionKey, string Data)
        {
            string KeyString = Sha256Hash(EncryptionKey).Substring(0, 32);
            byte[] Key = ASCIIEncoding.UTF8.GetBytes(KeyString);
            byte[] IV = new byte[16];

            string encrypted = null;
            RijndaelManaged rj = new RijndaelManaged
            {
                Key = Key,
                IV = IV,
                Mode = CipherMode.CBC
            };

            try
            {
                MemoryStream ms = new MemoryStream();

                using (CryptoStream cs = new CryptoStream(ms, rj.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(Data);
                        sw.Close();
                    }
                    cs.Close();
                }
                byte[] encoded = ms.ToArray();
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
                rj.Clear();
                rj.Dispose();
            }
            return encrypted;
        }
        public static string EncryptByte(string EncryptionKey, byte[] Data)
        {
            StringBuilder StringBuilder = new StringBuilder();

            foreach (byte Byte in Data)
            {
                StringBuilder.Append(Convert.ToString(Byte, 2).PadLeft(8, '0'));
            }

            return EncryptString(EncryptionKey, StringBuilder.ToString());
        }

        public static string DecryptString(string DecryptionKey, string CipherData)
        {
            string KeyString = Sha256Hash(DecryptionKey).Substring(0, 32);
            byte[] key = Encoding.UTF8.GetBytes(KeyString);
            byte[] iv = new byte[16];
            try
            {
                using (var rijndaelManaged =
                       new RijndaelManaged { Key = key, IV = iv, Mode = CipherMode.CBC })
                using (var memoryStream =
                       new MemoryStream(Convert.FromBase64String(CipherData)))
                using (var cryptoStream =
                       new CryptoStream(memoryStream,
                           rijndaelManaged.CreateDecryptor(key, iv),
                           CryptoStreamMode.Read))
                {
                    return new StreamReader(cryptoStream).ReadToEnd();
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }

        public static byte[] DecryptByte(string EncryptionKey, string Data)
        {
            var DecryptedData = DecryptString(EncryptionKey, Data);

            int numOfBytes = DecryptedData.Length / 8;
            byte[] bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(DecryptedData.Substring(8 * i, 8), 2);
            }

            return bytes;
        }
    }
}
