using MongoDB.Bson.Serialization;

using SRecipe = Schema.NET.Recipe;

namespace HQ78.Recipe.Api.BsonMappers
{
    public class RecipeMapper : IBsonMapper<SRecipe>
    {
        public void SetupBsonMapping(BsonClassMap<SRecipe> document)
        {
            document.AutoMap();

            // The identifier is used instead of the id as this is where the
            // db id can be stored where the Recipe.Id stores the original url
            // of the recipe.
            //document.MapIdMember(r => r.Identifier);
        }
    }
}
