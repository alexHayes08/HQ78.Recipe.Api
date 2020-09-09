using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace HQ78.Recipe.Api.Extensions
{
    public static class MemberInfoExtensions
    {
        public static Type GetMemberType(this MemberInfo memberInfo)
        {
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)memberInfo).FieldType;
                case MemberTypes.Property:
                    return ((PropertyInfo)memberInfo).PropertyType;
                case MemberTypes.TypeInfo:
                    return (Type)memberInfo;
                default:
                    throw new InvalidCastException("Member type must be a field, property, or type info.");
            }
        }

        public static bool IsMemberNullable(this MemberInfo memberInfo)
        {
#nullable disable

            if (memberInfo is null)
                throw new ArgumentNullException(nameof(memberInfo));

            var memberValueType = memberInfo.GetMemberType();

            // First check if the memberValueType is a Nullable<T>.
            if (Nullable.GetUnderlyingType(memberValueType) is object)
                return true;

            var nullable = memberInfo.CustomAttributes.FirstOrDefault(
                x => x.AttributeType.FullName == "System.Runtime.CompilerServices.NullableAttribute"
            );

            var ctorArgCount = nullable?.ConstructorArguments.Count ?? 0;

            if (ctorArgCount == 1)
            {
                var attributeArgument = nullable.ConstructorArguments[0];

                if (attributeArgument.ArgumentType == typeof(byte[]))
                {
                    var args = attributeArgument.Value as ReadOnlyCollection<CustomAttributeTypedArgument>;

                    if (args is null)
                        throw new InvalidCastException();

                    if (args.Count > 0 && args[0].ArgumentType == typeof(byte))
                    {
                        return (byte)args[0].Value == 2;
                    }
                }
                else if (attributeArgument.ArgumentType == typeof(byte))
                {
                    return (byte)attributeArgument.Value == 2;
                }
            }

            var context = memberValueType.DeclaringType?.CustomAttributes.FirstOrDefault(
                x => x.AttributeType.FullName == "System.Runtime.CompilerServices.NullableContextAttribute"
            );

            if (context != null
                && context.ConstructorArguments.Count == 1
                && context.ConstructorArguments[0].ArgumentType == typeof(byte)
            )
            {
                return (byte)context.ConstructorArguments[0].Value == 2;
            }

            // Couldn't find a suitable attribute
            return false;

#nullable enable
        }
    }
}
