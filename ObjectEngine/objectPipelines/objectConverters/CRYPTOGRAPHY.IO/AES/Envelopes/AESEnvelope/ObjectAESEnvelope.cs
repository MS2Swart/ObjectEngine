using System.Security.Cryptography;

namespace ObjectEngine.objectPipelines.objectConverters.CRYPTOGRAPHY.IO.AES.Envelopes.AESEnvelope
{
    internal class ObjectAESEnvelope<TObject>(Aes Key,string cyphertext) : IObjectAESCryptographer where TObject : class, new()
    {

        private protected TObject Payload = new();
        private protected byte[] Key = Key.Key;
        private protected byte[] IV = Key.IV;
        private protected string CypherText = cyphertext;

    }
}
