﻿using MongoDB.Bson.Serialization;

using SRecipe = Schema.NET.Recipe;

namespace HQ78.Recipe.Api.BsonMappers
{
    public class RecipeMapper : IBsonMapper<SRecipe>
    {
        public void SetupBsonMapping(BsonClassMap<SRecipe> document)
        {
            document.AutoMap();

            document.MapField(x => x.Image);

            // Use the id for the auto-generated db id.
            // Reserve the identifier field for the original url of the schema object.
            document.MapIdField(x => x.Id);
        }
    }
}
