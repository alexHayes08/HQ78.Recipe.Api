using HQ78.Recipe.DataLayer.BsonMappers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace HQ78.Recipe.DataLayer.SchemaMappings
{
    public class GenericSchemaObject
    {
        public string Type { get; set; }
        public string Context { get; set; }
        public string Identifier { get; set; } // The db id?
        public Uri Id { get; set; }
        public IDictionary<string, BsonValue> OtherProperties { get; set; }
    }

    public class GenericSchemaObjectMapping : IBsonMapper<GenericSchemaObject>
    {
        public void SetupBsonMapping(BsonClassMap<GenericSchemaObject> document)
        {
            throw new NotImplementedException();
        }
    }
}
