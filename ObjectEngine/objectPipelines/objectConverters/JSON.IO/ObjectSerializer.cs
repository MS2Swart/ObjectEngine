using Microsoft.VisualBasic;
using ObjectEngine.objectPipelines.objectManager;
using System.Collections.Concurrent;
using System.Runtime.Serialization;
using System.Text.Json;

namespace ObjectEngine.objectPipelines.objectConverters.JSON.IO
{
    /// <summary>
    /// Object Serializer Class Serializes C# Typs int intended Serializable Formats Default is always JSON.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="objectManager"></param>
    public class ObjectSerializer<TObject> : ISerializableIterator where TObject : class, new()
    {
        public ConcurrentQueue<(Guid Id,string SerializedObject)> SERIALIZED_QUEUED = new();
        public ConcurrentQueue<(Guid Id, object DeserializedObject)> DESERIALIZED_QUEUED = new();
        private readonly ConcurrentDictionary<Guid, string> SERIALIZED_READY = new();
        private readonly ConcurrentDictionary<Guid, object> DESERIALIZED_READY = new();
        private readonly ObjManager<TObject>? ObjManager;
        public ObjectSerializer(ObjManager<TObject>? objectManager = null)
        {
            if (objectManager is not null)
            {
                ObjManager = objectManager;
            }
        }

        /// <summary>
        /// Serialize Object to serializeble Format
        /// </summary>
        /// <remarks>This Chain Method will receive an expression tree for diffrent Serialization Format if needed.</remarks>
        /// <returns></returns>
        /// <exception cref="SerializationException"></exception>
        private protected ObjectSerializer<TObject> Serialize(out List<string?> SerializedObjects)
        {
             var _serializedObjects =new List<string?>();
            try
            {
                if (ObjManager is null)
                {
                    throw new NullReferenceException($"Could not {nameof(Serialize)},Missing Depedency: {nameof(ObjManager)}");
                }
                Parallel.ForEach(ISerializableIterator.SerializableIterator(ObjManager), new ParallelOptions(){ MaxDegreeOfParallelism = Environment.ProcessorCount }, serializable => {
                    var serialized = JsonSerializer.Serialize((TObject)serializable.TargetObject);
                    _serializedObjects.Add(serialized);
                    SERIALIZED_READY.TryAdd(serializable.Id, serialized);
                    Console.WriteLine($"[Serialize] Id={serializable.TargetObject} Type={typeof(TObject).Name} Json={serialized}");
                });
                SerializedObjects = _serializedObjects;
            }
            catch (Exception ex)
            {
                throw new SerializationException("Object failed to serialize", ex);
            }
            return this;
        }
        private protected ObjectSerializer<TObject> Serialize(Guid Id,object TargetObject, out string? SerializedObject)
        {
            try
            {
                    var serialized = JsonSerializer.Serialize((TObject)TargetObject);
                SerializedObject = serialized;
                    SERIALIZED_READY.TryAdd(Id, serialized);
                    Console.WriteLine($"[Serialize] Id={TargetObject} Type={typeof(TObject).Name} Json={serialized}");
            }
            catch (Exception ex)
            {
                throw new SerializationException("Object failed to serialize", ex);
            }
            return this;
        }
        private protected ObjectSerializer<TObject> Deserialize( out List<TObject> DeserializedObjects)
        {
            try
            {
                var deserializedObjects = new List<TObject>();
                foreach (var (Id, SerializedObject) in SERIALIZED_QUEUED)
                {
                    var deserialized = JsonSerializer.Deserialize<TObject>(SerializedObject)
                    ?? throw new IOException("Could not deserialize FORMAT_DATA to type T.");
                    deserializedObjects.Add(deserialized);
                                    DESERIALIZED_READY.TryAdd(Id, deserialized!);
                                    Console.WriteLine($"[Deserialize] Id={Id} Type={typeof(TObject).Name} Obj={deserialized}");
                }
                DeserializedObjects = deserializedObjects;

            }
            catch (Exception ex)
            {
                throw new SerializationException("Object failed to deserialize", ex);
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
        private protected ObjectSerializer<TObject> Deserialize(Guid Id,string FORMATDATA,out TObject DeserializedObject)
        {
            try
            {

                    var deserialized = JsonSerializer.Deserialize<TObject>(FORMATDATA)
                        ?? throw new IOException("Could not deserialize FORMAT_DATA to type T.");
                    DeserializedObject = deserialized;
                    DESERIALIZED_READY.TryAdd(Id, deserialized!);
                    Console.WriteLine($"[Deserialize] Id={Id} Type={typeof(TObject).Name} Obj={deserialized}");

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
        private protected ObjectSerializer<TObject> SubmitSerializable() 
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
        private protected ObjectSerializer<TObject> SubmitDeserializable() 
        {
            foreach (var deserializable in DESERIALIZED_READY)
            {
                DESERIALIZED_QUEUED.Enqueue((deserializable.Key, deserializable.Value));
                Console.WriteLine($"Deserialized Object Submited Id {deserializable.Key} Object: {deserializable.Value}");
            }
            return this;
        }
        /// <summary>
        /// Clears All Object Serializer Collections
        /// </summary>
        /// <returns></returns>
        private protected ObjectSerializer<TObject> ClearSerializer()
        {
            SERIALIZED_READY.Clear();DESERIALIZED_READY.Clear(); SERIALIZED_QUEUED.Clear();DESERIALIZED_QUEUED.Clear();
            return this;
        }
    }
}
