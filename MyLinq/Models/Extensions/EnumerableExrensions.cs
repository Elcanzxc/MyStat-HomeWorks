using System;
using System.Collections.Generic;

namespace MyLinq.Models.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool All<T>(this IEnumerable<T> collection , Func<T,bool> predicate)
        {
            foreach(T element in collection)
            {
                if (!predicate(element))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Any<T>(this IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                return true;
            }
               
            return false;
        }

        public static bool Any<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            foreach (var item in collection)
            {
                if (predicate(item))
                    return true;
            }
            return false;
        }

        public static T First<T>(this IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                return item;
            }

            throw new InvalidOperationException("Не содержит элементов");

        }

        public static T First<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            foreach (var item in collection)
            {
                if (predicate(item))
                {
                    return item;
                }
                    
            }

            throw new InvalidOperationException("Не содержит элементов удовлетворяющих условие");
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                return item;
            }
               
            return default;
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            foreach (var item in collection)
            {
                if (predicate(item))
                {
                    return item;
                }
                   
            }

            return default;
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> collection, Func<T, bool> predicate,T defaultValue)
        {
            foreach (var item in collection)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            return defaultValue;
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            var result = new List<T>();

            foreach (var item in collection)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public static int Count<T>(this IEnumerable<T> collection)
        {
            int count = 0;
            foreach (var item in collection)
            {
                count++;
            }
            return count;
        }

        public static int Count<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            int count = 0;
            foreach (var item in collection)
            {
                if (predicate(item))
                    count++;
            }
            return count;
        }
    }
}
