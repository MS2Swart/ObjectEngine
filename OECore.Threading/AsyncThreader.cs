using ObjectEngine.objectPipelines.objectManager;
using OECore.OEThreading;

namespace OECore.AsnycThreading
{
    public static class AsyncThreader
    {
        public static  AsyncThreaderHelper<TObject> Build<TObject>(int concurrentLimit) where TObject : Task<AsyncThreaderHelper<TObject>>, new()
        {
            var Threader = new AsyncThreaderHelper<TObject>(concurrentLimit);
            return Threader;
        }
        public static async Task<AsyncThreaderHelper<TObject>> AddAsync<TObject>(AsyncThreaderHelper<TObject>asyncThreaderHelper, Guid Id, ObjectEngine.objectPipelines.objectTreading.ObjectThreader<TObject>.Payload Target,TaskCompletionSource<TObject> taskCompletionSource) where TObject : Task<AsyncThreaderHelper<TObject>>, new()
        {
            var Threader = asyncThreaderHelper;
            await Threader.PoolObjectAysnc(Id, Target, taskCompletionSource);
            return Threader;
        }

        public static async Task<AsyncThreaderHelper<TObject>> AddAsync<TObject>(AsyncThreaderHelper<TObject> asyncThreaderHelper, ObjManager<TObject> objManager, ObjectEngine.objectPipelines.objectTreading.ObjectThreader<TObject>.Payload Target, TaskCompletionSource<TObject> taskCompletionSource) where TObject : Task<AsyncThreaderHelper<TObject>>, new()
        {
            var Threader = asyncThreaderHelper;
            await Threader.PoolObjectAysnc(objManager, Target, taskCompletionSource);
            return Threader;
        }

        public static async Task<AsyncThreaderHelper<TObject>> StartAsync<TObject>(AsyncThreaderHelper<TObject> asyncThreaderHelper) where TObject : Task<AsyncThreaderHelper<TObject>>, new()
        {
            var Threader = asyncThreaderHelper;
            await Threader.EnqueObjectAsync();
            await Threader.ProcessObjectAsync();
            return Threader;
        }

    }
}
