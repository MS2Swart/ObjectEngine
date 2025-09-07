using ObjectEngine.objectPipelines.objectObjectDevelopment;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace ObjectEngine.objectPipelines.objectConverters.CRYPTOGRAPHY.IO.AES.Envelopes.AESEnvelope
{
    internal class ObjectAESEnvelope<TObject> : ObjectDeveloper<TObject>, IObjectAESCryptographer where TObject : class, new()
    {
        private protected object? ObjectPayload { get; private set; } = InitializeObjectPayload() ?? null;
        private static object InitializeObjectPayload()
        {
            return Payload;
        }

        private static protected SymmetricAlgorithm? SymmetricAlgorithm { get; private set; }
        
    }
}
