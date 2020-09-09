using HotChocolate.Types;
using HQ78.Recipe.Api.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HQ78.Recipe.Api.Helpers
{
    public static class GraphQLTypeHelpers
    {
        /// <summary>
        /// Returns the scalar type for the strings, decimals, uris, etc...
        /// </summary>
        /// <param name="type"></param>
        /// <param name="scalarType"></param>
        /// <returns></returns>
        public static bool TryGetBuiltInScalarType(
            Type type,
            [NotNullWhen(true)] out ScalarType? scalarType
        )
        {
            if (type == typeof(string))
                scalarType = new StringType();
            else if (type == typeof(decimal))
                scalarType = new DecimalType();
            else if (type == typeof(int))
                scalarType = new IntType();
            else if (type == typeof(bool))
                scalarType = new BooleanType();
            else if (type == typeof(float))
                scalarType = new FloatType();
            else if (type == typeof(Guid))
                scalarType = new UuidType();
            else if (type == typeof(DateTime))
                scalarType = new DateTimeType();
            else if (type == typeof(byte))
                scalarType = new ByteType();
            else if (type == typeof(Uri))
                scalarType = new UrlType();
            else if (type == typeof(long))
                scalarType = new LongType();
            else if (type == typeof(short))
                scalarType = new ShortType();
            else
                scalarType = null;

            return scalarType is object;
        }

        /// <summary>
        /// Returns the scalar type for the strings, decimals, uris, etc...
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ScalarType GetBuiltInPrimitiveType(Type type)
        {
            if (TryGetBuiltInScalarType(type, out var scalarType))
                return scalarType;
            else
                throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if the type has a built in primitive type with GraphQL.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBuiltInScalarType(Type type)
        {
            // Make sure we unwrap all Nullable<> types.
            type = Nullable.GetUnderlyingType(type) ?? type;

            return !type.IsEnum && (
                type.IsPrimitive
                || typeof(Uri) == type
                || typeof(DateTime) == type
                || typeof(string) == type
                || typeof(decimal) == type
            );
        }

        public static bool IsBuiltInDictionaryType(Type type)
        {
            if (!type.IsGenericType)
                return false;

            var genericTypeDef = type.IsConstructedGenericType
                ? type.GetGenericTypeDefinition()
                : type;

            // TODO: Fix this.
            return genericTypeDef.GetInterfaces().Any(
                i => typeof(IDictionary<,>).IsAssignableFrom(i)
            );

            throw new NotImplementedException();
        }

        public static TypeKind GetGraphQLType(
            MemberInfo memberInfo,
            out bool isNullable
        )
        {
            isNullable = memberInfo.IsMemberNullable();
            var underlyingType = memberInfo.GetMemberType();
            var result = TypeKind.Object;

            // Check if it's a primitive type.
            if (IsBuiltInScalarType(underlyingType))
            {
                result = TypeKind.Scalar;
            }
            // Check if it's a list type.
            else if (IsBuiltInListType(underlyingType))
            {
                result = TypeKind.List;
            }
            else if (IsBuiltInUnionType(underlyingType))
            {
                result = TypeKind.Union;
            }
            else if (IsEnumType(underlyingType))
            {
                result = TypeKind.Enum;
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInterfaceType(Type type)
        {
            return type.IsInterface;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEnumType(Type type)
        {
            return type.IsEnum;
        }

        public static bool IsBuiltInUnionType(Type type)
        {
            throw new NotImplementedException();
        }

        public static bool IsBuiltInListType(Type type)
        {
            if (type.IsArray)
            {
                return true;
            }
            else if (type.IsGenericType)
            {
                if (type.IsConstructedGenericType)
                    throw new ArgumentException();

                // Just need to verify the type has a GetEnumerator() method on
                // it. This will return true for dictionaries but that's ok
                // since GraphQL does represent dictionaries as lists.
                return type.GetMethod(nameof(IEnumerable.GetEnumerator)) is object;
            }
            else
            {
                return false;
            }
        }

        public static Type GetListMemberType(Type type)
        {
            // TODO: Handle dictionaries.

            throw new NotImplementedException();
        }
    }
}
