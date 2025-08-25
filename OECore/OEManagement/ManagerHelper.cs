using ObjectEngine.objectPipelines.objectManager;

namespace OECore.OEManagement
{
    public class ManagerHelper<T> : ObjManager<T>,ICreateObject<T> ,IRemoveObject<T>,IClearObject<T>,ISubmitObject<T> where T : class,new()
    {
        #region MethodChain:CreateObject
        public ManagerHelper<T> CreateObject()
        {
            Create();
            return this;
        }
        #endregion
        
        #region MethodChain:RemoveObject
        public ManagerHelper<T> RemoveObject(Guid Id) 
        {
            Remove(Id);
            return this;
        }

        #endregion

        #region MethodChain:ClearObject
        public ManagerHelper<T> ClearObject() 
        {
            Clear();
            return this;
        }
        #endregion

        #region MethodChain:SubmitObject
        public ManagerHelper<T> SubmitObject()
        { 
            Submit();
            return this;
        }
        #endregion
    }
}
