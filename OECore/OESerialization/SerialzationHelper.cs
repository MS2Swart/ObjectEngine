using ObjectEngine.objectPipelines.objectConverters.JSON.IO;
using ObjectEngine.objectPipelines.objectManager;

namespace OECore.OESerialization
{

    /// <summary>
    /// Helps Chain Object Serializer Methods
    /// </summary>
    /// <remarks>Requires an ObjManager<T> Management Class, Previously ObjectManager<T>, changed due to .NET Conflictions.</remarks>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="objectManager"></param>
    public class SerializationHelper<TObject>(ObjManager<TObject>? objectManager = null) : ObjectSerializer<TObject>(objectManager), ISerializeObjects<TObject>, ISubmitObjectSerializers<TObject> where TObject : class, new()
    {

        #region Serialization Method Chains.


        /// <summary>
        /// Serializes the current object and returns the instance for method chaining.
        /// </summary>
        /// <returns></returns>
        public SerializationHelper<TObject> SerializeObject(out List<string?> SerializedObject)
        {
            Serialize(out var serializedObject);
            SerializedObject = serializedObject;
            return this;
        }
        public SerializationHelper<TObject> SerializeObject(Guid Id, object TargetObject, out string? SerializedObject)
        {
            Serialize(Id, TargetObject, out var serializedObject);
            SerializedObject = serializedObject;
            return this;
        }
        /// <summary>
        /// Deserializes the specified JSON string and associates it with the given identifier.
        /// </summary>
        public SerializationHelper<TObject> DeserializeObject(out List<TObject> DeserializedObject)
        {

            Deserialize(out var deserializedObject);
            DeserializedObject = deserializedObject;
            return this;
        }
        public SerializationHelper<TObject> DeserializeObject(Guid Id, string FORMATDATA, out TObject DeserializedObject)
        {

            Deserialize(Id,FORMATDATA,out var deserializedObject);
            DeserializedObject = deserializedObject;
            return this;
        }
        /// <summary>
        /// Submits the current serializable object for processing and returns the current instance.
        /// </summary>
        public SerializationHelper<TObject> SubmitSerializableObject()
        {
            SubmitSerializable();
            return this;
        }
        /// <summary>
        /// Submits the current object for deserialization and returns the instance for method chaining.
        /// </summary>
        public SerializationHelper<TObject> SubmitDeserializableObject()
        {
            SubmitDeserializable();
            return this;
        }

        public SerializationHelper<TObject> ClearSerializableObjects()
        {
            ClearSerializer();
            return this;
        }

        #endregion

    }
}
