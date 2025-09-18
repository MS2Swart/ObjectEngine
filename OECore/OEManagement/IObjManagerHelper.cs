using ObjectEngine.objectPipelines.objectManager;

namespace OECore.OEManagement
{
    #region Object Engine Management Helper Contracts

    internal interface IManageObjects<TObject> where TObject : class, new()
    {
        ObjManagerHelper<TObject> CreateObject();
        ObjManagerHelper<TObject> RemoveObject(Guid Id);
        ObjManagerHelper<TObject> ClearObject();
        ObjManagerHelper<TObject> SubmitObject();
    }

    #endregion
}
