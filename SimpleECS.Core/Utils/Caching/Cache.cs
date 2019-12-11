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
			for ( var i = 0; i < capacity; i++ ) {
				_free.Push(new T());
			}
		}

		public T Hold() {
			var instance = (_free.Count > 0) ? _free.Pop() : new T();
			_used.Add(instance);
			return instance;
		}

		public void Release(T instance) {
			for ( var i = 0; i < _used.Count; i++ ) {
				if ( _used[i] == instance ) {
					_used.RemoveAt(i);
					i--;
				}
			}
			_free.Push(instance);
		}

		public void Release(object instance) {
			Release((T)instance);
		}

		public void ReleaseAll() {
			for ( var i = 0; i < _used.Count; i++ ) {
				_free.Push(_used[i]);
			}
			_used.Clear();
		}

		public int GetTotalCount() {
			return _free.Count + _used.Count;
		}
	}
}