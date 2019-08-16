using System;
using System.Linq;
using System.Linq.Expressions;
using PredicateExtensions.Assets;

namespace PredicateExtensions
{
    //Attributed to: Adam Tegen via StackOverflow http://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
    //Modified by Ed Charbeneau
    public static class PredicateExtensions
    {
        /// <summary>
        /// Begin an expression chain
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Default return value if the chanin is ended early</param>
        /// <returns>A lambda expression stub</returns>
        public static Expression<Func<T, bool>> Begin<T>(bool value = false)
        {
            if (value)
                return parameter => true; //value cannot be used in place of true/false

            return parameter => false;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right) =>
            CombineLambdas(left, right, ExpressionType.AndAlso);

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right) => CombineLambdas(left, right, ExpressionType.OrElse);

        private static Expression<Func<T, bool>> CombineLambdas<T>(this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right, ExpressionType expressionType)
        {
            //Remove expressions created with Begin<T>()
            if (IsExpressionBodyConstant(left))
                return (right);

            var p = left.Parameters.First();

            var visitor = new SubstituteParameterVisitor();
            visitor.Sub[right.Parameters.First()] = p;

            Expression body = Expression.MakeBinary(expressionType, left.Body, visitor.Visit(right.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        private static bool IsExpressionBodyConstant<T>(Expression<Func<T, bool>> left) =>
            left.Body.NodeType == ExpressionType.Constant;
    }
}