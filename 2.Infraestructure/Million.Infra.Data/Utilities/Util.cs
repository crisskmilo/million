using System.Linq.Expressions;
using System.Linq;
using System;

namespace Million.Infra.Data.Utilities
{
    public static class Util
    {
        public static IQueryable<T> ContainsByField<T>(this IQueryable<T> q, string field, string value)
        {
            var eParam = Expression.Parameter(typeof(T), "e");
            var property = Expression.Property(eParam, field);
            if (property.Type != typeof(string))
                throw new InvalidOperationException($"Property '{field}' must be string type.");

            var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
            var lowerProperty = Expression.Call(property, toLowerMethod);
            var lowerValue = Expression.Constant(value.ToLower(), typeof(string));
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var containsCall = Expression.Call(lowerProperty, containsMethod, lowerValue);
            var notNullCheck = Expression.NotEqual(property, Expression.Constant(null, typeof(string)));
            var lambda = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(notNullCheck, containsCall), eParam);
            return q.Where(lambda);
        }

        public static IQueryable<T> ContainsByField<T>(this IQueryable<T> q, string field, string[] values)
        {
            var eParam = Expression.Parameter(typeof(T), "e");
            var property = Expression.Property(eParam, field);
            if (property.Type != typeof(string))
                throw new InvalidOperationException($"Property '{field}' must be string type.");
            var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
            var lowerProperty = Expression.Call(property, toLowerMethod);
            var notNullCheck = Expression.NotEqual(property, Expression.Constant(null, typeof(string)));
            Expression combinedExpression = null;

            foreach (var value in values)
            {
                var lowerValue = Expression.Constant(value.ToLower(), typeof(string));
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var containsCall = Expression.Call(lowerProperty, containsMethod, lowerValue);
                combinedExpression = combinedExpression == null
                    ? containsCall
                    : Expression.OrElse(combinedExpression, containsCall);
            }
            var finalExpression = Expression.AndAlso(notNullCheck, combinedExpression);
            var lambda = Expression.Lambda<Func<T, bool>>(finalExpression, eParam);
            return q.Where(lambda);
        }
    }
}
