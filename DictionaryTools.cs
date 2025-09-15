using System;
using System.Collections.Generic;
using System.Text;

namespace BOBetterIntentsAndRewording
{
    public static class DictionaryTools
    {
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key) where TValue : new()
        {
            if (dict.TryGetValue(key, out var value))
                return value;

            return dict[key] = new();
        }
    }
}
