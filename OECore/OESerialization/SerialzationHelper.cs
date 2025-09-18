using ObjectEngine.objectPipelines.objectConverters.JSON.IO;
using ObjectEngine.objectPipelines.objectManager;
using ObjectEngine.objectPipelines.objectTreading;
using OECore.OEManagement;
using System.Net.Http.Headers;
using System.Runtime.Serialization;

namespace OECore.OESerialization
{

    /// <summary>
    /// Helps Chain Object Serializer Methods
    /// </summary>
    /// <remarks>Requires an ObjManager<T> Management Class, Previously ObjectManager<T>, changed due to .NET Conflictions.</remarks>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="objectManager"></param>
    public class SerializationHelper<TObject>(ObjManager<TObject> objectManager) : ObjectSerializer<TObject>(objectManager), ISerializeObjects<TObject>, ISubmitObjectSerializers<TObject> where TObject : class, new()
    {

        #region Serialization Method Chains.


        /// <summary>
        /// Serializes the current object and returns the instance for method chaining.
        /// </summary>
        /// <returns></returns>
        public SerializationHelper<TObject> SerializeObject()
        {
            Serialize();
            return this;
        }
        /// <summary>
        /// Deserializes the specified JSON string and associates it with the given identifier.
        /// </summary>
        public SerializationHelper<TObject> DeserializeObject()
        {
            foreach (var (Id, SerializedObject) in SERIALIZED_QUEUED)
            {
                Deserialize(Id, SerializedObject);
            }
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

        #endregion

    }
}
