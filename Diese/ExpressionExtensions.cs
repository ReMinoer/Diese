using System;
using System.Linq.Expressions;

namespace Diese
{
    static public class ExpressionExtensions
    {
        static public Func<TProperty> Getter<TProperty>(this Expression<Func<TProperty>> propertyLambda)
        {
            return Expression.Lambda<Func<TProperty>>(propertyLambda.Body).Compile();
        }

        static public Action<TObject, TProperty> Setter<TObject, TProperty>(this Expression<Func<TObject, TProperty>> propertyLambda)
        {
            var member = (MemberExpression)propertyLambda.Body;
            ParameterExpression param = Expression.Parameter(typeof(TProperty), "value");
            return Expression.Lambda<Action<TObject, TProperty>>(Expression.Assign(member, param), propertyLambda.Parameters[0], param).Compile();
        }
    }
}