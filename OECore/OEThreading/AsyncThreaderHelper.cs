using ObjectEngine.objectPipelines.objectManager;
using ObjectEngine.objectPipelines.objectTreading;

namespace OECore.OEThreading
{
    public class AsyncThreaderHelper<TObject>(int ConcurrentTaskLimit) : ObjectThreader<TObject>(ConcurrentTaskLimit), IPoolObjects<TObject>, IEnqueueObjects<TObject>, IProcessObjects<TObject> where TObject : class, new()
    {
        public async Task<AsyncThreaderHelper<TObject>> PoolObjectAysnc(Guid Id, Payload payload,TaskCompletionSource<TObject> taskCompletionSource)
        {
            await PoolAsync(Id, payload, taskCompletionSource);
            return this;
        }
        public async Task<AsyncThreaderHelper<TObject>> PoolObjectAysnc(ObjManager<TObject> objManager,Payload payload,TaskCompletionSource<TObject> taskCompletionSource)
        {
            await PoolAsync(objManager, payload, taskCompletionSource);
            return this;
        }
        public async Task<AsyncThreaderHelper<TObject>> EnqueObjectAsync()
        {
            await EnqueAsync();
            return this;
        }
        public async Task<AsyncThreaderHelper<TObject>> ProcessObjectAsync()
        {
            await ProcessAsync();
            return this;
        }
    }
}
