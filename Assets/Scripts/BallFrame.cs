using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallFrame : MonoBehaviour {

	public float CanvasMaxWidth = 1280.0f;
	
	public float distance = 15f;
	private float _startWidth;
	// Use this for initialization
	void Start () {
		_startWidth = GetWidth();
	}
	
	// Update is called once per frame
	void Update () {
		float newWidth = GetWidth();
		float s = newWidth / _startWidth;
		this.transform.localScale = new Vector3(s, s, s);
	}

	private float GetWidth() {
		Vector3 v3ViewPort = new Vector3(0,0,distance);
		Vector3 v3BottomLeft = Camera.main.ViewportToScreenPoint(v3ViewPort);
		v3ViewPort.Set(1,1,distance);
		Vector3 v3TopRight = Camera.main.ViewportToScreenPoint(v3ViewPort);
		Debug.Log(v3TopRight);
		Debug.Log(v3BottomLeft);
		return v3BottomLeft.x + v3TopRight.x;
	}
}
