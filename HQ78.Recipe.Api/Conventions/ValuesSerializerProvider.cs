using MongoDB.Bson.Serialization;
using Schema.NET;
using System;
using System.Collections.Generic;

namespace HQ78.Recipe.Api.Conventions
{
    public class ValuesSerializerProvider : IBsonSerializationProvider
    {
        private readonly Type values2SerializerType;
        private readonly Type values3SerializerType;
        private readonly Type values4SerializerType;
        private readonly Type values7SerializerType;
        private readonly Dictionary<Type, IBsonSerializer> mappedSerializers;

        public ValuesSerializerProvider()
        {
            values2SerializerType = typeof(ValuesSerializer<,>);
            values3SerializerType = typeof(ValuesSerializer<,,>);
            values4SerializerType = typeof(ValuesSerializer<,,,>);
            values7SerializerType = typeof(ValuesSerializer<,,,,,,>);
            mappedSerializers = new Dictionary<Type, IBsonSerializer>();
        }

        public IBsonSerializer? GetSerializer(Type type)
        {
            if (type.IsGenericType)
            {
                var genericType = type.IsConstructedGenericType
                    ? type.GetGenericTypeDefinition()
                    : type;

                var isValuesType = genericType == values2SerializerType
                    || genericType == values3SerializerType
                    || genericType == values4SerializerType
                    || genericType == values7SerializerType;

                if (!isValuesType)
                    return null;

                var genericArgs = type.GetGenericArguments();

                Type constructedType = genericArgs.Length switch
                {
                    2 => values2SerializerType.MakeGenericType(genericArgs),
                    3 => values3SerializerType.MakeGenericType(genericArgs),
                    4 => values4SerializerType.MakeGenericType(genericArgs),
                    7 => values7SerializerType.MakeGenericType(genericArgs),
                    _ => throw new NotImplementedException(),
                };

                return GetOrCreateSerializer(constructedType);
            }
            else
            {
                return null;
            }
        }

        private IBsonSerializer GetOrCreateSerializer(Type closedGenericType)
        {
            if (mappedSerializers.TryGetValue(closedGenericType, out var serializer))
            {
                return serializer;
            }
            else
            {
                serializer = Activator.CreateInstance(closedGenericType) as IBsonSerializer;

                if (serializer is null)
                    throw new Exception();

                mappedSerializers.Add(closedGenericType, serializer);

                return serializer;
            }
        }
    }
}
