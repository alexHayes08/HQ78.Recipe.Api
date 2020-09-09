using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.GraphiQL;
using HotChocolate.AspNetCore.Voyager;
using HotChocolate.Types.Descriptors;
using HQ78.Recipe.Api.BsonMappers;
using HQ78.Recipe.Api.Extensions;
using HQ78.Recipe.Api.GraphQL.Queries;
using HQ78.Recipe.Api.GraphQL.Types;
using HQ78.Recipe.Api.Models;
using HQ78.Recipe.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schema.NET;
using System;
using System.Threading.Tasks;
using SRecipe = Schema.NET.Recipe;

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

            services
                .MapBsonClass<SRecipe, RecipeMapper>()
                .AddMongoDb(
                    Configuration.GetConnectionString("RecipeDb"),
                    Configuration.GetValue<string>("DatabaseName")
                );

            services.AddGraphQL(
                sp =>
                {
                    var schema = SchemaBuilder.New()
                        //.EnableRelaySupport()
                        .AddType<ThingTypeDescriptor>()
                        .AddServices(sp)
                        //.AddObjectType<OneOrManyTypeDescriptor>()
                        //.Use(next => context =>
                        //{
                        //    return Task.CompletedTask;
                        //})
                        //.AddObjectType<ThingTypeDescriptor>()
                        .AddObjectType<SchemaOrgTypeDescriptor<SRecipe>>()
                        //.AddObjectType<RecipeTypeDescriptor>()
                        .AddQueryType(d => d.Name(QueryConstants.QueryTypeName))
                        //.AddType<RecipeQueryType>()
                        .AddType<HelloWorldQueryType>()
                        .Create();

                    return schema;
                }
            );
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

            const string GRAPH_QL_PATH = "/api/graphql";

            app
                .UseGraphQL(GRAPH_QL_PATH)
                //.UseGraphiQL()
                    //new GraphiQLOptions
                    //{
                    //    Path = "/graphql"
                    //})
                .UsePlayground(GRAPH_QL_PATH, GRAPH_QL_PATH + "/playground")
                .UseVoyager(GRAPH_QL_PATH, GRAPH_QL_PATH + "/voyager");

            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
