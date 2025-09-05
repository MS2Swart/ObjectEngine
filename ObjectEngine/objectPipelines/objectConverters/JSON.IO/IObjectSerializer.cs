using ObjectEngine.objectPipelines.objectConverters.JSON.IO.SerializationAttributes;
using ObjectEngine.objectPipelines.objectManager;
using System.Reflection;

namespace ObjectEngine.objectPipelines.objectConverters.JSON.IO
{
    internal interface ISerializableIterator
    {
        #region SerializableStateMachine

        private protected static IEnumerable<(Guid Id, object TargetObject)> SerializableIterator<TObject>(ObjManager<TObject> objectManager) where TObject : class,new()
        {
            foreach (var (id, created, success) in objectManager.QUEUED_OBJECTS)
            {
                if (!success || created is null) continue;
                var Attribute = typeof(TObject).GetCustomAttribute<ObjectSerializeAttribute>();
                if (Attribute is not null)
                {
                    yield return (id, created);
                }

            }
        }

        #endregion
    }
}
