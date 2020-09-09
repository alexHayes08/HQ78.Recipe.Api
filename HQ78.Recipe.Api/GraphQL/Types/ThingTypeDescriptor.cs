using HotChocolate.Types;
using Schema.NET;
using System.Collections.Generic;

namespace HQ78.Recipe.Api.GraphQL.Types
{
    public class ThingTypeDescriptor : InterfaceType<IThing>
    {
        protected override void Configure(IInterfaceTypeDescriptor<IThing> descriptor)
        {
            descriptor.Name("IThing");
            descriptor.Field(x => x.AdditionalType);
            descriptor.Field(x => x.AlternateName);
            descriptor.Field(x => x.DisambiguatingDescription);
            descriptor.Field(x => x.Description);
            descriptor.Field(x => x.Name);
            descriptor.Field(x => x.SameAs);
            descriptor.Field(x => x.Url);
 
            descriptor.Ignore(x => x.Identifier);
            descriptor.Ignore(x => x.PotentialAction);
            descriptor.Ignore(x => x.Image);
            descriptor.Ignore(x => x.MainEntityOfPage);
            descriptor.Ignore(x => x.SubjectOf);
        }
    }
}
