using System;
using System.Linq.Expressions;
using FluentAssertions;
using Xunit;

namespace PredicateExtensions.Core.UnitTests
{
    /* 
     * Data is not needed for these tests, we only need to see if the expression trees are formed correctly
     * Assertions are performed on strings instead of Expression objects 
     * since two Expressions will evaluate to different instances
     */

    public class PredicateExtensionTests
    {
        readonly Expression<Func<string, bool>> _equalsA = str => str == "A";
        readonly Expression<Func<string, bool>> _equalsC = str => str == "C";
        readonly Expression<Func<string, bool>> _containsB = str => str.Contains("B");

        [Fact]
        public void Can_Reduce_Predicates_With_PredicateExtensions_Or_Method()
        {
            Expression<Func<string, bool>> expectedOrExpression = str => (str == "A" || str.Contains("B"));
            var expectedExpression = expectedOrExpression.ToString();
            
            var orExpression = _equalsA.Or(_containsB);
            var resultExpression = orExpression.ToString();
            
            resultExpression.Should().Be(expectedExpression);
            LogResults(expectedExpression, resultExpression);
        }
  
        [Fact]
        public void Can_Reduce_Predicates_With_PredicateExtensions_And_Method()
        {
            Expression<Func<string, bool>> expectedAndExpression = str => (str == "A" && str.Contains("B"));
            var expectedExpression = expectedAndExpression.ToString();

            var andExpression = _equalsA.And(_containsB);
            var resultExpression = andExpression.ToString();

            resultExpression.Should().Be(expectedExpression);
            LogResults(expectedExpression, resultExpression);
        }
  
        [Fact]
        public void Can_Begin_New_Expression_When_Or_Method()
        {
            var predicate = PredicateExtensions.Begin<string>(false);
            Expression<Func<string, bool>> expectedOrExpression = str => (str == "A" || str.Contains("B"));
            var expectedExpression = expectedOrExpression.ToString();

            var orExpression = predicate.Or(_equalsA.Or(_containsB));
            var resultExpression = orExpression.ToString();

            resultExpression.Should().Be(expectedExpression);
            LogResults(expectedExpression, resultExpression);
        }

        [Fact]
        public void Can_Begin_New_Expression_When_And_Method()
        {
            var predicate = PredicateExtensions.Begin<string>(true);
            Expression<Func<string, bool>> expectedOrExpression = str => (str == "A" || str.Contains("B"));
            var expectedExpression = expectedOrExpression.ToString();

            var orExpression = predicate.And(_equalsA.Or(_containsB));
            var resultExpression = orExpression.ToString();

            resultExpression.Should().Be(expectedExpression);
            LogResults(expectedExpression, resultExpression);
        }

        [Fact]
        public void Cant_Begin_New_Expression_When_Or_Method()
        {
            var predicate = PredicateExtensions.Begin<string>(true);
            Expression<Func<string, bool>> expectedOrExpression = str => (str == "A" || str.Contains("B"));
            var expectedExpression = predicate.ToString();

            var orExpression = predicate.Or(_equalsA.Or(_containsB));
            var resultExpression = orExpression.ToString();

            resultExpression.Should().Be(expectedExpression);
            LogResults(expectedExpression, resultExpression);
        }

        [Fact]
        public void Cant_Begin_New_Expression_When_And_Method()
        {
            var predicate = PredicateExtensions.Begin<string>(false);
            Expression<Func<string, bool>> expectedOrExpression = str => (str == "A" || str.Contains("B"));
            var expectedExpression = predicate.ToString();

            var orExpression = predicate.And(_equalsA.Or(_containsB));
            var resultExpression = orExpression.ToString();

            resultExpression.Should().Be(expectedExpression);
            LogResults(expectedExpression, resultExpression);
        }

        [Fact]
        public void Can_Reduce_Grouped_Predicates()
        {
            Expression<Func<string, bool>> expectedGroupedPredicate;
            expectedGroupedPredicate = str => (str == "A" || str.Contains("B")) && (str == "C");

            var expectedExpression = expectedGroupedPredicate.ToString();

            var groupedPredicate =
                (_equalsA.Or(_containsB))
                .And(_equalsC);

            var resultExpression = groupedPredicate.ToString();

            resultExpression.Should().Be(resultExpression);

            LogResults(expectedExpression, resultExpression);
        }
  
        private void LogResults(string expectedExpression, string resultExpression)
        {
            Console.Write(expectedExpression);
            Console.WriteLine();
            Console.Write(resultExpression);
        }
    }
}