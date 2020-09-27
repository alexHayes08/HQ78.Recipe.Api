using HQ78.Recipe.Api.Conventions;
using MongoDB.Bson.Serialization;
using System;
using System.Linq.Expressions;

namespace HQ78.Recipe.Api.Helpers
{
    public class MongoDbHelper
    {
        private MongoDbHelper()
        { }

        public static MongoDbHelper Default { get; } = new MongoDbHelper();

        public MongoDbHelper MapInterfaceToImplementation<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface, new()
        {
            BsonSerializer.RegisterSerializer(
                new GenericInterfaceSerializer<TInterface, TClass>()
            );

            return Default;
        }

        public MongoDbHelper MapInterfaceToDefaultImplementation<TInterface, TClass>(
            Expression<Func<TClass, string>> fieldOrPropAccessor
        )
        {
            var interfaceType = typeof(TInterface);
            var implementationType = typeof(TClass);

            var serializer = CreateSerializer<TInterface, TClass>(
                fieldOrPropAccessor
            );

            BsonSerializer.RegisterSerializer(interfaceType, serializer);

            return Default;
        }

        public MongoDbHelper MapInterfaceToDefaultImplementation<T>()
        {
            var interfaceType = typeof(T);
            var implementationType = GetImpliedImplentationType(interfaceType);
            var serializer = CreateSerializer<T>(implementationType);

            BsonSerializer.RegisterSerializer(interfaceType, serializer);

            return Default;
        }

        private IBsonSerializer CreateSerializer<TInterface, TClass>(
            Expression<Func<TClass, string>>? fieldOrPropAccessor = null
        )
        {
            IBsonSerializer? serializer;

            var closedGenericType = typeof(GenericInterfaceSerializer<,>)
                .MakeGenericType(typeof(TInterface), typeof(TClass));

            if (fieldOrPropAccessor is object)
            {
                serializer = Activator.CreateInstance(
                    closedGenericType,
                    fieldOrPropAccessor
                ) as IBsonSerializer;
            }
            else
            {
                serializer = Activator.CreateInstance(closedGenericType) as IBsonSerializer;
            }

            if (serializer is null)
                throw new Exception();

            return serializer;
        }

        private IBsonSerializer CreateSerializer<T>(
            Type implementationType
        )
        {
            IBsonSerializer? serializer;

            var closedGenericType = typeof(GenericInterfaceSerializer<,>)
                .MakeGenericType(typeof(T), implementationType);

            serializer = Activator.CreateInstance(closedGenericType) as IBsonSerializer;

            if (serializer is null)
                throw new Exception();

            return serializer;
        }

        private Type GetImpliedImplentationType(Type interfaceType)
        {
            var implementationName = interfaceType.Namespace
                + "."
                + interfaceType.Name.Substring(1);

            var implementationType = interfaceType
                .Assembly
                .GetType(implementationName);

            if (implementationType is null)
                throw new TypeLoadException(implementationName);

            return implementationType;
        }
    }
}
