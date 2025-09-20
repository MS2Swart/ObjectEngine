using ObjectEngine.objectPipelines.objectManager;
using OECore.OEManagement;

namespace OECore.ClassManager
{
    public class ClassManager
    {
        public static ObjManager<TObject> Build<TObject>() where TObject : ObjManagerHelper<TObject>, new()
        {
            var Instance = new ObjManager<TObject>();
            return Instance;
        }
        public static ObjManagerHelper<TObject> Create<TObject>(ObjManagerHelper<TObject> objManagerHelper) where TObject : ObjManagerHelper<TObject>, new()
        {

            var Instance = objManagerHelper.CreateObject();
            return Instance;
        }
        public static ObjManagerHelper<TObject> Create<TObject>(ObjManagerHelper<TObject> objManagerHelper, Guid Id) where TObject : ObjManagerHelper<TObject>, new()
        {
            var Instance = objManagerHelper.CreateObject(Id);
            return Instance;
        }
        public static ObjManagerHelper<TObject> Remove<TObject>(ObjManagerHelper<TObject> objManagerHelper, Guid Id) where TObject : ObjManagerHelper<TObject>, new()
        {
            var Instance = objManagerHelper.RemoveObject(Id);
            return Instance;
        }
        public static ObjManagerHelper<TObject> Update<TObject>(ObjManagerHelper<TObject> objManagerHelper, Guid Id,object TargetObject) where TObject : ObjManagerHelper<TObject>, new()
        {
            var Instance = objManagerHelper.UpdateObject(Id, TargetObject);
            return Instance;
        }
        public static ObjManagerHelper<TObject> Clear<TObject>(ObjManagerHelper<TObject> objManagerHelper) where TObject : ObjManagerHelper<TObject>, new()
        {
            var Instance = objManagerHelper.ClearObject();
            return Instance;
        }
        public static ObjManagerHelper<TObject> Submit<TObject>(ObjManagerHelper<TObject> objManagerHelper) where TObject : ObjManagerHelper<TObject>, new()
        {
            var Instance = objManagerHelper.SubmitObject();
            return Instance;
        }
    }
}
