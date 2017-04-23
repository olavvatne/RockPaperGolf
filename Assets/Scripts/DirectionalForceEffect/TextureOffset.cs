using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureOffset : MonoBehaviour {
	public float speed = 10f;
	private float _prevOffset = 0f;
	private Renderer _renderer;
	// Use this for initialization
	void Start () {
		_renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		float newOffset = _prevOffset + (0.1f * speed * Time.deltaTime) % 10f;
		_prevOffset = newOffset;
		_renderer.material.SetTextureOffset("_MainTex", new Vector2(newOffset, 0));
	}
}
