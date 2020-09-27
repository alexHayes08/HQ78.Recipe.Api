using HQ78.Recipe.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Schema.NET;
using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using SRecipe = Schema.NET.Recipe;

namespace HQ78.Recipe.Api.Controllers
{
    [ApiController]
    [Route("Recipes")]
    public class RecipesController : ControllerBase
    {
        #region Fields

        private readonly IMongoCollection<BsonDocument> schemaObjectsRepository;

        #endregion

        #region Constructor

        public RecipesController(IMongoDatabase mongoDatabase)
        {
            schemaObjectsRepository = mongoDatabase.GetCollection<BsonDocument>("SchemaObjects");
        }

        #endregion

        #region Methods

        [HttpPost("listV2")]
        public async Task<IActionResult> ListRecipesV2(PaginationRequest paginationRequest)
        {
            var documentCursor = await schemaObjectsRepository.FindAsync(
                    Builders<BsonDocument>.Filter.Eq(
                        "@type",
                        "Recipe"
                    ),
                    new FindOptions<BsonDocument, BsonDocument>
                    {
                        Limit = paginationRequest.Limit
                    }
                )
                .ConfigureAwait(false);

            var documents = await documentCursor
                .ToListAsync()
                .ConfigureAwait(false);

            var formattedDocs = documents.Select(d => HandleId(d)).ToArray();

            var response = new PaginationResponse<BsonDocument>
            {
                Items = formattedDocs
            };

            return Content(response.ToJson(), MediaTypeNames.Application.Json);
        }

        [HttpPost("list")]
        public async Task<PaginationResponse<IRecipe>> ListRecipies(PaginationRequest paginationRequest)
        {
            var documentCursor = await schemaObjectsRepository.FindAsync(
                    Builders<BsonDocument>.Filter.Eq(
                        "@type",
                        "Recipe"
                    ),
                    new FindOptions<BsonDocument, BsonDocument>
                    {
                        Limit = paginationRequest.Limit
                    }
                )
                .ConfigureAwait(false);

            var documents = await documentCursor
                .ToListAsync()
                .ConfigureAwait(false);

            var recipes = documents
                .Select(
                    doc =>
                    {
                        var recipe = BsonSerializer.Deserialize<IRecipe>(doc);

                        return recipe;
                    })
                .ToArray();

            return new PaginationResponse<IRecipe>
            {
                Items = recipes,
                TotalCount = recipes.Length
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IRecipe>> GetRecipe(string id)
        {
            var document = await schemaObjectsRepository.Find(
                    Builders<BsonDocument>.Filter.And(
                        Builders<BsonDocument>.Filter.Eq(
                            "@type",
                            "Recipe"
                        ),
                        Builders<BsonDocument>.Filter.Eq(
                            "identifier",
                            id
                        )
                    )
                )
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (document is null)
                return NotFound();

            var recipe = BsonSerializer.Deserialize<IRecipe>(document);

            return new ActionResult<IRecipe>(recipe);
        }

        private BsonDocument HandleId(BsonDocument document)
        {
            var id = document["_id"].AsObjectId;
            document.Remove("_id");
            document.InsertAt(2, new BsonElement("identifier", id.ToString()));

            return document;
        }

        #endregion
    }
}
