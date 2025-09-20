namespace OECore.OECryptography.AES
{
    internal interface ICreateKey<TObject> where TObject : class,new()
    {
        AESCryptographyHelper<TObject> CreateKey();
    }
    internal interface IEncrypt<TObject> where TObject : class, new()
    {
        AESCryptographyHelper<TObject> EncryptObject();
        AESCryptographyHelper<TObject> EncryptObject(Guid Id, string SerializedObject);
    }
    internal interface IDecrypt<TObject> where TObject : class, new()
    {
        AESCryptographyHelper<TObject> DecryptObject();
        AESCryptographyHelper<TObject> DecryptObject(Guid Id, byte[] EncryptedObject);
    }
}
