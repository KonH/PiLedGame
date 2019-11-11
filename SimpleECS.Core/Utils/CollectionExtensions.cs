using System;
using System.Collections.Generic;

namespace SimpleECS.Core.Utils {
	public static class CollectionExtensions {
		public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key) where TKey : notnull {
			return dictionary.GetValueOrDefault(key, default);
		}

		public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) where TKey : notnull {
			if ( dictionary == null ) {
				throw new ArgumentNullException(nameof(dictionary));
			}
			return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
		}
	}
}