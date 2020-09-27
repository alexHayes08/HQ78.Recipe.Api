using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Schema.NET;
using System;

namespace HQ78.Recipe.Api.Conventions
{
    public class ThingSerializer : SerializerBase<IThing>
    {
        public override IThing Deserialize(
            BsonDeserializationContext context,
            BsonDeserializationArgs args
        )
        {
            switch (context.Reader.GetCurrentBsonType())
            {
                case BsonType.Document:
                    return base.Deserialize(context, args);
                case BsonType.String:
                    return new Thing
                    {
                        Name = context.Reader.ReadString()
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
