using System.Collections.Generic;
using System.Linq.Expressions;

namespace PredicateExtensions.Assets
{
    internal class SubstituteParameterVisitor : ExpressionVisitor
    {
        public readonly Dictionary<Expression, Expression> Sub = new Dictionary<Expression, Expression>();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            Expression newValue;
            return Sub.TryGetValue(node, out newValue) ? newValue : node;
        }
    }
}