using System.Collections.Generic;

namespace HQ78.Recipe.DataLayer
{
    public class NutritionalInformation
    {
        public IEnumerable<string> Calories { get; set; }
        public IEnumerable<string> CarbohydrateContent { get; set; }
        public IEnumerable<string> CholesterolContent { get; set; }
        public IEnumerable<string> FatContent { get; set; }
        public IEnumerable<string> FiberContent { get; set; }
        public IEnumerable<string> ProteinContent { get; set; }
        public IEnumerable<string> SaturatedFatContent { get; set; }
        public IEnumerable<string> ServingSize { get; set; }
        public IEnumerable<string> SodiumContent { get; set; }
        public IEnumerable<string> SugarContent { get; set; }
        public IEnumerable<string> TransFatContent { get; set; }
        public IEnumerable<string> UnsaturatedFatContent { get; set; }
    }
}
