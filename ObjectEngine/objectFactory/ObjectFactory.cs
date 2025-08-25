using System.Collections.Concurrent;
using System.Text.Json.Serialization;

namespace ObjectEngine.objectFactory
{
    public class ObjectFactory
    {
        /// <summary>
        /// Dictionary property containing Guid type and object type.
        /// <list type="bullet">
        /// <item>
        /// GUID: Used as Unique Identifier
        /// </item>
        /// <item>
        /// OBJECT: Used in Object Engine Pipeline
        /// </item>
        /// </list>
        /// </summary>
        private protected ConcurrentDictionary<Guid,object> ObjectDictionary  = new();
        /// <summary>
        /// Creates a instance of typeof(T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>new T();//instance</returns>
        private protected static Type CreateType<T>() where T : class,new()
        {
            return typeof(T);
        }
        /// <summary>
        /// Creates an instance of object
        /// </summary>
        /// <param name="type"></param>
        /// <returns>object</returns>
        /// <exception cref="InvalidOperationException">Could not create type instance into an object instance.</exception>
        private protected static object CreateObject(Type instance) 
        {
            var Result = Activator.CreateInstance(instance) ?? throw new InvalidOperationException($"Could not create {instance.Name} into an object instance.");
            object ObjectType = Result;
            return ObjectType;
        }
        /// <summary>
        /// Adds a Created Guid for identification and a CreatedObject instance to a Dictionary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Tuple=(Guid,typeof(T)Instance)</returns>
        private protected (Guid Id, object CreatedObject, bool Success) AddObjectToDictionary(object createdObject) 
        {
            try
            {
                var createdGuid = Guid.NewGuid();
                var isSuccessful = ObjectDictionary.TryAdd(createdGuid, createdObject);
                var Result = (createdGuid, createdObject, isSuccessful);
                Console.WriteLine($"Object Factory: TEMP: ObjectToDictionary GUID: {Result.createdGuid} OBJECT: {Result.createdObject} STATUS: {Result.isSuccessful}");
                return Result;
            }
            catch (Exception ex)
            {

                throw new TypeInitializationException($"Could not add typeof({createdObject.GetType().Name}) to {ObjectDictionary.GetType().Name}", ex);
            }
            finally
            { 
                ObjectDictionary.Clear();
            }
        }
        /// <summary>
        /// Removes the created object from dictionary by using the GUID type used for identification.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>object</returns>
        private protected (Guid Id,object? ResultObject, bool Success) RemoveObjectFromDictionary(Guid id) 
        {
            var RemovalTargetGuid = id;
            bool ISSuccessful = ObjectDictionary.TryRemove(id,out var value); // Returns Bool
            var Result = (RemovalTargetGuid, value, ISSuccessful);
            Console.WriteLine($"TEMP: ObjectToDictionary GUID: {Result.RemovalTargetGuid} OBJECT: {Result.value} STATUS: {Result.ISSuccessful}");
            return Result;
        }
    }
}
