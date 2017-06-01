using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraFollow : MonoBehaviour {
	
	private Transform _mainTarget;
	public Transform floorFocusPoint;
	public float smoothing = 5f;
	Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - floorFocusPoint.position;
	}

	public void MoveTo(Transform target) {
		_mainTarget = target;
	}
	// Update is called once per frame
	void FixedUpdate () {
		// TODO: error after object has been destroyed.
		if (_mainTarget) {
			Vector3 targetCamPos = _mainTarget.position + offset;
			transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
		}
		
	}
}
