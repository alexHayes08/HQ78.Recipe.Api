using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Schema.NET;
using System.Linq;

namespace HQ78.Recipe.Api.Conventions
{
    public class OneOrManySerializer<T> : SerializerBase<OneOrMany<T>>
    {
        public override void Serialize(
            BsonSerializationContext context,
            BsonSerializationArgs args,
            OneOrMany<T> value
        )
        {
            // If it's only a single value, just use the default serializer of
            // that type else use an array serializer.
            if (value.HasMany)
            {
                base.Serialize(context, args, value.ToArray());
            }
            else
            {
                base.Serialize(context, args, value.First());
            }
        }

        public override OneOrMany<T> Deserialize(
            BsonDeserializationContext context,
            BsonDeserializationArgs args
        )
        {
            // Check if it's an array.
            if (context.Reader.GetCurrentBsonType() == BsonType.Array)
            {
                var value = (T[])BsonSerializer.Deserialize(
                    context.Reader,
                    typeof(T[])
                );

                return new OneOrMany<T>(value);
            }
            else
            {
                var value = (T)BsonSerializer.Deserialize(
                    context.Reader,
                    typeof(T)
                );

                return new OneOrMany<T>(value);
            }
        }
    }
}
