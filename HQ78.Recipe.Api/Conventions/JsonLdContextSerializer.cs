using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Schema.NET;

namespace HQ78.Recipe.Api.Conventions
{
    public class JsonLdContextSerializer : SerializerBase<JsonLdContext>
    {
        public override JsonLdContext Deserialize(
            BsonDeserializationContext context,
            BsonDeserializationArgs args
        )
        {
            if (context.Reader.GetCurrentBsonType() == BsonType.String)
            {
                return new JsonLdContext
                {
                    Name = context.Reader.ReadString(),
                    Language = "en-US"
                };
            }
            else
            {
                return base.Deserialize(context, args);
            }
        }

        public override void Serialize(
            BsonSerializationContext context,
            BsonSerializationArgs args,
            JsonLdContext value
        )
        {
            // Serialize to a simple string if it's the default context.
            if (value.Name == "http://schema.org")
            {
                context.Writer.WriteString(value.Name);
            }
            else
            {
                base.Serialize(context, args, value);
            }
        }
    }
}
