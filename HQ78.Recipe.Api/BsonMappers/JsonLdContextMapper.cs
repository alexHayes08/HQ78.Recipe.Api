using MongoDB.Bson.Serialization;
using Schema.NET;

namespace HQ78.Recipe.Api.BsonMappers
{
    public class JsonLdContextMapper : IBsonMapper<JsonLdContext>
    {
        public void SetupBsonMapping(BsonClassMap<JsonLdContext> document)
        {
            document.AutoMap();
        }
    }
}
