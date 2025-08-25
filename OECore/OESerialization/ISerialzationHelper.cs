namespace OECore.OESerialization
{
    #region Object Engine Serialzation Helper Contracts
    
    /// <summary>
    /// ISerializeObjects Contract: Provides Serialization and Deserialization Patterns.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface ISerializeObjects<T> where T : class, new()
    {
        SerializationHelper<T> SerializeObject();
        SerializationHelper<T> DeserializeObject(Guid Id, string targetObject);
    }
    /// <summary>
    /// ISubmitObjectSerializers Contracts: Provides Ready available collection Objects to Ready Available collection Queues.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface ISubmitObjectSerializers<T> where T : class, new()
    {
        SerializationHelper<T> SubmitSerializableObject();
        SerializationHelper<T> SubmitDeserializableObject();
    }

    #endregion
}
