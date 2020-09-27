using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace HQ78.Recipe.Api.Helpers
{
    public static class TypeHelpers
    {
        public static MemberInfo GetFieldOrProp<T>(string memberName)
        {
            var type = typeof(T);

            var result = type.GetField(memberName) as MemberInfo
                ?? type.GetProperty(memberName) as MemberInfo
                ?? throw new MissingMemberException();

            return result;
        }

        /// <summary>
        /// Copied from
        /// <a href="https://stackoverflow.com/a/32025393/6907446">stack overflow</a>.
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static bool IsTypeImplicitlyConvertable(
            Type baseType,
            Type targetType
        )
        {
            return baseType.GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(mi => mi.Name == "op_Implicit" && mi.ReturnType == targetType)
                .Any(
                    mi =>
                    {
                        var firstParameter = mi.GetParameters().FirstOrDefault();

                        return firstParameter?.ParameterType == baseType;
                    }
                );
        }

        public static bool IsTypeExplicitlyConvertable(
            Type baseType,
            Type targetType
        )
        {
            return baseType.GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(mi => mi.Name == "op_Explicit" && mi.ReturnType == targetType)
                .Any(
                    mi =>
                    {
                        var firstParameter = mi.GetParameters().FirstOrDefault();

                        return firstParameter?.ParameterType == baseType;
                    }
                );
        }

        public static bool TryCreateTypeWithCtor<T>(
            object[] ctorArgs,
            out T constructedObject)
        {
            bool hadError = false;

            try
            {
                constructedObject = (T)Activator.CreateInstance(typeof(T), ctorArgs);
            }
            catch (MissingMethodException)
            {
                constructedObject = default;
                hadError = true;
            }

            return !hadError;
        }
    }
}
