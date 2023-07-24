using HQ78.Recipe.Api.BsonMappers;
using HQ78.Recipe.Api.Conventions;
using HQ78.Recipe.Api.Extensions;
using HQ78.Recipe.Api.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using Schema.NET;
using System;

namespace HQ78.Recipe.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region MongoDB

            var conventionPack = new ConventionPack
            {
                new CamelCaseElementNameConvention()
            };

            ConventionRegistry.Register(
                "camelCase",
                conventionPack,
                t => true
            );

            //BsonClassMap.RegisterClassMap<SRecipe>(
            //    x => new RecipeMapper().SetupBsonMapping(x)
            //);

            BsonClassMap.RegisterClassMap<JsonLdObject>(
                x => new JsonLdObjectMapper().SetupBsonMapping(x)
            );

            BsonClassMap.RegisterClassMap<Thing>(
                x => new ThingMapper().SetupBsonMapping(x)
            );

            BsonSerializer.RegisterSerializationProvider(
                new ValuesSerializerProvider()
            );

            BsonSerializer.RegisterGenericSerializerDefinition(
                typeof(OneOrMany<>),
                typeof(OneOrManySerializer<>)
            );

            MongoDbHelper.Default
                .MapInterfaceToDefaultImplementation<IAggregateRating>()
                .MapInterfaceToDefaultImplementation<IClaimReview>()
                .MapInterfaceToDefaultImplementation<IClip>()
                .MapInterfaceToDefaultImplementation<ICreativeWork>()
                .MapInterfaceToDefaultImplementation<IDemand>()
                .MapInterfaceToDefaultImplementation<IHowTo>()
                .MapInterfaceToDefaultImplementation<IHowToSection>()
                .MapInterfaceToDefaultImplementation<IHowToDirection>()
                .MapInterfaceToDefaultImplementation<IHowToItem>()
                .MapInterfaceToDefaultImplementation<IHowToStep>()
                .MapInterfaceToDefaultImplementation<IHowToSupply>()
                .MapInterfaceToDefaultImplementation<IHowToTip>()
                .MapInterfaceToDefaultImplementation<IHowToTool>()
                .MapInterfaceToDefaultImplementation<IIntangible>()
                .MapInterfaceToDefaultImplementation<IImageGallery>()
                .MapInterfaceToDefaultImplementation<IImageObject>()
                .MapInterfaceToDefaultImplementation<IItemList>()
                .MapInterfaceToDefaultImplementation<IMonetaryAmount>()
                .MapInterfaceToDefaultImplementation<INutritionInformation>()
                .MapInterfaceToDefaultImplementation<IOrganization>()
                .MapInterfaceToDefaultImplementation<IPerson>()
                .MapInterfaceToDefaultImplementation<IPublicationEvent>()
                .MapInterfaceToDefaultImplementation<IQuantitativeValue>()
                .MapInterfaceToDefaultImplementation<IRating>()
                .MapInterfaceToDefaultImplementation<IRecipe>()
                .MapInterfaceToDefaultImplementation<IReview>()
                .MapInterfaceToDefaultImplementation<IReviewAction>()
                .MapInterfaceToDefaultImplementation<IStructuredValue>()
                .MapInterfaceToDefaultImplementation<IThing, Thing>(x => x.Name)
                .MapInterfaceToDefaultImplementation<IVideoObject>();

            //BsonSerializer.RegisterSerializer(
            //    typeof(IThing),
            //    new ThingSerializer()
            //);

            BsonSerializer.RegisterSerializer(
                typeof(TimeSpan),
                new TimeSpanSerializer()
            );

            BsonSerializer.RegisterSerializer(
                typeof(JsonLdContext),
                new JsonLdContextSerializer()
            );

            BsonSerializer.RegisterGenericSerializerDefinition(
                typeof(Values<,>),
                typeof(ValuesSerializer<,>)
            );

            BsonSerializer.RegisterGenericSerializerDefinition(
                typeof(Values<,,>),
                typeof(ValuesSerializer<,,>)
            );

            BsonSerializer.RegisterGenericSerializerDefinition(
                typeof(Values<,,,>),
                typeof(ValuesSerializer<,,,>)
            );

            BsonSerializer.RegisterGenericSerializerDefinition(
                typeof(Values<,,,,,,>),
                typeof(ValuesSerializer<,,,,,,>)
            );

            //BsonSerializer.RegisterDiscriminatorConvention(
            //    typeof(Values<,>),
            //    new SchemaValuesConvention()
            //);

            //BsonSerializer.RegisterDiscriminatorConvention(
            //    typeof(Values<,,>),
            //    new SchemaValuesConvention()
            //);

            //BsonSerializer.RegisterDiscriminatorConvention(
            //    typeof(Values<,,,>),
            //    new SchemaValuesConvention()
            //);

            //BsonSerializer.RegisterDiscriminatorConvention(
            //    typeof(Values<,,,,,,>),
            //    new SchemaValuesConvention()
            //);

            services
                .AddMongoDb(
                    Configuration.GetConnectionString("RecipeDb"),
                    Configuration.GetValue<string>("DatabaseName")
                );

            #endregion

            //services.AddGraphQL(
            //    sp =>
            //    {
            //        var schema = SchemaBuilder.New()
            //            //.EnableRelaySupport()
            //            .AddType<ThingTypeDescriptor>()
            //            .AddServices(sp)
            //            //.AddObjectType<OneOrManyTypeDescriptor>()
            //            //.Use(next => context =>
            //            //{
            //            //    return Task.CompletedTask;
            //            //})
            //            //.AddObjectType<ThingTypeDescriptor>()
            //            .AddObjectType<SchemaOrgTypeDescriptor<SRecipe>>()
            //            //.AddObjectType<RecipeTypeDescriptor>()
            //            .AddQueryType(d => d.Name(QueryConstants.QueryTypeName))
            //            //.AddType<RecipeQueryType>()
            //            .AddType<HelloWorldQueryType>()
            //            .Create();

            //        return schema;
            //    }
            //);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseWebSockets();

            #region GraphQL

            //const string GRAPH_QL_PATH = "/api/graphql";

            //app
            //    .UseGraphQL(GRAPH_QL_PATH)
            //    //.UseGraphiQL()
            //        //new GraphiQLOptions
            //        //{
            //        //    Path = "/graphql"
            //        //})
            //    .UsePlayground(GRAPH_QL_PATH, GRAPH_QL_PATH + "/playground")
            //    .UseVoyager(GRAPH_QL_PATH, GRAPH_QL_PATH + "/voyager");

            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
