using UnityEngine;

namespace ShootGame.Unity {
	public class UnityRenderer : MonoBehaviour {
		const int BorderMargin = 1;

		[SerializeField] SpriteRenderer Renderer = null;

		Texture2D _texture;

		public void Init(int width, int height) {
			var marginWidth = width + BorderMargin * 2;
			var marginHeight = height + BorderMargin * 2;
			var texture = new Texture2D(marginWidth, marginHeight);
			texture.filterMode = FilterMode.Point;
			var rect = new Rect(0, 0, marginWidth, marginHeight);
			Renderer.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), 1);
			_texture = texture;
			Clear(marginWidth, marginHeight);
		}

		void Clear(int marginWidth, int marginHeight) {
			for ( var x = 0; x < marginWidth; x++ ) {
				for ( var y = 0; y < marginHeight; y++ ) {
					_texture.SetPixel(x, y, new Color(0, 0, 0, 0));
				}
			}
			Apply();
		}

		public void SetPixel(int x, int y, float r, float g, float b, float a) {
			var color = new Color(r, g, b, a);
			_texture.SetPixel(x + BorderMargin, y + BorderMargin, color);
		}

		public void Apply() {
			_texture.Apply();
		}
	}
}
