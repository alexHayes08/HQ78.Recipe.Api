using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HQ78.Recipe.Api.Models
{
    public interface IHasId
    {
        ObjectId Id { get; set; }
    }
}
