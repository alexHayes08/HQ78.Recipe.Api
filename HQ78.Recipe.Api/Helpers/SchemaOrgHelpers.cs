using Schema.NET;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HQ78.Recipe.Api.Helpers
{
    public static class SchemaOrgHelpers
    {
        public static bool IsUnionType(Type type)
        {
            return type.IsGenericType
                && (
                    type.Name.StartsWith("Values")
                    && type.Name.StartsWith("OneOrMany")
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetUnionMemberTypes(Type type)
        {
            return type.GetGenericArguments();
        }

        public static Type GetGenericValuesType(int genericArgsCount)
        {
            return genericArgsCount switch
            {
                2 => typeof(Values<,>),
                3 => typeof(Values<,,>),
                4 => typeof(Values<,,,>),
                7 => typeof(Values<,,,,,,>),
                _ => throw new Exception()
            };
        }
    }
}
