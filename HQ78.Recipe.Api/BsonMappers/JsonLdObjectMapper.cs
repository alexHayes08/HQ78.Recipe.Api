using MongoDB.Bson.Serialization;
using Schema.NET;

namespace HQ78.Recipe.Api.BsonMappers
{
    public class JsonLdObjectMapper : IBsonMapper<JsonLdObject>
    {
        public void SetupBsonMapping(BsonClassMap<JsonLdObject> document)
        {
            document.MapField(x => x.Context).SetElementName("@context");
            document.MapField(x => x.Type).SetElementName("@type");
            document.MapField(x => x.Id).SetElementName("id");
        }
    }
}
