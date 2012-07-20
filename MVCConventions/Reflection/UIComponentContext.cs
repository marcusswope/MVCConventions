using System;
using MVCConventions.Infrastructure;

namespace MVCConventions.Reflection
{
    public class UIComponentContext
    {
        private readonly string _prefix;
        public Accessor Accessor { get; private set; }
        public object Model { get; private set; }
        public Type ModelType { get { return Model.GetType(); } }
        public object RawValue { get { return Accessor.GetValue(Model); } }
        public string MVC3TagId { get { return joinProperties('_'); } }
        public string MVC3TagName { get { return joinProperties('.'); } }

        private string joinProperties(char joinValue)
        {
            var id = string.IsNullOrEmpty(_prefix) ? string.Empty : _prefix + joinValue;
            foreach (var propertyName in Accessor.PropertyNames)
            {
                if (propertyName.StartsWith("[") && propertyName.EndsWith("]"))
                {
                    id = id.TrimEnd(joinValue);
                }
                id += propertyName;
                id += joinValue;
            }
            return id.TrimEnd(joinValue);
        }

        public Type PropertyType
        {
            get { return Accessor.InnerProperty.PropertyType; }
        }

        public UIComponentContext(Accessor accessor, object model, string prefix)
        {
            _prefix = prefix;
            Accessor = accessor;
            Model = model;
        }

        public T GetModel<T>()
            where T : class
        {
            return Model as T;
        }

        public T Value<T>()
        {
            return (T)RawValue;
        }

        public T PropertyAttribute<T>()
            where T : Attribute
        {
            return Accessor.InnerProperty.GetCustomAttributes(typeof(T), true)[0] as T;
        }
    }
}