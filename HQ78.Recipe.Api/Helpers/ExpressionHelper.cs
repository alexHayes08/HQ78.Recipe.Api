using System;
using System.Linq.Expressions;

namespace HQ78.Recipe.Api.Helpers
{
    public static class ExpressionHelper
    {
        public static string GetAccessorExpressionName<TClass, TProp>(
            Expression<Func<TClass, TProp>> accessor
        )
        {
            return accessor.Body switch
            {
                MemberExpression m => m.Member.Name,
                UnaryExpression x when x.Operand is MemberExpression m => m.Member.Name,
                _ => throw new NotImplementedException()
            };
        }
    }
}
