using System;
using System.Collections.Generic;
using System.Linq;

namespace Diese
{
    static public class TypeExtension
    {
        static public string GetDisplayName(this Type type)
        {
            string result = type.Name;

            if (type.IsGenericType)
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
            foreach (Type interfaceType in type.GetInterfaces())
                yield return interfaceType;

            for (Type baseType = type.BaseType; baseType != null; baseType = baseType.BaseType)
                yield return baseType;
        }

        static public IEnumerable<Type> GetInheritedTypes<TResult>(this Type type)
        {
            return type.GetInheritedTypes(typeof(TResult));
        }

        static public IEnumerable<Type> GetInheritedTypes(this Type type, Type assignableType)
        {
            foreach (Type interfaceType in type.GetInterfaces().Where(assignableType.IsAssignableFrom))
                yield return interfaceType;

            for (Type baseType = type.BaseType; baseType != null && assignableType.IsAssignableFrom(type.BaseType); baseType = baseType.BaseType)
                yield return baseType;
        }
    }
}