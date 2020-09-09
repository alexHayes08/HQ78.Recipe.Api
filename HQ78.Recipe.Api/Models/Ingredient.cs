using HotChocolate;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HQ78.Recipe.Api.Models
{
    public class Ingredient
        : IHasName
        , IHasId
    {
        public Ingredient()
        {
            this.Id = ObjectId.Empty;
            this.RecipeId = string.Empty;
            this.Name = string.Empty;

            this.Quantity = new IngredientQuantity
            {
                MeasurementType = string.Empty,
                Value = 0
            };
        }

        [BsonId(Order = 0)]
        [GraphQLType(typeof(string))]
        public ObjectId Id { get; set; }

        /// <summary>
        /// The id of the recipe this ingredient belongs to.
        /// </summary>
        [BsonElement("recipeId", Order = 1)]
        public string RecipeId { get; set; }

        /// <summary>
        /// The name of the ingredient.
        /// </summary>
        [BsonElement("name", Order = 2)]
        public string Name { get; set; }

        /// <summary>
        /// The quantity of the ingredient.
        /// </summary>
        [BsonElement("quantity", Order = 3)]
        public IngredientQuantity Quantity { get; set; }

        public static implicit operator Ingredient(string ingredient)
        {
            return new Ingredient
            {
                Name = ingredient,
                Quantity = new IngredientQuantity
                {
                    MeasurementType = string.Empty,
                    Value = 1
                }
            };
        }

        public static implicit operator string(Ingredient ingredient)
        {
            return ingredient.Name;
        }
    }
}
