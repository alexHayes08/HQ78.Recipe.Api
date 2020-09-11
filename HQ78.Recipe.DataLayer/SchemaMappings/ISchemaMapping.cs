namespace HQ78.Recipe.DataLayer.SchemaMappings
{
    public interface ISchemaMapping<TSchema, TModel>
    {
        void PopulateSchema(TSchema schema, TModel model);
        void PopulateModel(TModel model, TSchema schema);
    }
}
