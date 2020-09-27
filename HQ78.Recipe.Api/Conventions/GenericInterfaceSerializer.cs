using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Linq.Expressions;

namespace HQ78.Recipe.Api.Conventions
{
    public class GenericInterfaceSerializer<TInterface, TClass> : SerializerBase<TInterface>
        where TInterface : class
        where TClass : class, TInterface, new()
    {
        private readonly IBsonSerializer? nestedSerializer;

        public GenericInterfaceSerializer()
        { }

        public GenericInterfaceSerializer(
            Expression<Func<TClass, string>> fieldOrPropAccessor
        )
        {
            nestedSerializer = new GenericJsonLinkSerializer<TClass>(
                fieldOrPropAccessor
            );
        }

        public override TInterface Deserialize(
            BsonDeserializationContext context,
            BsonDeserializationArgs args
        )
        {
            if (nestedSerializer is object)
            {
                var result = nestedSerializer.Deserialize(context, args) as TInterface;

                return result is object
                    ? result
                    : throw new InvalidCastException();
            }
            else
            {
                return (TInterface)BsonSerializer.Deserialize(
                    context.Reader,
                    typeof(TClass)
                );
            }
        }
    }
}
