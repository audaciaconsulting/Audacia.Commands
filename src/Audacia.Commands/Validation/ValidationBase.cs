using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Audacia.Core.Extensions;

namespace Audacia.Commands.Validation
{
    /// <summary>
    /// Base class for validation.
    /// </summary>
    public class ValidationBase
    {
        /// <summary>
        /// Gets the name of the property represented by the given <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">The <see cref="Expression"/> object containing the properties.</param>
        /// <returns>The name of the property.</returns>
        protected string GetNameForProperty(Expression expression)
        {
            if (expression is LambdaExpression)
            {
                return ExpressionExtensions.GetPropertyInfo(expression)?.Name ?? string.Empty;
            }

            if (expression is UnaryExpression unaryExpression)
            {
                return string.Join(".", GetProperties(unaryExpression.Operand).Select(p => p?.Name));
            }

            return string.Empty;
        }
        
        /// <summary>
        /// Gets the properties from the given <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">The <see cref="Expression"/> object containing the properties.</param>
        /// <returns>A collection of properties contained in the given <paramref name="expression"/>.</returns>
        protected IEnumerable<PropertyInfo?> GetProperties(Expression expression)
        {
            if (!(expression is MemberExpression memberExpression))
            {
                yield break;
            }

            var property = memberExpression.Member as PropertyInfo;
            foreach (var propertyInfo in GetProperties(memberExpression.Expression))
            {
                yield return propertyInfo;
            }

            yield return property;
        }
    }
}
