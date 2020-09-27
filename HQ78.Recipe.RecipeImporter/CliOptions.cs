using CommandLine;
using System;

namespace HQ78.Recipe.RecipeImporter
{
    public class CliOptions
    {
        [Option(
            Default = "mongodb://localhost:27017",
            HelpText = "The mongodb connection string. Defaults the default local mongodb instance."
        )]
        public string DbConnectionString { get; set; }

        [Option(
            Default = "hq78-recipe-db",
            HelpText = "The name of the database.",
            Required = false
        )]
        public string DatabaseName { get; set; }

        [Option(
            Default = "SchemaObjects",
            HelpText = "The table name to insert the data into."
        )]
        public string TableName { get; set; }

        [Option(
            HelpText = "The url of the html page. Can be remote or local uri (http/https/file uri's are all valid).",
            Required = true
        )]
        public Uri Url { get; set; }

        [Option(
            Default = "//script[@type='application/ld+json']",
            HelpText = "A valid XPath selector which returns the script element containing the valid schema.org stuff.",
            Required = false
        )]
        public string XPathSelector { get; set; }
    }
}
