using System;
using System.Collections.Generic;
using System.Linq;

namespace Share
{
    public static class CollectionEx
    {
        private static string ConvertObjectToString(object obj)
        {
            if (obj == null) return "null";

            Type type = obj.GetType();
            if (!type.IsClass || type == typeof(string)) return obj.ToString();

            IEnumerable<string> properties = type.GetProperties()
                .Select(p => $"{p.Name}:{p.GetValue(obj)}");

            IEnumerable<string> fields = type.GetFields()
                .Select(f => $"{f.Name}:{f.GetValue(obj)}");

            return string.Join(",\n", properties.Concat(fields));
        }
        public static string ArrayToString<T>(this T[] array, string separator)
        {
            return string.Join(separator, array);
        }
        public static string ArrayToStringDetail<T>(this T[] array, string separator)
        {
            string result = "";
            if (array.Length is 0 || !array[0].GetType().IsClass) return ArrayToString(array, separator);
            foreach (T item in array)
            {
                result = result.Append(ConvertObjectToString(item)).Append(separator);
            }
            return result;
        }
        public static string ListToString<T>(this List<T> list, string separator)
        {
            return string.Join(separator, list);
        }
        public static string ListToStringDetail<T>(this List<T> list, string separator)
        {
            string result = "";
            if (list.Count is 0 || !list[0].GetType().IsClass) return ListToString(list, separator);
            foreach (T item in list)
            {
                result = result.Append(ConvertObjectToString(item)).Append(separator);
            }
            return result;
        }
        public static void SafeAdd<T>(this IList<T> list, T value)
        {
            if (list == null) return;
            if (list.Contains(value)) return;
            list.Add(value);
        }
        public static void SafeAddRange<T>(this IList<T> list, IEnumerable<T> values)
        {
            if (list == null) return;
            if (values == null) return;
            
            foreach (T value in values)
            {
                if (list.Contains(value)) continue;
                list.Add(value);
            }
        }
        public static void SafeRemove<T>(this IList<T> list, T value)
        {
            if (list == null) return;
            if (!list.Contains(value)) return;
            list.Remove(value);
        }
        
        public static void SafeRemoveRange<T>(this IList<T> list, IEnumerable<T> values)
        {
            if (list == null) return;
            if (values == null) return;
            
            foreach (T value in values)
            {
                if (!list.Contains(value)) continue;
                list.Remove(value);
            }
        }
        public static void AddOrUpdate<TValue>(this IList<TValue> list, TValue value)
        {
            if (list.Contains(value))
                list[list.IndexOf(value)] = value;
            else
                list.Add(value);
        }
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, IList<TValue>> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key].Add(value);
            else
                dictionary.Add(key, new List<TValue> {value});
        }
        public static void AddOrUpdate<TKey, TSubKey, TSubValue>(this IDictionary<TKey, IDictionary<TSubKey, TSubValue>> dictionary, TKey key, TSubKey value, TSubValue subValue)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key].AddOrUpdate(value, subValue);
            else
                dictionary.Add(key, new Dictionary<TSubKey, TSubValue> {{value, subValue}});
        }
        
        public static void SafeRemove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (!dictionary.ContainsKey(key)) return;
            dictionary.Remove(key);
        }
        public static string DictionaryToString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, string separator)
        {
            return string.Join(separator, dictionary.Select(x => $"{x.Key} : {x.Value}"));
        }
        public static string DictionaryToStringDetail<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, string separator)
        {
            string result = "";
            bool keyIsClass = dictionary.Keys.Count is not 0 && dictionary.Keys.First().GetType().IsClass;
            bool valueIsClass = dictionary.Keys.Count is not 0 && dictionary.Values.First().GetType().IsClass;
            string keyString = "";
            string valueString = "";
            foreach (KeyValuePair<TKey, TValue> item in dictionary)
            {
                if (keyIsClass)
                    keyString = keyString.Append(ConvertObjectToString(item.Key)).Append(",");
                if (valueIsClass)
                    valueString = valueString.Append(ConvertObjectToString(item.Value)).Append(",");
                result = result.Append($"{(keyIsClass ? keyString : item.Key)} : {(valueIsClass ? valueString : ConvertObjectToString(item.Value))}").Append(separator);
            }
            return result;
        }
        
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action, Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                ForEach(enumerable, action);
                return;
            }
            
            foreach (T item in enumerable)
            {
                if(predicate.Invoke(item))
                    action(item);
            }
        }
        
        public static ICollection<T> FluentForEach<T>(this ICollection<T> enumerable, Action<T> action, Func<T, bool> predicate)
        {
            foreach (T item in enumerable)
            {
                if(predicate.Invoke(item))
                    action(item);
            }

            return enumerable;
        }

        public static void For<T>(this IList<T> collection, Action<int, T> action, int start = 0, int modifier = 1)
        {
            if (collection == null) return;
            if (collection.Count == 0) return;
            
            for (int i = start; i < collection.Count; i += modifier)
            {
                action(i, collection[i]);
            }
        }

        public static IList<T> FluentFor<T>(this IList<T> collection, Action<int, T> action, int start = 0, int modifier = 1)
        {
            if (collection == null) return null;
            if (collection.Count == 0) return collection;
            
            for (int i = start; i < collection.Count; i += modifier)
            {
                action(i, collection[i]);
            }

            return collection;
        }

        public static void For(Action action, int count, int start = 0, int modifier = 1)
        {
            for (int i = start; i < count; i += modifier)
            {
                action();
            }
        }

        public static void For(Action<int> action, int count, int start = 0, int modifier = 1)
        {
            for (int i = start; i < count; i += modifier)
            {
                action(i);
            }
        }
        public static void For(this object obj, Action<int, object> action, int count, int start = 0, int modifier = 1)
        {
            for (int i = start; i < count; i += modifier)
            {
                action(i, obj);
            }
        }
        
        public static void For<T>(this T obj, Action<T> action, int count, int start = 0, int modifier = 1)
        {
            for (int i = start; i < count; i += modifier)
            {
                action(obj);
            }
        }
        public static void For<T>(this T obj, Action<int, T> action, int count, int start = 0, int modifier = 1)
        {
            for (int i = start; i < count; i += modifier)
            {
                action(i, obj);
            }
        }
        public static void For<T>(this IList<T> collection, Action<int, T> action, Func<int, T ,bool> predicate, int start = 0, int modifier = 1)
        {
            if (collection == null) return;
            if (collection.Count == 0) return;
            
            if(predicate == null)
            {
                For(collection, action, start, modifier);
                return;
            }
            
            for (int i = start; i < collection.Count; i += modifier)
            {
                if(!predicate.Invoke(i, collection[i])) continue;
                
                action(i, collection[i]);
            }
        }  
    }
}
