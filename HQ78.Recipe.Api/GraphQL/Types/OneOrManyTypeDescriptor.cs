using HotChocolate.Types;
using Schema.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HQ78.Recipe.Api.GraphQL.Types
{
    public class OneOrManyTypeDescriptor : UnionType
    {
        protected override void Configure(IUnionTypeDescriptor descriptor)
        {
            descriptor.Name("OneOrMany");
            //descriptor.Type<ObjectType>();
            //descriptor.Type<ListType<ObjectType>>();
        }
    }
}
