using ObjectEngine.objectPipelines.objectConverters.JSON.IO.SerializationAttributes;
using ObjectEngine.objectPipelines.objectManager;
using System.Reflection;

namespace ObjectEngine.objectPipelines.objectConverters.JSON.IO
{
    /// <summary>
    /// ISerialize Contract: Provides Serialization and Deserialization Patterns.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface ISerialize<TObject> where TObject : class,new()
    {
        ObjectSerializer<TObject> Serialize();
        ObjectSerializer<TObject> Deserialize(Guid Id,string targetObject);
    }
    /// <summary>
    /// ISubmitSerializers Contract: Provides Serialization and Deserialization Queue Submitions Patterns.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface ISubmitSerializers<TObject> where TObject : class, new()
    {
        ObjectSerializer<TObject> SubmitSerializable();
        ObjectSerializer<TObject> SubmitDeserializable();
    }
    internal interface ISerializableIterator
    {
        #region SerializableStateMachine

        // If the attribute decorates the class T
        private static bool HasAttrOnT<TObject>() where TObject : Attribute
            => typeof(TObject).GetCustomAttribute<TObject>() is not null;

        // If the attribute decorates the created instance's concrete type
        //private static bool HasAttrOnInstance<TAttr>(object o) where TAttr : Attribute
        //    => o?.GetType().GetCustomAttribute<TAttr>() is not null;

        private protected static IEnumerable<(Guid Id, object TargetObject)> SerializableIterator<TObject>(ObjManager<TObject> objectManager) where TObject : class,new()
        {
            foreach (var (id, created, success) in objectManager.QUEUED_OBJECTS)
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
