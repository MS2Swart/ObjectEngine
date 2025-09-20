using ObjectEngine.objectPipelines.objectManager;
using OECore.OESerialization;

namespace OECore.Serialzation
{
    public static class Serializer
    {

        public static SerializationHelper<TObject> Build<TObject>(ObjManager<TObject>? objManager = null) where TObject : SerializationHelper<TObject>, new()
        {
            return new SerializationHelper<TObject>(objManager);
        }

        public static SerializationHelper<TObject> Serialize<TObject>(SerializationHelper<TObject> serializationHelper) where TObject : SerializationHelper<TObject>, new()
        {
            var Serializer = serializationHelper;
            Serializer.SerializeObject(out var _)
                      .SubmitSerializableObject();
            return Serializer;
        }
        public static SerializationHelper<TObject> Serialize<TObject>(SerializationHelper<TObject> serializationHelper, Guid Id,object TargetObject) where TObject : SerializationHelper<TObject>, new()
        {
            var Serializer = serializationHelper;
            Serializer.SerializeObject(Id, TargetObject, out var _)
                      .SubmitSerializableObject();
            return Serializer;
        }
        public static SerializationHelper<TObject> Deserizalize<TObject>(SerializationHelper<TObject> serializationHelper) where TObject : SerializationHelper<TObject>, new()
        {
            var Serializer = serializationHelper;
            Serializer.DeserializeObject(out var _)
                      .SubmitDeserializableObject();
            return Serializer;
        }
        public static SerializationHelper<TObject> Deserizalize<TObject>(SerializationHelper<TObject> serializationHelper,Guid Id, string SerializedObject) where TObject : SerializationHelper<TObject>, new()
        {
            var Serializer = serializationHelper;
            Serializer.DeserializeObject(Id, SerializedObject, out var _)
                      .SubmitDeserializableObject();
            return Serializer;
        }
        public static SerializationHelper<TObject> Clear<TObject>(SerializationHelper<TObject> serializationHelper) where TObject : SerializationHelper<TObject>, new()
        {
            var Serializer = serializationHelper;
            Serializer.ClearSerializableObjects();
            return Serializer;
        }
    }
}
