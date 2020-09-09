using HotChocolate.Language;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HQ78.Recipe.Api.Helpers
{
    public delegate bool TryGetTypeDescriptorDel(Type type, out IType typeDescriptor);
    public delegate bool TryGetValueDelegateDel<T>(Type type, out T value);
    public delegate T PlaceHolderGeneratorDel<T>(Type type, out bool isCompleted);

    public class TypeDescriptorHelper
    {
        #region Fields

        private readonly INamingConventions namingConventions;
        private readonly ITypeInspector typeInspector;

        private readonly DefaultTypeRegistrar<IType> typeRegistrar;
        private readonly XmlDocumentationProvider xmlDocumentationProvider;

        #endregion

        #region Constructor

        public TypeDescriptorHelper(
            INamingConventions namingConventions,
            ITypeInspector typeInspector
        )
        {
            this.namingConventions = namingConventions;
            this.typeInspector = typeInspector;

            typeRegistrar = new DefaultTypeRegistrar<IType>(
                placeholderGenerator: MockITypeGenerator,
                interceptor: TryGetScalarType
            );

            xmlDocumentationProvider = new XmlDocumentationProvider(
                new XmlDocumentationFileResolver()
            );
        }

        #endregion

        #region Methods

        public IEnumerable<IType> GenerateTypeDescriptors<T>() where T : class
        {
            var rootType = typeof(T);

            var graphQLType = GraphQLTypeHelpers.GetGraphQLType(
                rootType,
                out _
            );

            IType graphQLRootType;
            var rootTypeName = namingConventions.GetTypeName(rootType);
            var flags = BindingFlags.Public | BindingFlags.Instance;

            var members = Enumerable.Concat<MemberInfo>(
                rootType.GetFields(flags),
                rootType.GetProperties(flags)
            );

            switch (graphQLType)
            {
                case TypeKind.Object:
                    var ctor = typeof(ObjectType<>)
                        .MakeGenericType(rootType)
                        .GetConstructor(new Type[] { typeof(Action<>).MakeGenericType(IObjectTypeDescriptor<>) })
                        ?? throw new Exception();

                    graphQLRootType = new ObjectType(
                        desc =>
                        {
                            desc.Name(rootTypeName);

                            foreach (var member in members)
                            {
                                
                            }
                        }
                    );

                    break;
                case TypeKind.Interface:
                    graphQLRootType = new InterfaceType(
                        desc => desc.Name(rootTypeName)
                    );

                    break;
                default:
                    throw new NotImplementedException();
            }

            throw new NotImplementedException();
        }

        private void ConfigureObjectDescriptor(IObjectTypeDescriptor descriptor)
        {

        }

        private IType MockITypeGenerator(Type type, out bool isCompleted)
        {
            if (GraphQLTypeHelpers.IsBuiltInScalarType(type))
                throw new Exception();

            isCompleted = false;
            IType result;

            var graphQLTypeKind = GraphQLTypeHelpers.GetGraphQLType(
                type,
                out var isNullable
            );

            var typeName = namingConventions.GetTypeName(type);

            switch (graphQLTypeKind)
            {
                case TypeKind.Object:
                    result = new ObjectType(
                        desc => desc.Name(typeName)
                    );

                    break;
                case TypeKind.Interface:
                    result = new InterfaceType(
                        desc => desc.Name(typeName)
                    );

                    break;
                case TypeKind.Enum:
                    result = typeof(EnumType<>)
                        .MakeGenericType(type)
                        .GetConstructor(Array.Empty<Type>())
                        ?.Invoke(Array.Empty<object>()) as EnumType
                        ?? throw new Exception();

                    isCompleted = true;

                    break;

                case TypeKind.List:
                    var memberType = GraphQLTypeHelpers.GetListMemberType(type);

                    if (!typeRegistrar.TryGetValue(memberType, out var memberIType))
                        throw new Exception();

                    result = new ListType(memberIType);
                    break;

                default:
                    throw new NotImplementedException();
            }

            return result;
        }

        private bool TryGetScalarType(
            Type type,
            [MaybeNullWhen(false)] out IType primitiveType
        )
        {
            var isNullable = false;
            var underlyingType = Nullable.GetUnderlyingType(type);

            if (underlyingType is object)
                isNullable = true;
            else
                underlyingType = type;

            if (GraphQLTypeHelpers.TryGetBuiltInScalarType(underlyingType, out var scalarType))
            {
                primitiveType = isNullable
                    ? (IType)scalarType
                    : new NonNullType(scalarType);

                return true;
            }
            else
            {
                primitiveType = null;

                return false;
            }
        }

        private void ITypeCompletor(Type mappedType, IType graphQLType)
        {
            switch (graphQLType.Kind)
            {
                case TypeKind.Interface:
                    InterfaceCompletor(mappedType, (InterfaceType)graphQLType);
                    break;
                case TypeKind.List:
                    ListCompletor(mappedType, (ListType)graphQLType);
                    break;
                case TypeKind.Union:
                    UnionCompletor(mappedType, (UnionType)graphQLType);
                    break;
                case TypeKind.Object:
                    ObjectCompletor(mappedType, (ObjectType)graphQLType);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void EnumCompletor(Type enumType, EnumType graphQLType)
        {
            throw new NotImplementedException();
        }

        private void InterfaceCompletor(Type interfaceType, InterfaceType graphQLType)
        {
            throw new NotImplementedException();
        }

        private void ListCompletor(Type mappedType, ListType graphQLType)
        {
            throw new NotImplementedException();
        }

        private void UnionCompletor(Type mappedType, UnionType graphQLType)
        {
            throw new NotImplementedException();
        }

        private void ObjectCompletor(Type mappedType, ObjectType graphQLType)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
