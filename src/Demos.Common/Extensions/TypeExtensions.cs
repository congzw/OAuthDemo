using System;
using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Demos.Common
{
    public static class TypeExtensions
    {
        public static string GetFriendlyNameWithNamespace(this Type type)
        {
            var friendlyName = GetTypeName(type);
            return string.Format("{0}.{1}", type.Namespace, friendlyName);
        }

        public static string GetFriendlyName(this Type type)
        {
            var friendlyName = GetTypeName(type);
            return friendlyName;
        }

        public static string GetFriendlyFileName(this Type type)
        {
            var friendlyName = GetTypeName(type);
            return friendlyName.Replace("<", "[").Replace(">", "]");
        }

        public static string GetFriendlyNameForMethod(this MethodInfo methodInfo)
        {
            string friendlyName = methodInfo.Name;
            if (methodInfo.IsGenericMethod)
            {
                int iBacktick = friendlyName.IndexOf('`');
                if (iBacktick > 0)
                {
                    friendlyName = friendlyName.Remove(iBacktick);
                }
                friendlyName += "<";
                Type[] typeParameters = methodInfo.GetGenericArguments();
                for (int i = 0; i < typeParameters.Length; ++i)
                {
                    //string typeParamName = typeParameters[i].Name;
                    //friendlyName += (i == 0 ? typeParamName : "," + typeParamName);
                    var subType = typeParameters[i];
                    var subTypeName = GetFriendlyName(subType);
                    friendlyName += (i == 0 ? subTypeName : "," + subTypeName);
                }
                friendlyName += ">";
            }

            return friendlyName;
        }

        public static string GuessProjectPrefix(this Type type)
        {
            var prefix = "";
            var ns = type.Namespace;
            if (ns != null)
            {
                var result = ns.Split('.').FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(result))
                {
                    prefix = result;
                }
            }
            return prefix;
        }

        //----helpers----
        private static string GetTypeName(Type type)
        {
            if (type.IsGenericType)
            {
                return GetGenericTypeName(type);
            }
            return GetSimpleTypeName(type);
        }
        private static string GetSimpleTypeName(Type simpleType)
        {
            if (simpleType.IsGenericType)
            {
                throw new InvalidOperationException(simpleType + " Is Not A Simple Type.");
            }

            var namespacePrex = simpleType.Namespace + ".";
            var simpleClassNameTemplate = namespacePrex == "." ? simpleType.ToString() : simpleType.ToString().Replace(namespacePrex, "");
            return simpleClassNameTemplate;
        }
        private static string GetGenericTypeName(Type genericType)
        {
            if (!genericType.IsGenericType)
            {
                throw new InvalidOperationException(genericType + " Is Not A Generic Type.");
            }
            //var type = typeof(HelloWorld<int>.MyClass.HelloWorld2<string, object>);
            //ZQNB.Common.Extensions.HelloWorld`1+MyClass+HelloWorld2`2[System.Int32,System.String,System.Object]
            var namespacePrex = genericType.Namespace + ".";

            var genericClassNameTemplate = namespacePrex == "." ? genericType.ToString() : genericType.ToString().Replace(namespacePrex, "");
            var genericArguments = genericType.GetGenericArguments().ToList();

            int proecssedGenericIndex = 0;
            int indexOfGeneric = genericClassNameTemplate.IndexOf('`');
            while (indexOfGeneric > 0)
            {
                var currentGenericCount = Convert.ToInt32(genericClassNameTemplate[(indexOfGeneric + 1)].ToString());
                genericClassNameTemplate = genericClassNameTemplate.Remove(indexOfGeneric, 2);

                var currentGenericArguments = genericArguments.Skip(proecssedGenericIndex).Take(currentGenericCount).ToList();
                proecssedGenericIndex += currentGenericCount;

                var subTypeNames = currentGenericArguments.Select(GetTypeName).ToList();
                var subTypeName = "<" + string.Join(",", subTypeNames) + ">";
                genericClassNameTemplate = genericClassNameTemplate.Insert(indexOfGeneric, subTypeName);

                indexOfGeneric = genericClassNameTemplate.IndexOf('`');
            }

            int indexOfGenericParamsLast = genericClassNameTemplate.IndexOf('[');
            if (indexOfGenericParamsLast > 0)
            {
                genericClassNameTemplate = genericClassNameTemplate.Substring(0, indexOfGenericParamsLast);
            }

            return genericClassNameTemplate;

        }
    }
}
