using ObjectEngine.objectPipelines.objectManager;

namespace OECore.OEManagement
{
    public class ManagerHelper<TObject> : ObjManager<TObject>,IManageObjects<TObject> where TObject : class,new()
    {
        #region MethodChain:CreateObject
        public ManagerHelper<TObject> CreateObject()
        {
            Create();
            return this;
        }
        #endregion
        
        #region MethodChain:RemoveObject
        public ManagerHelper<TObject> RemoveObject(Guid Id) 
        {
            Remove(Id);
            return this;
        }

        #endregion

        #region MethodChain:ClearObject
        public ManagerHelper<TObject> ClearObject() 
        {
            Clear();
            return this;
        }
        #endregion

        #region MethodChain:SubmitObject
        public ManagerHelper<TObject> SubmitObject()
        { 
            Submit();
            return this;
        }
        #endregion
    }
}
