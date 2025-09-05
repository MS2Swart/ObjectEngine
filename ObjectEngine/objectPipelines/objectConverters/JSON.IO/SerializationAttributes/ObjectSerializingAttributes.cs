namespace ObjectEngine.objectPipelines.objectConverters.JSON.IO.SerializationAttributes
{
    /// <summary>
    /// Specifies that a class is eligible for object serialization and defines the serialization format to be used.
    /// </summary>
    /// <remarks>This attribute can be applied to classes to indicate that they support serialization.  The
    /// serialization format can be specified using the <see cref="SerializationFomat"/> enumeration.</remarks>
    /// <param name="serializationFomat"></param>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false)]
    public sealed class ObjectSerializeAttribute : Attribute{}
}
