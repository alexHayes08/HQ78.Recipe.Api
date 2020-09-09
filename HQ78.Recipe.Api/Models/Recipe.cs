using HotChocolate;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Schema.NET;
using System;
using System.Collections.Generic;

namespace HQ78.Recipe.Api.Models
{
    public class Recipe : IHasId
    {
        public Recipe()
        {
            Id = ObjectId.Empty;
            Ingredients = string.Empty;
            Steps = Array.Empty<string>();
            CookeTime = TimeSpan.Zero;
        }

        [BsonId]
        [GraphQLType(typeof(string))]
        public ObjectId Id { get; set; }

        public TimeSpan CookeTime { get; set; }

        public OneOrMany<string> Ingredients { get; set; }

        public ICollection<string> Steps { get; }
    }
}
