using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HQ78.Recipe.Api.Extensions;
using HQ78.Recipe.Api.Helpers;
using Schema.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HQ78.Recipe.Api.GraphQL.Types
{
    public class SchemaOrgTypeDescriptor<T> : ObjectType<T>
    {
        protected override void Configure(IObjectTypeDescriptor<T> descriptor)
        {
            var wrappedType = typeof(T);

            descriptor.Name(wrappedType.Name);

            const BindingFlags bindingFlags = BindingFlags.Instance
                | BindingFlags.Public
                | BindingFlags.DeclaredOnly;

            var members = Enumerable.Union(
                wrappedType.GetFields(bindingFlags).Cast<MemberInfo>(),
                wrappedType.GetProperties(bindingFlags).Cast<MemberInfo>()
            );

            foreach (var member in members)
            {
                var memberType = member.GetMemberType();
                var fieldDescriptor = descriptor.Field(member.Name);

                if (SchemaOrgHelpers.IsUnionType(memberType))
                {
                    var unionTypes = SchemaOrgHelpers.GetUnionMemberTypes(memberType)
                        .Select(
                            t =>
                            {
                                var newTypeCtor = typeof(ObjectType<>)
                                    .MakeGenericType(t)
                                    .GetConstructor(Array.Empty<Type>());

                                if (newTypeCtor is null)
                                    throw new Exception();

                                var objectType = newTypeCtor.Invoke(null) as ObjectType;

                                return objectType;
                            }
                        );

                    var graphQLUnionType = new UnionType(
                        desc =>
                        {
                            desc.Name(member.Name);

                            foreach (var unionType in unionTypes)
                                desc.Type(unionType);
                        }
                    );

                    fieldDescriptor.Type(graphQLUnionType);
                }
                else if (memberType.IsEnum)
                {
                    var newTypeCtor = typeof(EnumType<>)
                        .MakeGenericType(memberType)
                        .GetConstructor(Array.Empty<Type>());

                    if (newTypeCtor is null)
                        throw new Exception();

                    var objectType = newTypeCtor.Invoke(null) as EnumType;

                    if (objectType is null)
                        throw new Exception();

                    fieldDescriptor.Type(objectType);
                }
                else if (
                    GraphQLTypeHelpers.TryGetBuiltInScalarType(
                        memberType,
                        out var scalarType
                    )
                )
                {
                    fieldDescriptor.Type(scalarType);
                }
                else
                {
                    // Is a normal type.
                }
            }
        }
    }
}
