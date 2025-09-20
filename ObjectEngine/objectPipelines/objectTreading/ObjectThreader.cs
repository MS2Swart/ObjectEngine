using ObjectEngine.objectPipelines.objectManager;
using System.Collections.Concurrent;
using System.Threading;


namespace ObjectEngine.objectPipelines.objectTreading
{
    public class ObjectThreader<TObject>(int ConcurrentTaskLimit) : IWrapObjects<TObject> where TObject : class, new()
    {
        public delegate Task<TObject> Payload();
        private readonly SemaphoreSlim _semaphore = new(ConcurrentTaskLimit);

        internal class ObjectTaskState
        {
            public Guid Id { get; init; }
            public Payload? Payload { get; init; }
            public bool Success { get; set; }
            public bool IsProcessing { get; set; }
            public bool IsRunning { get; set; }
            public bool IsCompleted { get; set; }
            public TaskCompletionSource<TObject>? CompletionSource { get; init; }
        }
        private readonly ConcurrentDictionary<Guid, ObjectTaskState> PROCESSED_OBJECTS = [];
        private readonly ConcurrentQueue<Task<KeyValuePair<Guid, ObjectTaskState>>> PROCESSING_QUEUE = [];
        private readonly ConcurrentQueue<Func<Task<KeyValuePair<Guid, ObjectTaskState>>>> TASK_QUEUE = new();
        private readonly ConcurrentDictionary<Guid, ObjectTaskState> THREAD_POOL = [];
        private protected async Task<ObjectThreader<TObject>> PoolAsync(ObjManager<TObject> objManager, Payload ObjectPayload, TaskCompletionSource<TObject> taskCompletionSource)
        {
            var TaskWrapper = IWrapObjects<TObject>.ObjectWrapper(objManager, ObjectPayload, taskCompletionSource);
            foreach (var ObjectWrappedTask in TaskWrapper)
            {
                var WrappedTask = await ObjectWrappedTask;
                var IsAdded = THREAD_POOL.TryAdd(WrappedTask.Id, WrappedTask);
                if (IsAdded)
                {
                    Console.WriteLine($"ObjectID: {WrappedTask.Id} ADDED TO: THREAD_POOL, Object Success Status: {WrappedTask.Success}");
                }
            }
            return this;
        }
        private protected async Task<ObjectThreader<TObject>> PoolAsync(Guid Id, Payload ObjectPayload, TaskCompletionSource<TObject> taskCompletionSource)
        {
            var TaskWrapper = IWrapObjects<TObject>.ObjectWrapper(Id, ObjectPayload, taskCompletionSource);
            foreach (var ObjectWrappedTask in TaskWrapper)
            {   
                var WrappedTask = await ObjectWrappedTask;
                var IsAdded = THREAD_POOL.TryAdd(WrappedTask.Id, WrappedTask);
                if (IsAdded)
                {
                    Console.WriteLine($"ObjectID: {WrappedTask.Id} ADDED TO: THREAD_POOL, Object Success Status: {WrappedTask.Success}");
                }
            }
            return this;
        }
        private protected async Task<ObjectThreader<TObject>> EnqueAsync()
        {
            await Task.Run(() =>
            {

                foreach (KeyValuePair<Guid, ObjectTaskState> thread in THREAD_POOL)
                {
                    // Capture current thread for closure
                    var localThread = thread;

                    Task<KeyValuePair<Guid, ObjectTaskState>> ObjectThreadTask()
                    {
                        return Task.FromResult(localThread);
                    }

                    TASK_QUEUE.Enqueue(() => ObjectThreadTask());
                }
            });
            return this;
        }
        private protected async Task<ObjectThreader<TObject>> ProcessAsync()
        {
            await Task.Run(async () =>
            {
                foreach (Func<Task<KeyValuePair<Guid, ObjectTaskState>>> task in TASK_QUEUE)
                {
                    var localtask = task.Invoke();
                    PROCESSING_QUEUE.Enqueue(localtask);
                }
                foreach (var queued in PROCESSING_QUEUE)
                {
                    if (!PROCESSING_QUEUE.TryDequeue(out var task)) continue;

                    var (id, state) = await task;

                    state.IsRunning = true;
                    state.IsProcessing = true;

                    await _semaphore.WaitAsync();
                    try
                    {
                        if (state.Payload is null)
                            throw new NullReferenceException($"Payload was null for task {id}");

                        var result = await state.Payload.Invoke();
                        state.Success = result is not null;
                        if (state.CompletionSource is null)
                        {
                            throw new NullReferenceException($"State Completion Source has retuned null");
                        }
                        if (result is not null)
                        {
                            state.CompletionSource.SetResult(result);
                        }
                        else
                            throw new NullReferenceException("Result has returned null");
                    }
                    catch (Exception ex)
                    {
                        if (state.CompletionSource is null)
                        {
                            throw new NullReferenceException($"State Completion Source has retuned null", ex);
                        }
                        state.Success = false;
                        state.CompletionSource.SetException(ex);
                    }
                    finally
                    {
                        state.IsCompleted = true;
                        state.IsRunning = false;
                        _semaphore.Release();
                        _semaphore.Dispose();

                        PROCESSED_OBJECTS.TryAdd(id, state);
                    }
                }

            });

            return this;
        }
    }
}
