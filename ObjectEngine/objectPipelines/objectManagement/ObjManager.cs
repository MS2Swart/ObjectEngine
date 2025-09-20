using ObjectEngine.objectFactory;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ObjectEngine.objectPipelines.objectManager
{
    /// <summary>
    /// Object Manager shotend to ObjManager due to .Net alias conflictions, provides Object collections patterns and a Queue for Object Control flow.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjManager<TObject> : ObjectFactory,IManage where TObject : class, new()
    {
        public ConcurrentQueue<(Guid Id,object CreatedObject, bool Success)> QUEUED_OBJECTS = new();
        private readonly ConcurrentDictionary<Guid, (object CreatedObject, bool Success)> PASSED_OBJECTS = new();
        private readonly ConcurrentDictionary<Guid, (object? CreatedObject, bool Failed)> FAILED_OBJECTS = new();

        #region CreationChain

        /// <summary>
        /// Creates an object That will be added to Either a PassedObject or FailedObject Dictionary
        /// </summary>
        /// <returns>ObjManager<T></returns>
        private protected ObjManager<TObject> Create()
        {

                    (Guid Id, object CreatedObject, bool Success) OBJECT_INSTANCE_STORED;
                    foreach (var OBJECT_INSTANCE in IManage.InitialCreationRunner(ICreate.SystemObject<TObject>(ICreate.SystemType<TObject>())))
                    {
                        var (Id, CreatedObject, Success) = AddObjectToDictionary(OBJECT_INSTANCE);
                        if (Success == true && CreatedObject is not null)
                        {
                            OBJECT_INSTANCE_STORED = (Id, CreatedObject, Success);
                            var OBJECT_INSTANCE_STORED_TRASFORM = KeyValuePair.Create(OBJECT_INSTANCE_STORED.Id, (OBJECT_INSTANCE_STORED.CreatedObject, OBJECT_INSTANCE_STORED.Success));
                            PASSED_OBJECTS.TryAdd(OBJECT_INSTANCE_STORED_TRASFORM.Key, OBJECT_INSTANCE_STORED_TRASFORM.Value);
                        }
                        else
                        {
                            FAILED_OBJECTS.TryAdd(Id, (CreatedObject, Success));
                        }

                    }
            return this;
        }
        private protected ObjManager<TObject> Create(Guid Guid)
        {
                (Guid Id, object CreatedObject, bool Success) OBJECT_INSTANCE_STORED;
                var (Id, CreatedObject, Success) = AddObjectToDictionary(Guid, ICreate.SystemObject<TObject>(ICreate.SystemType<TObject>()));

                if (Success == true && CreatedObject is not null)
                {
                    OBJECT_INSTANCE_STORED = (Id, CreatedObject, Success);
                    var OBJECT_INSTANCE_STORED_TRASFORM = KeyValuePair.Create(OBJECT_INSTANCE_STORED.Id, (OBJECT_INSTANCE_STORED.CreatedObject, OBJECT_INSTANCE_STORED.Success));
                    PASSED_OBJECTS.TryAdd(OBJECT_INSTANCE_STORED_TRASFORM.Key, OBJECT_INSTANCE_STORED_TRASFORM.Value);
                }
                else
                {
                    FAILED_OBJECTS.TryAdd(Id, (CreatedObject, Success));
                }

            return this;
        }


        #endregion

        #region RemovalChain
        /// <summary>
        /// Removed and object from the passed Object Dictionary
        /// </summary>
        /// <param name="ObjectID"></param>
        /// <returns>ObjManager<T></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        private protected ObjManager<TObject> Remove(Guid objectId)
        {
            if (PASSED_OBJECTS.TryRemove(objectId, out var removed))
            {
                Console.WriteLine($"Object Manager: Removed Id={objectId}, Obj={removed.CreatedObject}, Success={removed.Success}");
                return this;
            }

            throw new InvalidDataException($"Id={objectId} not found in PASSED_OBJECTS");
        }

        /// <summary>
        /// Clears the whole heap of collections available.
        /// </summary>
        /// <returns>ObjManager<T></returns>
        /// <exception cref="OperationCanceledException"></exception>
        private protected ObjManager<TObject> Clear()
        {
            try
            {
                PASSED_OBJECTS.Clear(); FAILED_OBJECTS.Clear(); QUEUED_OBJECTS.Clear(); ObjectDictionary.Clear();
                Console.WriteLine($"Object Manager Cleared: Collection Pool for source: {typeof(TObject).Name}");
            }
            catch (Exception ex)
            {
                throw new OperationCanceledException($"Clear opperation canceled or could not finish, please see to ObjectEngine: {this.GetType().Name}",ex);
            }
            return this;
        }

        #endregion

        #region UpdateChain

        private protected ObjManager<TObject> Update(Guid Id ,object TargetObject)
        {
            var IsCompleted = PASSED_OBJECTS.TryGetValue(Id, out var ObjectData);
            if (IsCompleted)
            {
                var IsUpdated = PASSED_OBJECTS.TryUpdate(Id,ObjectData,(TargetObject,IsCompleted));
                if (IsUpdated)
                {
                    throw new Exception($"Update Failed: {nameof(TargetObject)} to {nameof(PASSED_OBJECTS)}, Completion Status: {IsCompleted}, Update Status: {IsUpdated}");
                }
            }
            return this;
        }

        #endregion

        #region SubmitionChain
        /// <summary>
        /// Submits the object to a Queue that tell the engine witch objects are ready for use.
        /// Note:  
        /// </summary>
        /// <returns>ObjManager<T></returns>
        /// <exception cref="Exception"></exception>
        private protected ObjManager<TObject> Submit() 
        {
            if (!FAILED_OBJECTS.IsEmpty)
            {
                throw new Exception($"Broken objects cannot be submited, Object Count: {FAILED_OBJECTS.Count}");
            }
            foreach (var Submission in PASSED_OBJECTS)
            {
                QUEUED_OBJECTS.Enqueue((Submission.Key, Submission.Value.CreatedObject, Submission.Value.Success));
                var QUEUEDObject = PASSED_OBJECTS.TryRemove(Submission.Key,out var Result);
                if (QUEUEDObject)
                {
                    Console.WriteLine($"Object Manager: Removed QUEUED object {Result.CreatedObject.GetType().Name} from Queued passed objects.");
                }
                Console.WriteLine($"Object Manager: QUEUED_OBJECTS Added, Source: {typeof(TObject)}");
            }

            return this;
        }

        #endregion
    }
}
