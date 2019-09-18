using System.Collections.Generic;
using System.Linq.Expressions;

namespace PredicateExtensions.Core.Assets
{
    internal class SubstituteParameterVisitor : ExpressionVisitor
    {
        public readonly Dictionary<Expression, Expression> Sub = new Dictionary<Expression, Expression>();

        protected override Expression VisitParameter(ParameterExpression node) =>
            Sub.TryGetValue(node, out var newValue) ? newValue : node;
    }
}