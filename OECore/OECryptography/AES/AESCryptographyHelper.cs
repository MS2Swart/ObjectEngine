using ObjectEngine.objectPipelines.objectConverters.CRYPTOGRAPHY.IO.AES;
using OECore.OESerialization;
using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace OECore.OECryptography.AES
{
    public class AESCryptographyHelper<TObject>(SerializationHelper<TObject> objectSerializer) : ObjectAESCryptographer where TObject : class, new()
    {
        private static readonly ConcurrentBag<Aes> RESTRICTEDKEY = [];
        private protected ConcurrentDictionary<Guid, byte[]> ENCRYPTED_OBJECTS = new();
        private protected ConcurrentDictionary<Guid, string> DECRYPTED_OBJECTS = new();

        public AESCryptographyHelper<TObject> CreateKey()
        {
            var CreatedAES = CreateAes();
            RESTRICTEDKEY.Add(CreatedAES);
            return this;
        }

        #region Encryption

        public AESCryptographyHelper<TObject> EncryptObject()
        {
            
            foreach (var (Id, SerializedObject) in objectSerializer.SERIALIZED_QUEUED)
            {
                var EncryptedObject = Encrypt(SerializedObject);
                var EncryptedAdded = ENCRYPTED_OBJECTS.TryAdd(Id,EncryptedObject);
                if (EncryptedAdded)
                {
                    Console.WriteLine("[ENCRYPTION_ADDED] TO [ENCRYPTED_OBJECTS]");
                }
            }
            return this;
        }

        public AESCryptographyHelper<TObject> EncryptObject(Guid Id,string SerializedObject)
        {
            var EncryptedObject = Encrypt(SerializedObject);
            var EncryptedAdded = ENCRYPTED_OBJECTS.TryAdd(Id, EncryptedObject);
            if (EncryptedAdded)
            {
                Console.WriteLine("[ENCRYPTION_ADDED] TO [ENCRYPTED_OBJECTS]");
            }
            return this;
        }

        #endregion

        #region Decryption

        public AESCryptographyHelper<TObject> DecryptObject()
        {
            foreach (KeyValuePair<Guid,byte[]> EncryptedObject in ENCRYPTED_OBJECTS)
            {
                var DecryptedObject = Decrypt(EncryptedObject.Value);
                var EncryptedRemoved = ENCRYPTED_OBJECTS.Remove(EncryptedObject.Key, out byte[]? Result);
                if (Result is null)
                {
                    throw new NullReferenceException();
                }
                if (EncryptedRemoved)
                {
                    var DecryptionAdded = DECRYPTED_OBJECTS.TryAdd(EncryptedObject.Key,DecryptedObject);
                    if (DecryptionAdded)
                    {
                        Console.WriteLine($"[ENCRYPTION_REMOVED] AND [DECRYPTION_ADDED] TO [DYCRYPED_OBJECTS] : Source: {nameof(DecryptedObject)}");
                    }
                }
            }
            return this;
        }
        public AESCryptographyHelper<TObject> DecryptObject(Guid Id, byte[] EncryptedObject)
        {

                var DecryptedObject = Decrypt(EncryptedObject);
                var EncryptedRemoved = ENCRYPTED_OBJECTS.Remove(Id, out byte[]? Result);
                if (Result is null)
                {
                    throw new NullReferenceException();
                }
                if (EncryptedRemoved)
                {
                    var DecryptionAdded = DECRYPTED_OBJECTS.TryAdd(Id, DecryptedObject);
                    if (DecryptionAdded)
                    {
                        Console.WriteLine($"[ENCRYPTION_REMOVED] AND [DECRYPTION_ADDED] TO [DYCRYPED_OBJECTS] : Source: {nameof(DecryptedObject)}");
                    }
                }
            return this;
        }

        #endregion
    }
}
