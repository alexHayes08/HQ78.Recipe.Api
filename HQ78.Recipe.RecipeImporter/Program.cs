using HtmlAgilityPack;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using Schema.NET;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            using var httpClient = new HttpClient();

            var message = new HttpRequestMessage(
                HttpMethod.Get,
                "https://www.allrecipes.com/recipe/24771/basic-mashed-potatoes/"
            );

            var response = await httpClient
                .SendAsync(message)
                .ConfigureAwait(false);

            var rawHtml = await response.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            var html = new HtmlDocument();
            html.LoadHtml(rawHtml);

            var querySelector = "//script[@type='application/ld+json']";

            var scriptNode = html.DocumentNode.SelectSingleNode(
                XPathExpression.Compile(querySelector)
            );

            var json = JArray.Parse(scriptNode.InnerText);

            var recipes = json
                .Where(n => n.Value<string>("@type") == "Recipe")
                .Select(n => n.ToObject<Schema.NET.Recipe>())
                .ToList();

            //var connectionString = "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass%20Community&ssl=false";
            var connectionString = "mongodb://localhost:27017";
            var mongoDbClient = new MongoClient(connectionString);
            var database = mongoDbClient.GetDatabase("hq78-recipe-db");
            var recipeRepository = database.GetCollection<Schema.NET.Recipe>("Recipes");

            await recipeRepository.InsertManyAsync(recipes).ConfigureAwait(false);

            foreach (var recipe in recipes)
                Console.WriteLine(recipe.Name);
        }
    }
}
