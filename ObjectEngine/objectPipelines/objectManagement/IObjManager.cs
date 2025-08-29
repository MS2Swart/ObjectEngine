using ObjectEngine.objectPipelines.objectConverters.JSON.IO;

namespace ObjectEngine.objectPipelines.objectManager
{
    /// <summary>
    /// IMange Method Contract: Manage System Object instance\instances
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IManage<TObject> where TObject : class, new() 
    {
        ObjManager<TObject> Submit();
        ObjManager<TObject> Remove(Guid ObjectID);
        ObjManager<TObject> Clear(ObjectSerializer<TObject>? Serializer = null);

        #region InitialCreationController

        /// <summary>
        /// Enumerates and yield returns created object type
        /// </summary>
        /// <remarks>Enumerator used for personal engine design prefrence.</remarks>
        /// <param name="createdObject"></param>
        /// <returns>IEenumerator<T></returns>
        private protected static IEnumerator<object> InitialCreationController(object createdObject)
        {
            yield return createdObject;
        }
        #endregion

        #region InitialCreationRunner
        /// <summary>
        /// Enumerable and yield current Constroller object.
        /// </summary>
        /// <remarks>Enumerable userd for personal engine design prefrence</remarks>
        /// <param name="createdObject"></param>
        /// <returns>IEnumerable<object></returns>
        private protected static IEnumerable<object> InitialCreationRunner(object createdObject)
        {
            using (var Controller = InitialCreationController(createdObject))
            {
                while (Controller.MoveNext())
                {
                    if (Controller.Current is not null)
                    {
                        yield return Controller.Current;
                    }
                }
            }
            yield break;
        }

        #endregion
    }

}
