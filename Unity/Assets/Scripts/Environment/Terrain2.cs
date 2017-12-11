using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain2 : MonoBehaviour {

	private SpriteRenderer sr;
	private float widthWorld, heightWorld;
	private int widthPixel, heightPixel;
	private Color transp; 

	void Start(){
		sr = GetComponent<SpriteRenderer>(); 
		Texture2D tex = (Texture2D) Resources.Load("Images/Islands/" + this.name);
		Texture2D tex_clone = (Texture2D) Instantiate(tex);
		sr.sprite = Sprite.Create(tex_clone, 
			new Rect(0f, 0f, tex_clone.width, tex_clone.height),
			new Vector2(0.5f, 0.5f), 100f);
		transp = new Color(0f, 0f, 0f, 0f);
		InitSpriteDimensions();
	}



	private void InitSpriteDimensions() {
		widthWorld = sr.bounds.size.x;
		heightWorld = sr.bounds.size.y;
		widthPixel = sr.sprite.texture.width;
		heightPixel = sr.sprite.texture.height;
	}

	void Update () {

	}

	public void DestroyGround( CircleCollider2D cc ){

		V2int c = World2Pixel(cc.bounds.center.x, cc.bounds.center.y);
		int r = Mathf.RoundToInt(cc.bounds.size.x*widthPixel/widthWorld);

		int x, y, px, nx, py, ny, d;

		for (x = 0; x <= r; x++)
		{
			d = (int)Mathf.RoundToInt(Mathf.Sqrt(r * r - x * x));

			for (y = 0; y <= d; y++)
			{
				px = c.x + x;
				nx = c.x - x;
				py = c.y + y;
				ny = c.y - y;

				if (px < widthPixel && px > 0 && py < heightPixel && py > 0)
					sr.sprite.texture.SetPixel (px, py, transp);
				if (nx < widthPixel && nx > 0 && py < heightPixel && py > 0)
					sr.sprite.texture.SetPixel (nx, py, transp);
				if (px < widthPixel && px > 0 && ny < heightPixel && ny > 0)
					sr.sprite.texture.SetPixel (px, ny, transp);
				if (nx < widthPixel && nx > 0 && ny < heightPixel && ny > 0)
					sr.sprite.texture.SetPixel (nx, ny, transp);
			}
		}
		sr.sprite.texture.Apply();
		Destroy(GetComponent<PolygonCollider2D>());
		gameObject.AddComponent<PolygonCollider2D>();
	}

	private V2int World2Pixel(float x, float y) {
		V2int v = new V2int();

		float dx = x-transform.position.x;
		v.x = Mathf.RoundToInt(0.5f*widthPixel+ dx*widthPixel/widthWorld);

		float dy = y - transform.position.y;
		v.y = Mathf.RoundToInt(0.5f * heightPixel + dy * heightPixel / heightWorld);

		return v;
	}
}
