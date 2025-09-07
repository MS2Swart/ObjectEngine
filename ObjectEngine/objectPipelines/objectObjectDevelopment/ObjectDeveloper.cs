using ObjectEngine.objectPipelines.objectConverters.CRYPTOGRAPHY.IO.AES;
using ObjectEngine.objectPipelines.objectConverters.CRYPTOGRAPHY.IO.AES.Envelopes.AESEnvelope;

namespace ObjectEngine.objectPipelines.objectObjectDevelopment
{
    public class ObjectDeveloper<TObject> : IDevelopObject<TObject> where TObject : class, new()
    {
        private protected static TObject Payload { get; set; } = new TObject();

    }
}
