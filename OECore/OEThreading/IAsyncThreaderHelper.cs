using ObjectEngine.objectPipelines.objectManager;
namespace OECore.OEThreading
{
    internal interface IPoolObjects<TObject> where TObject : class,new()
    {
        Task<AsyncThreaderHelper<TObject>> PoolObjectAysnc(Guid Id, ObjectEngine.objectPipelines.objectTreading.ObjectThreader<TObject>.Payload payload, TaskCompletionSource<TObject> taskCompletionSource);
        Task<AsyncThreaderHelper<TObject>> PoolObjectAysnc(ObjManager<TObject> objManager, ObjectEngine.objectPipelines.objectTreading.ObjectThreader<TObject>.Payload payload, TaskCompletionSource<TObject> taskCompletionSource);
    }
    internal interface IEnqueueObjects<TObject> where TObject : class, new()
    {
        Task<AsyncThreaderHelper<TObject>> EnqueObjectAsync();
    }
    internal interface IProcessObjects<TObject> where TObject : class, new()
    {
        Task<AsyncThreaderHelper<TObject>> ProcessObjectAsync();
    }
}
