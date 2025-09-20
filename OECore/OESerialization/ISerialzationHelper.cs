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
        SerializationHelper<TObject> SerializeObject(out List<string?> SerializedObject);
        SerializationHelper<TObject> SerializeObject(Guid Id, object TargetObject, out string? SerializedObject);
    }
    /// <summary>
    /// ISubmitObjectSerializers Contracts: Provides Ready available collection Objects to Ready Available collection Queues.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    internal interface ISubmitObjectSerializers<TObject> where TObject : class, new()
    {
        SerializationHelper<TObject> DeserializeObject(out List<TObject> DeserializedObject);
        SerializationHelper<TObject> DeserializeObject(Guid Id, string FORMATDATA, out TObject DeserializedObject);
    }

    #endregion
}
