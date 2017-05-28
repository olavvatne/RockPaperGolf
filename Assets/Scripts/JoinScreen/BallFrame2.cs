using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallFrame2 : MonoBehaviour {

	public RectTransform frame;
	public float ballPosVert = 300;
	private CanvasScaler canvas;
	// Use this for initialization
	void Start () {
		canvas = frame.parent.GetComponent<CanvasScaler>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos2;
		float sx = Screen.width / canvas.referenceResolution.x;
		float sy = Screen.height / canvas.referenceResolution.y;
		float offset = canvas.referenceResolution.x / 2;
		Vector3 framePos = frame.localPosition;
		RectTransformUtility.ScreenPointToWorldPointInRectangle(
			frame,
			new Vector2((offset +framePos.x)*sx , ballPosVert * sy),
			Camera.main,
			out pos2
		);
		this.transform.position = pos2;
	}
}
