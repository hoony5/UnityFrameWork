using System;
using System.Reflection;

namespace Share
{
    public static class ReflectionEx
    {
        public static void FindPropertyAndBind(this object instance, Type instanceType, object dependencyInstance, Type dependencyType)
        {
            if(instanceType is null) throw new ArgumentNullException(nameof(instanceType));
            PropertyInfo[] properties = instanceType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (PropertyInfo propertyInfo in properties)
            {
                if(propertyInfo.PropertyType != dependencyType) continue;
                propertyInfo.SetValue(instance, dependencyInstance);
            }
        }
        public static void FindFieldAndBind(this object instance, Type serviceType, object dependencyInstance, Type contractType)
        {
            if(serviceType is null) throw new ArgumentNullException(nameof(serviceType));
            FieldInfo[] fieldInfos = serviceType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                if(fieldInfo.FieldType != contractType) continue;
                fieldInfo.SetValue(instance, dependencyInstance);
            }
        }
    }
}