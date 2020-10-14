using System;

namespace Bloodstone.Extensions
{
    public static class AssertionsExtensions
    {
        public static T ThrowIfNull<T>(this T target, string message = null)
        {
            if (target == null)
            {
                throw message is null ? new ArgumentNullException() : new ArgumentNullException(message);
            }

            return target;
        }
    }
}