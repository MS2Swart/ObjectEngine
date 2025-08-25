using ObjectEngine.objectPipelines.objectConverters.JSON.IO.SerializationAttributes;
using ObjectEngine.objectPipelines.objectManager;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;

namespace ObjectEngine.objectPipelines.objectConverters.JSON.IO
{
    /// <summary>
    /// Object Serializer Class Serializes C# Typs int intended Serializable Formats Default is always JSON.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="objectManager"></param>
    public class ObjectSerializer<T>(ObjManager<T> objectManager) : ISerialize<T>,ISubmitSerializers<T> where T : class, new()
    {
        public ConcurrentQueue<(Guid Id,string SerializedObject)> SERIALIZED_QUEUED = new();
        public ConcurrentQueue<(Guid Id, object DeserializedObject)> DESERIALIZED_QUEUED = new();
        private readonly ConcurrentDictionary<Guid, string> SERIALIZED_READY = new();
        private readonly ConcurrentDictionary<Guid, object> DESERIALIZED_READY = new();
        private readonly ObjManager<T> ObjManager = objectManager;

        /// <summary>
        /// Serialize Object to serializeble Format
        /// </summary>
        /// <remarks>This Chain Method will receive an expression tree for diffrent Serialization Format if needed.</remarks>
        /// <returns></returns>
        /// <exception cref="SerializationException"></exception>
        public ObjectSerializer<T> Serialize()
        {
            try
            {
                foreach (var (id, target) in SerializableIterator())
                {
                    var serialized = JsonSerializer.Serialize((T)target);
                    SERIALIZED_READY.TryAdd(id, serialized);
                    Console.WriteLine($"[Serialize] Id={id} Type={typeof(T).Name} Json={serialized}");
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException("Object failed to serialize", ex);
            }
            return this;
        }
        /// <summary>
        /// Derialize Serializable Format to C# System Type Format
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="FORMATDATA"></param>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        /// <exception cref="SerializationException"></exception>
        public ObjectSerializer<T> Deserialize(Guid Id,string FORMATDATA)
        {
            try
            {

                    var deserialized = JsonSerializer.Deserialize<object>(FORMATDATA)
                        ?? throw new IOException("Could not deserialize FORMAT_DATA to type T.");
                    DESERIALIZED_READY.TryAdd(Id, deserialized!);
                    Console.WriteLine($"[Deserialize] Id={Id} Type={typeof(T).Name} Obj={deserialized}");

            }
            catch (Exception ex)
            {
                throw new SerializationException("Object failed to deserialize", ex);
            }
            return this;
        }
        /// <summary>
        /// Submits Ready Available Serializable Format Objects to a serialized Queue.
        /// </summary>
        /// <returns></returns>
        public ObjectSerializer<T> SubmitSerializable() 
        {
            foreach (var serializable in SERIALIZED_READY)
            {
                SERIALIZED_QUEUED.Enqueue((serializable.Key, serializable.Value));
                Console.WriteLine($"Serialized Object Submited Id: {serializable.Key} Object: {serializable.Value}");
            }
            return this;
        }
        /// <summary>
        /// Submits a Ready Available Deserialized FORMATDATA to be deserialized as C# System Type.
        /// </summary>
        /// <returns></returns>
        public ObjectSerializer<T> SubmitDeserializable() 
        {
            foreach (var deserializable in DESERIALIZED_READY)
            {
                DESERIALIZED_QUEUED.Enqueue((deserializable.Key, deserializable.Value));
                Console.WriteLine($"Deserialized Object Submited Id {deserializable.Key} Object: {deserializable.Value}");
            }
            return this;
        }
        #region SerializableStateMachine

        // If the attribute decorates the class T
        private static bool HasAttrOnT<TAttr>() where TAttr : Attribute
            => typeof(T).GetCustomAttribute<TAttr>() is not null;

        // If the attribute decorates the created instance's concrete type
        //private static bool HasAttrOnInstance<TAttr>(object o) where TAttr : Attribute
        //    => o?.GetType().GetCustomAttribute<TAttr>() is not null;

        private IEnumerable<(Guid Id, object TargetObject)> SerializableIterator()
        {
            foreach (var (id, created, success) in ObjManager.QUEUED_OBJECTS)
            {
                if (!success || created is null) continue;

                // OPTION A: attribute on T
                if (HasAttrOnT<ObjectSerializeAttribute>())
                    yield return (id, created);

                // OPTION B: attribute on instance type
                // if (HasAttrOnInstance<ObjectSerializeAttribute>(created))
                //     yield return (id, created);
            }
        }


        #endregion
    }
}
