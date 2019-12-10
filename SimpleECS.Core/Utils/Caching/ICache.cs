namespace SimpleECS.Core.Utils.Caching {
	public interface ICache {
		void Release(object instance);
		void ReleaseAll();
		int GetTotalCount();
	}
}