using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Diese
{
    static public class TypeExtension
    {
        static public string GetDisplayName(this Type type)
        {
            string result = type.Name;

            if (type.GetTypeInfo().IsGenericType)
            {
                result = result.Substring(0, result.Length - 2);
                result += "<";
                result += string.Join(",", type.GenericTypeArguments.Select(GetDisplayName));
                result += ">";
            }

            return result;
        }

        static public IEnumerable<Type> GetInheritedTypes(this Type type)
        {
            foreach (Type interfaceType in type.GetTypeInfo().ImplementedInterfaces)
                yield return interfaceType;

            for (Type baseType = type.GetTypeInfo().BaseType; baseType != null; baseType = baseType.GetTypeInfo().BaseType)
                yield return baseType;
        }

        static public IEnumerable<Type> GetInheritedTypes<TResult>(this Type type)
        {
            return type.GetInheritedTypes(typeof(TResult));
        }

        static public IEnumerable<Type> GetInheritedTypes(this Type type, Type assignableType)
        {
            TypeInfo assignableTypeInfo = assignableType.GetTypeInfo();

            foreach (Type interfaceType in type.GetTypeInfo().ImplementedInterfaces.Where(x => assignableTypeInfo.IsAssignableFrom(x.GetTypeInfo())))
                yield return interfaceType;

            for (Type baseType = type.GetTypeInfo().BaseType; baseType != null && assignableTypeInfo.IsAssignableFrom(type.GetTypeInfo().BaseType.GetTypeInfo()); baseType = baseType.GetTypeInfo().BaseType)
                yield return baseType;
        }
    }
}