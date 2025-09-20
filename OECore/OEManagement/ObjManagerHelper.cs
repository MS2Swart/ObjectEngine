using ObjectEngine.objectPipelines.objectManager;

namespace OECore.OEManagement
{
    public class ObjManagerHelper<TObject> : ObjManager<TObject>,IManageObjects<TObject> where TObject : class, new()
    {
        #region MethodChain:CreateObject
        public ObjManagerHelper<TObject> CreateObject()
        {
            Create();
            return this;
        }
        public ObjManagerHelper<TObject> CreateObject(Guid Id)
        {
            Create(Id);
            return this;
        }
        #endregion

        #region MethodChain:RemoveObject
        public ObjManagerHelper<TObject> RemoveObject(Guid Id) 
        {
            Remove(Id);
            return this;
        }

        #endregion

        #region MethodChain:UpdateObject

        public ObjManagerHelper<TObject> UpdateObject(Guid Id,object TargetObject)
        {
            Update(Id, TargetObject);
            return this;
        }

        #endregion

        #region MethodChain:ClearObject
        public ObjManagerHelper<TObject> ClearObject() 
        {
            Clear();
            return this;
        }
        #endregion

        #region MethodChain:SubmitObject
        public ObjManagerHelper<TObject> SubmitObject()
        { 
            Submit();
            return this;
        }
        #endregion
    }
}
