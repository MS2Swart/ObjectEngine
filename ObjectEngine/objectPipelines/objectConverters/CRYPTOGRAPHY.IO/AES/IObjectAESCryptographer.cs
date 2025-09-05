using System.Security.Cryptography;

namespace ObjectEngine.objectPipelines.objectConverters.CRYPTOGRAPHY.IO.AES
{
    internal interface IObjectAESCryptographer
    {
        private protected static Aes? Vault { get; private set; }
        private protected static Aes CreateAesKey()
        {
            using var CreateAes = Aes.Create();
            Vault = CreateAes;
            return CreateAes;
        }
        private protected static byte[] Encrypt(string plainText)
        {
            try
            {
                if (Vault is null)
                {
                    throw new ArgumentNullException("Vault cannot be null",nameof(Vault));
                }
                using var TextObjectMemoryStream = new MemoryStream();
                using var TextObjectCryptoStream = new CryptoStream(TextObjectMemoryStream, Vault.CreateEncryptor(), CryptoStreamMode.Write);
                using var TextObjectStreamWriter = new StreamWriter(TextObjectCryptoStream);
                TextObjectStreamWriter.Write(plainText);
                var TextObjectArray = TextObjectMemoryStream.ToArray();
                return TextObjectArray;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Encryption Failed]: PlainTextObject");
                throw new Exception("[Encryption Failed]: PlainTextObject", ex);
            }

        }
        private protected static string Decrypt(byte[] cipherText)
        {
            if (Vault is null)
            {
                throw new ArgumentNullException(nameof(Vault));
            }
            using var CypherObjectMemoryStream = new MemoryStream(cipherText);
            using var CypherObjectCryptoStream = new CryptoStream(CypherObjectMemoryStream, Vault.CreateDecryptor(), CryptoStreamMode.Read);
            using var CypherObjectStreamWriter = new StreamReader(CypherObjectCryptoStream);
            var ReadCypherText = CypherObjectStreamWriter.ReadToEnd();
            return ReadCypherText;
        }
    }
}
