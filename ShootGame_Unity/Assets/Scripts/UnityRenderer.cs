using UnityEngine;

namespace ShootGame.Unity {
	public class UnityRenderer : MonoBehaviour {
		[SerializeField] SpriteRenderer Renderer = null;

		Texture2D _texture;

		public void Init(int width, int height) {
			var texture = new Texture2D(width, height);
			texture.filterMode = FilterMode.Point;
			var rect = new Rect(0, 0, texture.width, texture.height);
			Renderer.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), 1);
			_texture = texture;
		}

		public void SetPixel(int x, int y, float r, float g, float b, float a) {
			var color = new Color(r, g, b, a);
			_texture.SetPixel(x, y, color);
		}

		public void Apply() {
			_texture.Apply();
		}
	}
}
