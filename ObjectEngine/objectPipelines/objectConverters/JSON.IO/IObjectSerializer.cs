using ObjectEngine.objectPipelines.objectManager;

namespace ObjectEngine.objectPipelines.objectConverters.JSON.IO
{
    internal interface ISerializableIterator
    {
        #region SerializableStateMachine

        private protected static IEnumerable<(Guid Id, object TargetObject)> SerializableIterator<TObject>(ObjManager<TObject> objectManager) where TObject : class, new()
        {
            foreach (var (id, created, success) in objectManager.QUEUED_OBJECTS)
            {
                if (!success || created is null) continue;
                    yield return (id, created);
                

            }
        }

        #endregion
    }
}
