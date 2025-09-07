using System.Security.Cryptography;

namespace ObjectEngine.objectPipelines.objectConverters.CRYPTOGRAPHY.IO.AES
{
    public class ObjectAESCryptographer : IObjectAESCryptographer
    {
        private protected static Aes CreateAes() 
        {
            var CreatedAes = IObjectAESCryptographer.CreateAesKey();
            return CreatedAes;
        }
        private protected static byte[] Encrypt(string plainText)
        {
            var EncryptedText = IObjectAESCryptographer.Encrypt(plainText);
            return EncryptedText;
        }
        private protected static string Decrypt(byte[] cipherText)
        {
            var DecryptedText = IObjectAESCryptographer.Decrypt(cipherText);
            return DecryptedText;
        }
    }
}
