using ObjectEngine.objectPipelines.objectConverters.JSON.IO;

namespace ObjectEngine.objectPipelines.objectManager
{
    /// <summary>
    /// ICreate Method Contract: Create ObjectCreate instance\instances.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface ICreate<T> where T : class,new()
    {
        ObjManager<T> Create();
    }
    /// <summary>
    /// IRemove Method Contract: Remove ObjectCreate instance\instances
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IRemove<T> where T : class, new() 
    {
        ObjManager<T> Remove(Guid ObjectID);
        ObjManager<T> Clear(ObjectSerializer<T>? Serializer = null);
    }
    /// <summary>
    /// ISubmit Method Contract: Submits Objects to a ConcurentQueue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface ISubmit<T> where T : class, new() 
    {
        ObjManager<T> Submit();
    }
    /// <summary>
    /// IEdit Method Contract: Edits the Object before submiting to Queue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IEdit<T> where T : class,new()
    {
        ObjManager<T> Edit(Guid id, object newObject);
    }
}
