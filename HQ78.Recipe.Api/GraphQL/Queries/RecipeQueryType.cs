using HotChocolate;
using HotChocolate.Types;
using HQ78.Recipe.Api.Models;
using System;
using System.Collections.Generic;
using SRecipe = Schema.NET.Recipe;
//using SRecipe = HQ78.Recipe.Api.Models.Recipe;

namespace HQ78.Recipe.Api.GraphQL.Queries
{
    [ExtendObjectType(Name = QueryConstants.QueryTypeName)]
    public class RecipeQueryType
    {
        #region Methods

        [GraphQLName("recipes")]
        public IEnumerable<SRecipe> GetRecipes()
        {
            return new[]
            {
                //new SRecipe
                //{
                //    CookeTime = TimeSpan.FromHours(1.5),
                //    Ingredients = "apples"
                //}
                new SRecipe
                {
                    CookTime = new Schema.NET.OneOrMany<TimeSpan?>(TimeSpan.FromHours(1.5)),
                    RecipeIngredient = new Schema.NET.OneOrMany<string>("apples")
                }
            };
        }

        #endregion
    }
}
