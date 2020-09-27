using HQ78.Recipe.Api.BsonMappers;
using HQ78.Recipe.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;

namespace HQ78.Recipe.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection MapBsonClass<TClass, TBsonMapper>(
            this IServiceCollection serviceDescriptors
        )
            where TBsonMapper : class, IBsonMapper<TClass>, new()
            where TClass : class
        {
            var mapper = new TBsonMapper();

            BsonClassMap.RegisterClassMap<TClass>(
                classMap => mapper.SetupBsonMapping(classMap)
            );

            return serviceDescriptors;
        }

        public static IServiceCollection AddMongoDb(
            this IServiceCollection serviceDescriptors,
            string connectionString,
            string dbName
        )
        {
            serviceDescriptors.AddSingleton<IMongoClient>(
                sp =>
                {
                    var client = new MongoClient(connectionString);

                    return client;
                }
            );

            serviceDescriptors.AddSingleton<IMongoDatabase>(
                sp =>
                {
                    var client = sp.GetRequiredService<IMongoClient>();

                    return client.GetDatabase(dbName);
                }
            );

            serviceDescriptors.AddSingleton<MongoCollectionSettings>(
                sp =>
                {
                    var settings = new MongoCollectionSettings();

                    return settings;
                }
            );

            serviceDescriptors.AddTransient(
                typeof(IMongoCollection<>),
                typeof(MongoCollectionProvider<>)
            );

            return serviceDescriptors;
        }
    }
}
