using ObjectEngine.objectPipelines.objectManager;

namespace OECore.OESerialization
{
    #region Object Engine Serialzation Helper Contracts

    /// <summary>
    /// ISerializeObjects Contract: Provides Serialization and Deserialization Patterns.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    internal interface ISerializeObjects<TObject> where TObject : class, new()
    {
        SerializationHelper<TObject> SerializeObject();
        SerializationHelper<TObject> DeserializeObject();
    }
    /// <summary>
    /// ISubmitObjectSerializers Contracts: Provides Ready available collection Objects to Ready Available collection Queues.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    internal interface ISubmitObjectSerializers<TObject> where TObject : class, new()
    {
        SerializationHelper<TObject> SubmitSerializableObject();
        SerializationHelper<TObject> SubmitDeserializableObject();
    }

    #endregion
}
