using MongoDB.Bson.Serialization;
using Schema.NET;
using System;
using System.Linq;

namespace HQ78.Recipe.Api.Conventions
{
    public class OneOrManySerializerProvider : IBsonSerializationProvider
    {
        public IBsonSerializer? GetSerializer(Type type)
        {
            var serializer = default(IBsonSerializer);

            if (!type.IsGenericType)
                return serializer;

            var openGenericType = type.IsConstructedGenericType
                ? type.GetGenericTypeDefinition()
                : type;

            if (openGenericType == typeof(OneOrMany<>))
            {
                var genericArg = type.GetGenericArguments().First();

                var closedSerializerType = typeof(OneOrManySerializer<>)
                    .MakeGenericType(genericArg);

                serializer = Activator.CreateInstance(closedSerializerType) as IBsonSerializer;
            }

            return serializer;
        }
    }
}
