namespace OECore.OEManagement
{
    #region Object Engine Management Helper Contracts

    internal interface IManageObjects<TObject> where TObject : class, new()
    {
        ManagerHelper<TObject> CreateObject();
        ManagerHelper<TObject> RemoveObject(Guid Id);
        ManagerHelper<TObject> ClearObject();
        ManagerHelper<TObject> SubmitObject();
    }

    #endregion
}
