using System.Collections.Generic;

namespace SimpleECS.Core.Utils.Caching {
	sealed class Cache<T> : ICache where T : class, new() {
		readonly Stack<T> _free = new Stack<T>();
		readonly List<T>  _used = new List<T>();

		public T Hold() {
			var instance = (_free.Count > 0) ? _free.Pop() : new T();
			_used.Add(instance);
			return instance;
		}

		public void Release(T instance) {
			_used.Remove(instance);
			_free.Push(instance);
		}

		public void Release(object instance) {
			Release((T)instance);
		}

		public void ReleaseAll() {
			foreach ( var item in _used ) {
				_free.Push(item);
			}
			_used.Clear();
		}
	}
}