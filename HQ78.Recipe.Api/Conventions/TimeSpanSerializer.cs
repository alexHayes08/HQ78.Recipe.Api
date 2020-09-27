using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Xml;

namespace HQ78.Recipe.Api.Conventions
{
    public class TimeSpanSerializer : SerializerBase<TimeSpan>
    {
        public override TimeSpan Deserialize(
            BsonDeserializationContext context,
            BsonDeserializationArgs args
        )
        {
            var str = context.Reader.ReadString();
            var timeSpan = XmlConvert.ToTimeSpan(str);

            return timeSpan;
        }

        public override void Serialize(
            BsonSerializationContext context,
            BsonSerializationArgs args,
            TimeSpan value
        )
        {
            var str = XmlConvert.ToString(value);

            context.Writer.WriteString(str);
        }
    }
}
