namespace ObjectEngine.objectFactory
{
    public interface ICreate
    {
        /// <summary>
        /// Creates a instance of typeof(T)
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <returns>new T();//instance</returns>
        private protected static Type SystemType<TObject>() where TObject : class,new()
        {
            return typeof(TObject);
        }
        /// <summary>
        /// Creates an instance of object
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>object</returns>
        /// <exception cref="InvalidOperationException">Could not create type instance into an object instance.</exception>
        private protected static object SystemObject<TObject>(Type instance) where TObject : class,new()
        {
            var Result = Activator.CreateInstance(instance) ?? throw new InvalidOperationException($"Could not create {instance.Name} into an object instance.");
            object ObjectType = Result;
            return ObjectType;
        }
    }
}
