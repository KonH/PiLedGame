using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleECS.Core.Utils.Caching {
	public sealed class CacheScope {
		readonly Dictionary<Type, ICache> _caches = new Dictionary<Type, ICache>();

		public CacheScope() {
			// Hack
			ReleaseAll();
		}

		public CacheScope Init<T>(int capacity = 4) where T : class, new() {
			_caches[typeof(T)] = new Cache<T>(capacity);
			return this;
		}

		public T Hold<T>() where T : class, new() {
			if ( !_caches.TryGetValue(typeof(T), out var cache) ) {
				cache = new Cache<T>();
				_caches.Add(typeof(T), cache);
			}
			return ((Cache<T>)cache).Hold();
		}

		public void Release<T>(T instance) where T : class, new() {
			if ( !_caches.TryGetValue(typeof(T), out var cache) ) {
				cache = new Cache<T>();
				_caches.Add(typeof(T), cache);
			}
			((Cache<T>)cache).Release(instance);
		}

		public void Release(Type type, object instance) {
			if ( !_caches.TryGetValue(type, out var cache) ) {
				return;
			}
			cache.Release(instance);
		}

		public void ReleaseAll() {
			foreach ( var pair in _caches ) {
				pair.Value.ReleaseAll();
			}
		}

		public Dictionary<Type, int> GetTotalCounts() {
			return _caches.ToDictionary(c => c.Key, c => c.Value.GetTotalCount());
		}
	}
}