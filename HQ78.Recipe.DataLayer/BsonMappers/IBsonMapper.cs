using MongoDB.Bson.Serialization;

namespace HQ78.Recipe.DataLayer.BsonMappers
{
    public interface IBsonMapper<T> where T : class
    {
        void SetupBsonMapping(BsonClassMap<T> document);
    }
}
