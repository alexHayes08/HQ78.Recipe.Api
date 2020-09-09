using HotChocolate;
using HotChocolate.Types;
using System;

namespace HQ78.Recipe.Api.GraphQL.Queries
{
    [ExtendObjectType(Name = QueryConstants.QueryTypeName)]
    public class HelloWorldQueryType
    {
        #region Methods

        [GraphQLName("helloWorld")]
        public string GetHelloWorld() => $"Hello world, time is {DateTime.Now.ToShortDateString()}.";

        #endregion
    }
}
