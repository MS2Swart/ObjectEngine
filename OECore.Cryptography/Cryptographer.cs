using OECore.OECryptography.AES;

namespace OECore.Cryptography
{
    public class Cryptographer
    {
        public static AESCryptographyHelper<TObject> CreateKey<TObject>(AESCryptographyHelper<TObject> cryptographyHelper) where TObject : AESCryptographyHelper<TObject>,new()
        {
            var Instance = cryptographyHelper;
            Instance.CreateKey();
            return Instance;
        }
        public static AESCryptographyHelper<TObject> Encrypt<TObject>(AESCryptographyHelper<TObject> cryptographyHelper) where TObject : AESCryptographyHelper<TObject>, new()
        {
            var Instance = cryptographyHelper;
            Instance.EncryptObject();
            return Instance;
        }
        public static AESCryptographyHelper<TObject> Decrypt<TObject>(AESCryptographyHelper<TObject> cryptographyHelper) where TObject : AESCryptographyHelper<TObject>, new()
        {
            var Instance = cryptographyHelper;
            Instance.DecryptObject();
            return Instance;
        }
    }
}
