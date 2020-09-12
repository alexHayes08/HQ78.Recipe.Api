using System;
using System.Collections.Generic;

namespace HQ78.Recipe.DataLayer
{
    public class Recipe
    {
        public IEnumerable<string> CookingMethod { get; set; }
        public IEnumerable<TimeSpan> CookTime { get; set; }
        public IEnumerable<NutritionalInformation> Nutrition { get; set; }
        public IEnumerable<string> RecipeCategory { get; set; }
        public IEnumerable<string> RecipeCuisine { get; set; }
        public IEnumerable<string> RecipeIngredient { get; set; }
        public IEnumerable<string> RecipeInstructions { get; set; }
        public object RecipeYield { get; set; }
        public object SuitableForDiet { get; set; }
    }
}
