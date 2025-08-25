using ObjectEngine.objectPipelines.objectConverters.JSON.IO;
using ObjectEngine.objectPipelines.objectManager;

namespace OECore.OESerialization
{
    /// <summary>
    /// Helps Chain Object Serializer Methods
    /// </summary>
    /// <remarks>Requires an ObjManager<T> Management Class, Previously ObjectManager<T>, changed due to .NET Conflictions.</remarks>
    /// <typeparam name="T"></typeparam>
    /// <param name="objectManager"></param>
    public class SerializationHelper<T>(ObjManager<T> objectManager) : ObjectSerializer<T>(objectManager), ISerializeObjects<T>, ISubmitObjectSerializers<T> where T : class, new()
    {
        #region Serialization Method Chains.

        /// <summary>
        /// Serializes the current object and returns the instance for method chaining.
        /// </summary>
        /// <returns></returns>
        public SerializationHelper<T> SerializeObject()
        {
            Serialize();
            return this;
        }
        /// <summary>
        /// Deserializes the specified JSON string and associates it with the given identifier.
        /// </summary>
        public SerializationHelper<T> DeserializeObject(Guid Id,string json)
        {
            Deserialize(Id,json);
            return this;
        }
        /// <summary>
        /// Submits the current serializable object for processing and returns the current instance.
        /// </summary>
        public SerializationHelper<T> SubmitSerializableObject()
        {
            SubmitSerializable();
            return this;
        }
        /// <summary>
        /// Submits the current object for deserialization and returns the instance for method chaining.
        /// </summary>
        public SerializationHelper<T> SubmitDeserializableObject()
        {
            SubmitDeserializable();
            return this;
        }

        #endregion

    }
}
