namespace ObjectEngine.objectPipelines.objectConverters.JSON.IO
{
    /// <summary>
    /// ISerialize Contract: Provides Serialization and Deserialization Patterns.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface ISerialize<T> where T : class,new()
    {
        ObjectSerializer<T> Serialize();
        ObjectSerializer<T> Deserialize(Guid Id,string targetObject);
    }
    /// <summary>
    /// ISubmitSerializers Contract: Provides Serialization and Deserialization Queue Submitions Patterns.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface ISubmitSerializers<T> where T : class, new()
    {
        ObjectSerializer<T> SubmitSerializable();
        ObjectSerializer<T> SubmitDeserializable();
    }
}
