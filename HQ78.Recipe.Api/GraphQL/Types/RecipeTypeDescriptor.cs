using HotChocolate.Types;
using HQ78.Recipe.Api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using SRecipe = Schema.NET.Recipe;

namespace HQ78.Recipe.Api.GraphQL.Types
{
    public class RecipeTypeDescriptor : ObjectType<SRecipe>
    {
        protected override void Configure(IObjectTypeDescriptor<SRecipe> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Name("Recipe");
            //descriptor.Field(x => x.Name);
            descriptor.Field(x => x.CookingMethod);
            descriptor.Field(x => x.RecipeIngredient);

            return;

            var recipeType = typeof(SRecipe);

            descriptor.Name(recipeType.Name);

            var props = recipeType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var prop in props)
            {
                if (prop.PropertyType.Name.StartsWith("OneOrMany"))
                {
                    GenerateOneOrManyDescriptor(descriptor, prop);
                }
                else if (prop.PropertyType.Name.StartsWith("Values"))
                {
                    GenerateValuesDescriptor(descriptor, prop);
                }
                else
                {
                    var parameterExpression = Expression.Parameter(recipeType, "x");

                    var expression = Expression.Lambda(
                        Expression.MakeMemberAccess(parameterExpression, prop),
                        parameterExpression
                    ) as Expression<Func<SRecipe, object>>;

                    descriptor.Field(expression);
                }
            }
        }

        private static void GenerateValuesDescriptor<T>(
            IObjectTypeDescriptor<T> parentDescriptor,
            MemberInfo member
        )
        {
            var memberType = member.GetMemberType();
            var genericTypeDef = memberType.GetGenericTypeDefinition();

            if (!genericTypeDef.Name.StartsWith("Values"))
                throw new ArgumentException("Member type must be a Values type.", nameof(member));

            var fieldDescriptor = parentDescriptor.Field(member.Name);
            var genericArgs = memberType.GetGenericArguments();

            foreach (var genericArg in genericArgs)
            {
                fieldDescriptor.Type(genericArg);
            }
        }

        private static void GenerateOneOrManyDescriptor<T>(
            IObjectTypeDescriptor<T> parentDescriptor,
            MemberInfo member
        )
        {
            var memberType = member.GetMemberType();
            var genericTypeDef = memberType.GetGenericTypeDefinition();

            if (!genericTypeDef.Name.StartsWith("OneOrMany"))
                throw new ArgumentException("Member type must be a OneOrMany type.", nameof(member));

            var wrappedSingleType = memberType.GetGenericArguments().First();
            var enumerableType = typeof(IEnumerable<>).MakeGenericType(memberType);

            parentDescriptor.Field(member.Name)
                .Type(wrappedSingleType)
                .Type(enumerableType);
        }
    }
}
