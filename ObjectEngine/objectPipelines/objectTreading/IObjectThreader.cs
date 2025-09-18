using ObjectEngine.objectPipelines.objectManager;
using OECore.OESerialization;

namespace ObjectEngine.objectPipelines.objectTreading
{
    internal interface IWrapObjects<TObject> where TObject : class, new()
    {
        private protected static Task<TObject> WrapObjectTask(ObjectThreader<TObject>.Payload payload, TaskCompletionSource<TObject> taskCompletionSource)
        {

            async Task<TObject> wrappedTask()
            {
                try
                {
                    var result = await payload();
                    taskCompletionSource.SetResult(result);
                    return result;
                }
                catch (Exception ex)
                {
                    taskCompletionSource.SetException(ex);
                    throw;
                }
            }
            return wrappedTask();
        }
        private protected static IEnumerable<Task<ObjectThreader<TObject>.ObjectTaskState>> ObjectWrapper(ObjManager<TObject> objManager, ObjectThreader<TObject>.Payload payload,TaskCompletionSource<TObject> taskCompletionSource)
        {
            foreach(var (Id, PayloadObject, Success) in objManager.QUEUED_OBJECTS)
            {
               var WrapedObject = WrapObjectTask(payload, taskCompletionSource);

                async Task<ObjectThreader<TObject>.ObjectTaskState> WrapObjectReturnTask()
                {
                    try
                    {
                        taskCompletionSource.SetResult(await payload());
                        var NewInstance = new ObjectThreader<TObject>.ObjectTaskState() 
                        {
                            Id = Id,
                            Payload = payload,
                            Success = Success,
                            IsProcessing = false,
                            IsCompleted = false,
                            IsRunning = false,
                            CompletionSource = taskCompletionSource
                        };
                        return NewInstance;
                    }
                    catch (Exception ex)
                    {
                        taskCompletionSource.SetException(ex);
                        throw;
                    }
                }
                var WrapedObjectReturnTask = WrapObjectReturnTask();
                yield return WrapedObjectReturnTask;

            }
        }
    }

}
