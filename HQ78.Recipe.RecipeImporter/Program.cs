using CommandLine;
using HtmlAgilityPack;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace HQ78.Recipe.RecipeImporter
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<CliOptions>(args)
                .WithParsedAsync(o => HandleCliOptions(o))
                .ConfigureAwait(false);
        }

        public static async Task HandleCliOptions(CliOptions options)
        {
            var html = new HtmlDocument();

            if (options.Url.IsFile)
            {
                html.Load(options.Url.AbsolutePath);
            }
            else
            {
                using var httpClient = new HttpClient();

                var message = new HttpRequestMessage(
                    HttpMethod.Get,
                    options.Url
                );

                var response = await httpClient
                    .SendAsync(message)
                    .ConfigureAwait(false);

                var rawHtml = await response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                html.LoadHtml(rawHtml);
            }

            var scriptNode = html.DocumentNode.SelectSingleNode(
                XPathExpression.Compile(options.XPathSelector)
            );

            var json = JArray.Parse(scriptNode.InnerText);
            const string idElName = "id";

            var recipes = json
                .Where(n => n.Value<string>("@type") == "Recipe")
                .Select(
                    n =>
                    {
                        if (BsonDocument.TryParse(n.ToString(), out var doc))
                        {
                            // Only set the identifier if not already set.
                            if (!doc.Contains(idElName))
                                doc.Add(new BsonElement(idElName, options.Url.ToString()));

                            return doc;
                        }
                        else
                        {
                            Console.WriteLine("Unable to parse schema, invalid format.");

                            return null;
                        }
                    }
                )
                .Where(l => l is object)
                .ToList();

            var dbName = options.DatabaseName;
            var connectionString = options.DbConnectionString;
            var mongoDbClient = new MongoClient(connectionString);
            var database = mongoDbClient.GetDatabase(dbName);
            var recipeRepository = database.GetCollection<BsonDocument>(options.TableName);

            foreach (var recipe in recipes)
            {
                // Remove all entries with the same identifier.
                await recipeRepository
                    .FindOneAndReplaceAsync(
                        Builders<BsonDocument>.Filter.Eq(
                            idElName,
                            options.Url.ToString()
                        ),
                        recipe,
                        new FindOneAndReplaceOptions<BsonDocument, BsonDocument>()
                        {
                            IsUpsert = true
                        }
                    )
                    .ConfigureAwait(false);

                Console.WriteLine($"{recipe["name"].AsString} - {recipe["_id"].AsObjectId}");
            }
        }
    }
}
