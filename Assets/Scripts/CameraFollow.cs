using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraFollow : MonoBehaviour {
	
	private Transform _mainTarget;
	public Transform floorFocusPoint;
	public float smoothing = 5f;
	public float angularSpeed = 2f;
	public float zoomSpeed = 1f;
	Vector3 currentOffset;

	private float _minZoom = 1;
	private float _maxZoom = 10;

	// Use this for initialization
	void Start () {
		currentOffset = transform.position - floorFocusPoint.position;
	}

	public void MoveTo(Transform target) {
		_mainTarget = target;
		//transform.position = _mainTarget.position + currentOffset;
	}
	// Update is called once per frame
	void Update () {
		// TODO: error after object has been destroyed.
		if (_mainTarget) {
			float movement = Input.GetAxis("Horizontal") * angularSpeed * Time.deltaTime;
			float zoom = Input.GetAxis("Vertical") * zoomSpeed * Time.deltaTime;

			Vector3 targetCamPos = _mainTarget.position + currentOffset;
			transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
			
			if(!Mathf.Approximately (movement, 0f)) {
				transform.RotateAround ( _mainTarget.position, Vector3.up, movement);
				currentOffset = transform.position - _mainTarget.position;
			}
			if(!Mathf.Approximately (zoom, 0f)) {
				Vector3 newOffset = currentOffset + (currentOffset * zoom);
				if (newOffset.magnitude > _minZoom && newOffset.magnitude < _maxZoom) {
					currentOffset = newOffset;
				}
			}
		}
		
	}
}
