namespace OECore.OEManagement
{
    #region Object Engine Management Helper Contracts

    /// <summary>
    /// ICreateObject Contract: Create an Object Type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface ICreateObject<T> where T : class,new()
    {
        ManagerHelper<T> CreateObject();
    }
    /// <summary>
    /// IRemoveObject: Remove an object type from collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IRemoveObject<T> where T : class, new() 
    {
        ManagerHelper<T> RemoveObject(Guid Id);
    }
    /// <summary>
    /// IClearObject Contract: Clear object collections that contrains object types.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IClearObject<T> where T : class, new() 
    {
        ManagerHelper<T> ClearObject();
    }
    /// <summary>
    /// ISubmitObject Contract:
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface ISubmitObject<T> where T : class, new()
    {
        ManagerHelper<T> SubmitObject();
    }

    #endregion
}
