using MongoDB.Bson.Serialization;
using Schema.NET;

namespace HQ78.Recipe.Api.BsonMappers
{
    public class ThingMapper : IBsonMapper<Thing>
    {
        public void SetupBsonMapping(BsonClassMap<Thing> document)
        {
            document.AutoMap();
            document.MapIdField(x => x.Identifier);
        }
    }
}
