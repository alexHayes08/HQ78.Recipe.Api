namespace HQ78.Recipe.Api.Models
{
    public class IngredientQuantity
    {
        /// <summary>
        /// The unit of the quanitity. May be cups, liters, gallons, etc...
        /// </summary>
        public string MeasurementType { get; set; }

        /// <summary>
        /// The numeric part of the quantity.
        /// </summary>
        public double Value { get; set; }
    }
}