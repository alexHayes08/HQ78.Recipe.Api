using CommandLine;
using HtmlAgilityPack;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using Schema.NET;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;

namespace HQ78.Recipe.RecipeImporter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<CliOptions>(args)
                .WithParsedAsync(o => HandleCliOptions(o))
                .ConfigureAwait(false);
        }

        public static async Task HandleCliOptions(CliOptions options)
        {
            using var httpClient = new HttpClient();

            var url = options.Url ?? "https://www.allrecipes.com/recipe/24771/basic-mashed-potatoes/";

            var message = new HttpRequestMessage(
                HttpMethod.Get,
                url
            );

            var response = await httpClient
                .SendAsync(message)
                .ConfigureAwait(false);

            var rawHtml = await response.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            var html = new HtmlDocument();
            html.LoadHtml(rawHtml);

            var querySelector = options.XPathSelector ?? "//script[@type='application/ld+json']";

            var scriptNode = html.DocumentNode.SelectSingleNode(
                XPathExpression.Compile(querySelector)
            );

            var json = JArray.Parse(scriptNode.InnerText);

            var recipes = json
                .Where(n => n.Value<string>("@type") == "Recipe")
                .Select(
                    n =>
                    {
                        var doc = BsonDocument.Parse(n.ToString());

                        if (!doc.Contains("identifier"))
                            doc.Add(new BsonElement("identifier", url));

                        return doc;
                    }
                )
                .ToList();

            var dbName = options.DatabaseName ?? "hq78-recipe-db";
            var connectionString = options.DbConnectionString ?? "mongodb://localhost:27017";
            var mongoDbClient = new MongoClient(connectionString);
            var database = mongoDbClient.GetDatabase(dbName);
            var recipeRepository = database.GetCollection<BsonDocument>("Recipes");

            await recipeRepository.InsertManyAsync(recipes).ConfigureAwait(false);

            foreach (var recipe in recipes)
                Console.WriteLine(recipe["name"].AsString);
        }
    }
}
