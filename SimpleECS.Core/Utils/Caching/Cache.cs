using System.Collections.Generic;

namespace SimpleECS.Core.Utils.Caching {
	public sealed class Cache<T> : ICache where T : class, new() {
		readonly Stack<T> _free;
		readonly List<T>  _used;

		public Cache() {
			_free = new Stack<T>();
			_used = new List<T>();
		}

		public Cache(int capacity) {
			_free = new Stack<T>(capacity);
			_used = new List<T>(capacity);
		}

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

		public int GetTotalCount() {
			return _free.Count + _used.Count;
		}
	}
}